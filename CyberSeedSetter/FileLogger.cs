using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CyberSeedSetter;

public sealed class FileLogger
{
    public static class LogKeys
    {
        public const string History = "history.log";
    }

    public sealed class File(string name, FileInfo info)
    {
        public string Name { get; } = name;
        public FileInfo Info { get; } = info;

        public void AppendLines(params string[] lines)
        {
            System.IO.File.AppendAllLines(Info.FullName, lines);
        }
    }

    private static readonly IEnumerable<string> defaultLogKeys =
    [
        LogKeys.History
    ];
    private readonly Dictionary<string, File> files;

    private static readonly DirectoryInfo assemblyDir = Directory.GetParent(typeof(FileLogger).Assembly.Location)!;

    public FileLogger()
    {
        files = defaultLogKeys.Select(key =>
        {
            var fileInfo = assemblyDir.GetFileInfo(key);
            if (!fileInfo.Exists)
            {
                using var _ = fileInfo.Create();
            }
            return new File(key, fileInfo);
        })
       .ToDictionary(file => file.Name);
    }

    public void Log(string message, string key)
    {
        GetFile(key).AppendLines(message);
    }

    public File GetFile(string key) => files[key];
    public bool TryGetFile(string key, out File? file) => files.TryGetValue(key, out file);
}