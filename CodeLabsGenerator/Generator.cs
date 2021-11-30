using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Markdig;

namespace CodeLabsGenerator
{
    public static class Generator
    {
        public static void GenerateHtmlPageFromMdFiles(string pathToMdSteps, string basePagePath)
        {
            StringBuilder mainBuilder = new();
            InsertBasePageHtmlFromFile(basePagePath, mainBuilder);
            InsertStepsOverview(pathToMdSteps, mainBuilder);
            InsertAllTabsFromMdFiles(pathToMdSteps, mainBuilder);
            AddLineNumberClassToCodeTag(mainBuilder);
            WriteFinalPageToFile(pathToMdSteps, mainBuilder);
        }

        private static void AddLineNumberClassToCodeTag(StringBuilder basePage)
        {
            basePage.Replace("class=\"language-", "class=\"line-numbers language-");
        }

        private static void WriteFinalPageToFile(string outputPath, StringBuilder mainBuilder)
        {
            File.WriteAllText(outputPath + "/Page.html", mainBuilder.ToString());
        }

        private static void InsertBasePageHtmlFromFile(string s, StringBuilder stringBuilder)
        {
            string basePage = File.ReadAllText(s);
            stringBuilder.Append(basePage);
        }

        private static void InsertStepsOverview(string pathToMdSteps, StringBuilder mainBuilder)
        {
            // TODO at some point, remove any leading numbering of file name, and prefix automatically by the i variable in the loop. I foresee that when changes are made to the tutorial steps, we will see numberings like 3.1, and 3.05 etc. The final output should number better
            
            string[] fileNames = Directory.GetFiles(pathToMdSteps, "*.md");
            StringBuilder sb = new();
            for (int i = 0; i < fileNames.Length; i++)
            {
                string fileName = ExtractMdFileName(fileNames, i);
                sb.Append($"<li class=\"step\" onclick=\"setTab({i})\">{fileName}</li>").Append('\n');
            }

            mainBuilder.Replace("###STEPOVERVIEW###", sb.ToString());
        }

        private static string ExtractMdFileName(string[] fileNames, int i)
        {
            var fileName = fileNames[i];
            string[] pathParts = fileName.Split("\\"); //TODO not linux/mac friendly?
            fileName = pathParts[^1].Replace(".md", ""); // the ^1 is index from behind, we don't start at 0 here
            return fileName;
        }

        private static void InsertAllTabsFromMdFiles(string pathToMdSteps, StringBuilder mainBuilder)
        {
            List<string> mdFiles = Directory.GetFiles(pathToMdSteps, "*.md").ToList();
            StringBuilder sb = new();
            foreach (string mdFile in mdFiles)
            {
                string tab = ConstructSingleTab(mdFile);
                sb.Append(tab).Append('\n');
            }

            mainBuilder.Replace("###STEPPAGES###", sb.ToString());
        }

        private static string ConstructSingleTab(string mdFile)
        {
            string mdFileContent = File.ReadAllText(mdFile);
            string mdAsHtml = Markdown.ToHtml(mdFileContent);
            StringBuilder sb = new();
            sb.Append("<div class=\"tab\">").Append('\n');
            {
                sb.Append(mdAsHtml).Append('\n');
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}