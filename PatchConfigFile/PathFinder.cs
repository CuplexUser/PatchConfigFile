using System;
using System.IO;
using System.Management;

namespace PatchConfigFile
{
    public static class PathFinder
    {
        /// <summary>
        /// Given a path will extract just the drive letter with volume separator.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>C:</returns>
        public static string GetDriveLetter(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("The path argument was null or whitespace.");
            }

            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException($"The path '{path}' was not a rooted path and GetDriveLetter does not support relative paths.");
            }

            if (path.StartsWith(@"\\"))
            {
                throw new ArgumentException("A UNC path was passed to GetDriveLetter");
            }

            return Directory.GetDirectoryRoot(path).Replace(Path.DirectorySeparatorChar.ToString(), "");
        }

        public static string ResolveToRootUnc(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("The path argument was null or whitespace.");
            }

            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException(
                    $"The path '{path}' was not a rooted path and ResolveToRootUNC does not support relative paths."
                );
            }

            if (path.StartsWith(@"\\"))
            {
                return Directory.GetDirectoryRoot(path);
            }

            // Get just the drive letter for WMI call
            string driveletter = GetDriveLetter(path);

            // Query WMI if the drive letter is a network drive, and if so the UNC path for it
            using (ManagementObject mo = new ManagementObject())
            {
                mo.Path = new ManagementPath($"Win32_LogicalDisk='{driveletter}'");

                DriveType driveType = (DriveType)((uint)mo["DriveType"]);
                string networkRoot = Convert.ToString(mo["ProviderName"]);

                if (driveType == DriveType.Network)
                {
                    return networkRoot;
                }

                return driveletter + Path.DirectorySeparatorChar;
            }
        }
    }
}