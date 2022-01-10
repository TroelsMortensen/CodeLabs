using System.Text;
using Markdig;

namespace IndexGenerator;

public class Generator
{
    private static string folderPathToMds = @"C:\TRMO\RiderProjects\CodeLabs\IndexMds";
    private static string indexBasePagePath = @"C:\TRMO\RiderProjects\CodeLabs\IndexGenerator\indexbase.html";
    private static string indexPagePath = @"C:\TRMO\RiderProjects\CodeLabs\index.html";
    private static MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

    private static string[] colors =
    {
        "#ffc7c5",
        "#e0ffbf",
        "#ffae93",
        "#ffb1dd",
        "#c2ecff",
        "#cddc39",
        "#795548",
        "#9c27b0",
        "#00bcd4",
        "#ffeb3b",
        "#9e9e9e",
        "#673ab7",
        "#009688",
        "#ffc107",
        "#607d8b",
        "#3f51b5",
        "#4caf50",
        "#ff9800"
    };

    public static void GenerateIndexPage()
    {
        StringBuilder mainBuilder = new StringBuilder();
        ConvertAllMdFiles(mainBuilder);
        string indexBasePageAsText = File.ReadAllText(indexBasePagePath);
        var result = indexBasePageAsText.Replace("###CARDS###", mainBuilder.ToString());
        File.WriteAllText(indexPagePath, result);
    }

    private static void ConvertAllMdFiles(StringBuilder mainBuilder)
    {
        List<string> mdFileNames = Directory.GetFiles(folderPathToMds, "*.md").ToList();
        int idx = 0;
        foreach (string mdFilePath in mdFileNames)
        {
            mainBuilder.Append($"<div class=\"card\" style=\"background-color: {colors[idx]}\">").Append("\n");
            {
                string mdText = File.ReadAllText(mdFilePath);
                string mdAsHtml = Markdown.ToHtml(mdText, pipeline);
                mainBuilder.Append(mdAsHtml);
            }
            mainBuilder.Append("</div>");
            idx++;
        }
    }
}