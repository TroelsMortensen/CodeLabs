﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            InsertDropDownStepsOverview(pathToMdSteps, mainBuilder);
            InsertAllTabsFromMdFiles(pathToMdSteps, mainBuilder);
            AddLineNumberClassToCodeTag(mainBuilder);
            MoveLineHighlightingAttributes(mainBuilder);
            InsertNumberedRingSteps(mainBuilder);
            WriteFinalPageToFile(pathToMdSteps, mainBuilder);
        }



        private static void InsertNumberedRingSteps(StringBuilder mainBuilder)
        {
            Regex pattern = new(@"\(\(\d*\)\)");
            MatchCollection matchCollection = pattern.Matches(mainBuilder.ToString());
            foreach (Match match in matchCollection)
            {
                string theMatch = match.Value;
                string stepNumber = theMatch.Replace("((", "").Replace("))", "");
                string replacementHtml = $"<span class=\"numberCircle\"><span>{stepNumber}</span></span>";
                mainBuilder.Replace(theMatch, replacementHtml);
            }
        }

        private static void MoveLineHighlightingAttributes(StringBuilder mainBuilder)
        {
            // Regex pattern = new Regex("<pre><code class=\"line-numbers language-(?<language>[a-z]){0,15}\\{\\d\\}\">");
            Regex pattern = new Regex("<pre><code class=\"line-numbers language-[a-z]{0,15}{(.*?)}\">");
            MatchCollection matchCollection = pattern.Matches(mainBuilder.ToString());
            foreach (Match match in matchCollection)
            {
                string existingHtml = match.Value;
                // Regex regex = new Regex("{(.*?)}");
                // Match dataLineValueMatch = regex.Match(existingHtml);
                string dataLineValue = match.Groups[1].Value;

                string replacementHtml = Regex.Replace(existingHtml, @"{(.*?)}", "");
                replacementHtml = replacementHtml.Replace("pre", $"pre data-line=\"{dataLineValue}\"");
                mainBuilder.Replace(existingHtml, replacementHtml);
            }
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

            var mdFiles = GetAndSortMdFiles(pathToMdSteps);

            StringBuilder sb = new();
            for (int i = 0; i < mdFiles.Count; i++)
            {
                string fileName = ExtractMdFileName(mdFiles[i]);
                fileName = StripLeadingZeros(fileName);
                sb.Append($"<li class=\"step\" onclick=\"setTab({i})\">{fileName}</li>").Append('\n');
            }

            mainBuilder.Replace("###STEPOVERVIEW###", sb.ToString());
        }
        
        // should refactor this to use the same method as InsertStepsOverview, somehow
        private static void InsertDropDownStepsOverview(string pathToMdSteps, StringBuilder mainBuilder)
        {
            var mdFiles = GetAndSortMdFiles(pathToMdSteps);

            StringBuilder sb = new();
            for (int i = 0; i < mdFiles.Count; i++)
            {
                string fileName = ExtractMdFileName(mdFiles[i]);
                fileName = StripLeadingZeros(fileName);
                sb.Append($"<span class=\"drop-down-step\" onclick=\"setTab({i})\">{fileName}</span>").Append('\n');
            }

            mainBuilder.Replace("###DROPDOWNSTEPOVERVIEW###", sb.ToString());
        }
        
        private static string StripLeadingZeros(string fileName)
        {
            var trimmedForZero = fileName.TrimStart('0');
            return trimmedForZero;
        }

        private static List<string> GetAndSortMdFiles(string pathToMdSteps)
        {
            List<string> mdFiles = Directory.GetFiles(pathToMdSteps, "*.md").ToList();

            mdFiles = mdFiles.OrderBy(s =>
                int.Parse(
                    ExtractMdFileName(s).Split(' ')[0]
                )
            ).ToList();
            return mdFiles;
        }

        private static string ExtractMdFileName(string fileName)
        {
            string[] pathParts = fileName.Split("\\"); //TODO not linux/mac friendly?
            fileName = pathParts[^1].Replace(".md", ""); // the ^1 is index from behind, we don't start at 0 here
            return fileName;
        }

        private static void InsertAllTabsFromMdFiles(string pathToMdSteps, StringBuilder mainBuilder)
        {
            List<string> mdFiles = GetAndSortMdFiles(pathToMdSteps);

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
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string mdAsHtml = Markdown.ToHtml(mdFileContent, pipeline);
            StringBuilder sb = new();
            sb.Append("<div class=\"tab\">").Append('\n');
            {
                sb.Append(mdAsHtml).Append('\n');
                InsertHorizontalLine(sb);
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        private static void InsertHorizontalLine(StringBuilder sb)
        {
            sb.Replace("</h1>", "</h1><hr/>");
            sb.Replace("<h1", "<hr/><h1");
        }
    }
}