using System;
using System.Collections.Generic;

namespace CodeLabsGenerator
{
    class Program
    {
        private static readonly string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\extralevelfolder\BasePage.html";

        static void Main(string[] args)
        {
            // GenerateOne("GoodreadsExercises");
            // GenerateOne("DvdRentalExercises");
            GenerateOne("TodoTutorialPart1_Blazor");
            // GenerateMany();
        }

        private static void GenerateOne(string tutorial)
        {
            string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\" + tutorial;
            Console.WriteLine("Generating \"" + folderPathToMdSteps.Split("\\")[^1] + "\" ...");
            Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
            Console.WriteLine("Done!");
        }

        private static void GenerateMany()
        {
            List<string> tutes = new()
            {
                "BlazorLogin",
                "CodelabsDoc",
                "CsharpDebugging",
                "CsharpSockets",
                "CsharpThreads",
                "DML",
                "DvdRentalExercises",
                "EERDiagramInAstah",
                "EerToLogical",
                "SepAppendix",
                "SQLAdventure",
                "TodoTutorialPart1_Blazor",
                "TodoTutorialPart2_WebApi",
                "TodoTutorialPart3_Client",
                "WebShopExercises",
            };
            tutes.ForEach(t => GenerateOne(t));
        }
    }
}