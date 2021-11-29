using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Markdig;

namespace CodeLabsGenerator
{
    public class Generator
    {
        public static void GenerateHtmlPageFromMdFiles(string pathToMdSteps, string basePagePath)
        {
            string basePage = ReadBasePageHtmlFromFile(basePagePath);
            string steps = ConstructStepsOverview(pathToMdSteps);
            string tabs = ConstructAllTabsFromMdFiles(pathToMdSteps);
            basePage = InsertStepsIntoBasePage(basePage, steps);
            basePage = InsertTabsIntoBasePage(basePage, tabs);
            basePage = FixLanguageClass(basePage);
            basePage = AddLineNumberClassToCodeTag(basePage);
            WriteFinalPageToFile(pathToMdSteps, basePage);
        }

        private static string AddLineNumberClassToCodeTag(string basePage)
        {
            return basePage.Replace("class=\"language-", "class=\"line-numbers language-");
        }

        private static string FixLanguageClass(string basePage)
        {
            return basePage.Replace("language-C#", "language-csharp");
        }

        private static void WriteFinalPageToFile(string outputPath, string basePage)
        {
            // if (!File.Exists(outputPath))
            // {
            //     Console.WriteLine("Creating");
            //     File.Create(outputPath);
            // }
            File.WriteAllText(outputPath + "/Page.html", basePage);
        }

        private static string InsertTabsIntoBasePage(string basePage, string tabs)
        {
            return basePage.Replace("###STEPPAGES###", tabs);
        }

        private static string InsertStepsIntoBasePage(string basePage, string steps)
        {
            return basePage.Replace("###STEPOVERVIEW###", steps);
        }

        private static string ReadBasePageHtmlFromFile(string s)
        {
            string basePage = File.ReadAllText(s);
            return basePage;
        }

        private static string ConstructStepsOverview(string pathToMdSteps)
        {
            int numOfMdFiles = Directory.GetFiles(pathToMdSteps, "*.md").Length;
            StringBuilder sb = new();
            for (int i = 0; i < numOfMdFiles; i++)
            {
                sb.Append($"<span class=\"step\" onclick=\"setTab({i})\">{i}</span>").Append("\n");
            }

            return sb.ToString();
        }

        private static string ConstructAllTabsFromMdFiles(string pathToMdSteps)
        {
            List<string> mdFiles = Directory.GetFiles(pathToMdSteps, "*.md").ToList();
            StringBuilder sb = new();
            foreach (string mdFile in mdFiles)
            {
                string tab = ConstructSingleTab(mdFile);
                sb.Append(tab);
                sb.Append("\n");
            }

            return sb.ToString();
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