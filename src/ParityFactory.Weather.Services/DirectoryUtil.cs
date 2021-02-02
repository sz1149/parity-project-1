using System;
using System.IO;

namespace ParityFactory.Weather.Services
{
    public static class DirectoryUtil
    {
        public static string GetUnprocessedDirectoryName(string baseDir)
        {
            var directory = Path.Combine(baseDir, "unprocessed");
            Directory.CreateDirectory(directory);
            return directory;
        }

        public static string[] GetAllUnprocessedFiles(string baseDir)
        {
            var directory = GetUnprocessedDirectoryName(baseDir);
            return Directory.GetFileSystemEntries(directory);
        }

        public static void ArchiveFiles(string baseDir, string[] filesToArchive, DateTime utcDateTime)
        {
            var destination = Path.Combine(baseDir, "archive", utcDateTime.Year.ToString(), utcDateTime.Month.ToString(),
                utcDateTime.Day.ToString(),
                utcDateTime.Hour.ToString());
            Directory.CreateDirectory(destination);
            foreach (var file in filesToArchive)
            {
                var fileInfo = new FileInfo(file);
                File.Move(file, Path.Combine(destination, fileInfo.Name));
            }
        }
    }
}