using System.Net.Mime;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test
{
    public class Test
    {
        public static void VoidMethod()
        {

        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var m = typeof(Test).GetMethods(BindingFlags.Static | BindingFlags.Public);
        }
    }

    //public static class SyncCommand
    //{
    //    public static async Task<int> Do(
    //        SyncOptions syncOptions,
    //        IConsole console,
    //        StartupOptions startupOptions = null
    //    )
    //    {
    //        var directoryAccessor = syncOptions.RootDirectory;
    //        var packageRegistry = PackageRegistry.CreateForTryMode(directoryAccessor);
    //        var markdownProject = new MarkdownProject(
    //            directoryAccessor,
    //            packageRegistry,
    //            startupOptions);

    //        var markdownFiles = markdownProject.GetAllMarkdownFiles().ToArray();
    //        if (markdownFiles.Length == 0)
    //        {
    //            console.Error.WriteLine($"No markdown files found under {directoryAccessor.GetFullyQualifiedRoot()}");
    //            return -1;
    //        }

    //        foreach (var markdownFile in markdownFiles)
    //        {
    //            var pipeline = markdownFile.Project.GetMarkdownPipelineFor(markdownFile.Path);
                
    //            var document = Markdig.Markdown.Parse(
    //                markdownFile.ReadAllText(),
    //                pipeline);

    //            var newPipeLine = new MarkdownPipelineBuilder().UseNormalizeCodeBlockAnnotations().Build();
    //            var writer = new StringWriter();

    //            var renderer = new NormalizeRenderer(writer);
    //            newPipeLine.Setup(renderer);

    //            var blocks = document
    //                .OfType<AnnotatedCodeBlock>()
    //                .OrderBy(c => c.Order)
    //                .ToList();

    //            if (!blocks.Any())
    //                continue;

    //            await Task.WhenAll(blocks.Select(b => b.InitializeAsync()));

    //            renderer.Render(document);
    //            writer.Flush();

    //            var updated = writer.ToString();

    //            var fullName = directoryAccessor.GetFullyQualifiedPath(markdownFile.Path).FullName;
    //            File.WriteAllText(Path.ChangeExtension(fullName, "synced.md"), updated);
    //        }

    //        console.Out.WriteLine("YEAH");
    //        return 0;
    //    }
    //}
}
