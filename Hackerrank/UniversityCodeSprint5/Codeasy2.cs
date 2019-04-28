using System;
using System.Text;

public static class HelloWorld {
    static string exePath;
    static StringBuilder sb = new StringBuilder();
    const int MAX_OUTPUT_SIZE = 4000;
    public static void Main2() {
        exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        go();

        string output = sb.ToString();
        if (sb.Length > MAX_OUTPUT_SIZE) output = sb.ToString().Substring(0, MAX_OUTPUT_SIZE);

        Console.WriteLine(output);

        Console.WriteLine("===================== Done =====================");
    }

    public static void ReadFilesList(string path) {
        sb.AppendLine("Reading Files List: " + path);
        var list = System.IO.Directory.GetFiles(path);
        var str = string.Join("\n", list);
        sb.AppendLine(str);
        sb.AppendLine();
    }

    public static void ReadDirectoriesList(string path, int maxLevel = 0) {
        if (maxLevel < 0) return;
        try {
            var list = System.IO.Directory.GetDirectories(path);
            if (list.Length == 0) return;
            sb.AppendLine("Reading Directories List: " + path);
            var str = string.Join("\n", list);
            sb.AppendLine(str);
            sb.AppendLine();

            foreach (var dir in list) {
                ReadDirectoriesList(dir, maxLevel - 1);
            }

        } catch (System.UnauthorizedAccessException) {
            sb.AppendLine("UnauthorizedAccessException: Failed to read directory: " + path);
        }
    }

    public static void ReadFile(string path, int length = Int32.MaxValue) {
        sb.AppendLine("Reading File: " + path);
        var text = System.IO.File.ReadAllText(path);
        if (length <= text.Length) text = text.Substring(0, length);
        sb.AppendLine(text);
        sb.AppendLine();
    }

    public static void go() {
        //ReadFilesList(@"C:\codeasy");
        //ReadFilesList(@"C:\codeasy\executor");

        //ReadFile(@"C:\codeasy\Codeasy.CodeChecker.HandlerSvc.exe.config");
        //ReadFile(@"C:\codeasy\Codeasy.CodeChecker.Implementation.dll.config");
        //ReadFile(@"C:\codeasy\Codeasy.Domain.dll.config");
        //ReadFile(@"C:\codeasy\executor\Codeasy.CodeChecker.ExecutorSvc.exe.config");
        //ReadFile(@"C:\codeasy\executor\Codeasy.CodeChecker.Implementation.dll.config");
        //ReadFile(@"C:\codeasy\executor\Codeasy.Domain.dll.config");

        //ReadFile(@"C:\codeasy\EasyNetQ.xml");
        //ReadFile(@"C:\codeasy\Microsoft.CodeAnalysis.CSharp.xml");
        //ReadFile(@"C:\codeasy\Microsoft.CodeAnalysis.xml");
        //ReadFile(@"C:\codeasy\Newtonsoft.Json.xml");
        //ReadFile(@"C:\codeasy\Ninject.xml");
        //ReadFile(@"C:\codeasy\RabbitMQ.Client.xml");
        //ReadFile(@"C:\codeasy\RestSharp.xml");
        //ReadFile(@"C:\codeasy\System.Collections.Immutable.xml");
        //ReadFile(@"C:\codeasy\System.Reflection.Metadata.xml");
        //ReadFile(@"C:\codeasy\executor\Microsoft.CodeAnalysis.CSharp.xml");
        //ReadFile(@"C:\codeasy\executor\Microsoft.CodeAnalysis.xml");
        //ReadFile(@"C:\codeasy\executor\Newtonsoft.Json.xml");
        //ReadFile(@"C:\codeasy\executor\RestSharp.xml");
        //ReadFile(@"C:\codeasy\executor\System.Collections.Immutable.xml");
        //ReadFile(@"C:\codeasy\executor\System.Reflection.Metadata.xml");
        ReadDirectoriesList(@"C:\", 2);
    }
}