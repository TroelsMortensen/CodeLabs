namespace CodeLabsGenerator
{
    class Program
    {
        private static readonly string basePagePath = @"C:\TRMO\RiderProjects\CodeLabs\BasePageSource\extralevelfolder\BasePage.html";

        static void Main(string[] args)
        {
            string folderPathToMdSteps = @"C:\TRMO\RiderProjects\CodeLabs\Tutorials\CsharpTodoTutorialPart1";
            Generator.GenerateHtmlPageFromMdFiles(folderPathToMdSteps, basePagePath);
        }
    }
}