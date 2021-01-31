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

        public static void ArchiveUnprocessedFiles(string baseDir, DateTime utcDateTime)
        {
            var source = Path.Combine(baseDir, "unprocessed");
            var destination = Path.Combine("archive", utcDateTime.Year.ToString(), utcDateTime.Month.ToString(),
                utcDateTime.Day.ToString(),
                utcDateTime.Hour.ToString());
            Directory.CreateDirectory(destination);
            Directory.Move(source, destination);
        }
    }
}