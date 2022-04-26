using System.Collections.Generic;

namespace LibrarIoh.IO
{
    /// <summary>
    /// A class that houses some common path operations.
    /// </summary>
    public class PathOperations
    {
        #region Methods

        /// <summary>
        /// Combine two paths into a single string.
        /// </summary>
        /// <param name="path1">The path you want to add to.</param>
        /// <param name="path2">The path you want to add.</param>
        /// <returns>The combined path as a single string.</returns>
        public static string Combine(string path1, string path2)
        {
            string concatenatedPath = RemoveFile(path1) + path2;

            // Removing Parent and Self References
            List<string> finalPathElements = new List<string>(concatenatedPath.Split('/'));
            for (int i = 0; i < finalPathElements.Count;)
            {
                if (finalPathElements[i] == "..")
                {
                    finalPathElements.RemoveRange(i - 1, 2);
                    --i;
                }
                else if (finalPathElements[i] == ".")
                {
                    finalPathElements.RemoveRange(i, 1);
                }
                else
                {
                    ++i;
                }
            }

            // Merging the final string
            string finalPath = string.Empty;
            for (int i = 0; i < finalPathElements.Count; i++)
            {
                if (i != 0)
                {
                    finalPath += "/";
                }

                finalPath += finalPathElements[i];
            }

            return finalPath;
        }

        /// <summary>
        /// Get the filename section of a path.
        /// </summary>
        /// <param name="path">The path you want to get the filename from.</param>
        /// <returns>The filename without the extension.</returns>
        public static string GetFileName(string path)
        {
            string pathWithoutExtension = RemoveExtension(path);
            int index = path.LastIndexOf('/');
            return index == -1 ? pathWithoutExtension : pathWithoutExtension.Substring(index + 1);
        }

        /// <summary>
        /// Remove the extension at the end of a path.
        /// </summary>
        /// <param name="path">The path you want to remove the extension from.</param>
        /// <returns>The path without the extension at the end.</returns>
        public static string RemoveExtension(string path)
        {
            int index = path.LastIndexOf('.');
            return index == -1 ? path : path.Substring(0, index);
        }

        /// <summary>
        /// Remove the file section of a path.
        /// </summary>
        /// <param name="path">The path you want to remove extension from.</param>
        /// <returns>The path without the last section.</returns>
        public static string RemoveFile(string path)
        {
            int index = path.LastIndexOf('/');
            return index == -1 ? path : path.Substring(0, index + 1);
        }

        #endregion Methods
    }
}
