using System;
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
            // basePage = InsertStepsIntoBasePage(basePage, steps);
            // basePage = InsertTabsIntoBasePage(basePage, tabs);
            
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

        private static string InsertTabsIntoBasePage(string basePage, string tabs)
        {
            return basePage.Replace("###STEPPAGES###", tabs);
        }

        private static string InsertStepsIntoBasePage(string basePage, string steps)
        {
            return basePage.Replace("###STEPOVERVIEW###", steps);
        }

        private static void InsertBasePageHtmlFromFile(string s, StringBuilder stringBuilder)
        {
            string basePage = File.ReadAllText(s);
            stringBuilder.Append(basePage);
        }

        private static void InsertStepsOverview(string pathToMdSteps, StringBuilder mainBuilder)
        {
            string[] fileNames = Directory.GetFiles(pathToMdSteps, "*.md");
            StringBuilder sb = new();
            for (int i = 0; i < fileNames.Length; i++)
            {
                string fileName = ExtractFileName(fileNames, i);
                sb.Append($"<li class=\"step\" onclick=\"setTab({i})\">{fileName}</li>").Append("\n");
            }

            mainBuilder.Replace("###STEPOVERVIEW###", sb.ToString());
        }

        private static string ExtractFileName(string[] fileNames, int i)
        {
            var fileName = fileNames[i];
            string[] pathParts = fileName.Split("\\"); //TODO not linux/mac friendly?
            fileName = pathParts[^1].Replace(".md", ""); // the ^1 is index from behind
            return fileName;
        }

        private static void InsertAllTabsFromMdFiles(string pathToMdSteps, StringBuilder mainBuilder)
        {
            List<string> mdFiles = Directory.GetFiles(pathToMdSteps, "*.md").ToList();
            StringBuilder sb = new();
            foreach (string mdFile in mdFiles)
            {
                string tab = ConstructSingleTab(mdFile);
                sb.Append(tab);
                sb.Append("\n");
            }

            mainBuilder.Replace("###STEPPAGES###", sb.ToString());
        }

        private static string ConstructSingleTab(string mdFile)
        {
            string mdFileContent = File.ReadAllText(mdFile);
            string mdAsHtml = Markdown.ToHtml(mdFileContent);
            StringBuilder sb = new();
            sb.Append("<div class=\"tab\">").Append("\n");
            {
                sb.Append(mdAsHtml).Append("\n");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}