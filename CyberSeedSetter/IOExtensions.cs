using System.IO;

namespace CyberSeedSetter;

public static class IOExtensions
{
    extension(DirectoryInfo dirInfo)
    {
        public FileInfo GetFileInfo(string fileName)
        {
            var fullPath = Path.Combine(dirInfo.FullName, fileName);
            return new FileInfo(fullPath);
        }
    }

    extension(FileInfo fileInfo)
    {
        public void Create(bool overwrite = false)
        {
            if (fileInfo.Exists && overwrite) fileInfo.Delete();
            using var _ = fileInfo.Create();
        }
    }
}