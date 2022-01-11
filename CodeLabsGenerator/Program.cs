using System;

namespace CodeLabsGenerator
{
    class Program
    {
        private static readonly string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\extralevelfolder\BasePage.html";

        static void Main(string[] args)
        {
            string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\EERDiagramInAstah";
            // string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\BlazorLogin";
            Console.WriteLine("Generating \"" + folderPathToMdSteps.Split("\\")[^1] + "\" ...");
            Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
            Console.WriteLine("Done!");
        }
    }
}