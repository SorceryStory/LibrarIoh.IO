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
            string concatenatedPath = RemoveDirectorySeparatorAtEnd(path1) + "/" + RemoveDirectorySeparatorAtStart(path2);

            // Removing Parent and Self References
            List<string> finalPathComponents = new List<string>(concatenatedPath.Split('/'));
            for (int i = 0; i < finalPathComponents.Count;)
            {
                if (finalPathComponents[i] == "..")
                {
                    finalPathComponents.RemoveRange(i - 1, 2);
                    --i;
                }
                else if (finalPathComponents[i] == ".")
                {
                    finalPathComponents.RemoveRange(i, 1);
                }
                else
                {
                    ++i;
                }
            }

            // Merging the final string
            string finalPath = string.Empty;
            for (int i = 0; i < finalPathComponents.Count; i++)
            {
                if (i != 0)
                {
                    finalPath += "/";
                }

                finalPath += finalPathComponents[i];
            }

            return finalPath;
        }

        /// <summary>
        /// Get the extension at the start of a path.
        /// </summary>
        /// <param name="path">The path you want to get the extension from.</param>
        /// <returns>The extension the path ends in or an empty string otherwise.</returns>
        public static string GetExtension(string path)
        {
            return HasExtension(path, out int extensionInd) ? path.Substring(0, extensionInd) : path;
        }

        /// <summary>
        /// Get the last component of a path.
        /// </summary>
        /// <param name="path">The path you want to get the component from.</param>
        /// <param name="removeExtension">Remove the extension, if it exists.</param>
        /// <returns>The last component.</returns>
        public static string GetLastComponent(string path, bool removeExtension)
        {
            string finalPath = removeExtension ? RemoveExtension(path) : path;
            int directoryInd = path.LastIndexOf('/');
            return directoryInd != -1 ? finalPath.Substring(directoryInd + 1) : finalPath;
        }

        /// <summary>
        /// Remove a separator if it exists at the end of a path.
        /// </summary>
        /// <param name="path">The path you want to remove the separator from.</param>
        /// <returns>The path without the separator at the end.</returns>
        public static string RemoveDirectorySeparatorAtEnd(string path)
        {
            return (path.LastIndexOf('/') == (path.Length - 1)) ? path.Substring(0, path.Length - 1) : path;
        }

        /// <summary>
        /// Remove a separator if it exists at the start of a path.
        /// </summary>
        /// <param name="path">The path you want to remove the separator from.</param>
        /// <returns>The path without the separator at the start.</returns>
        public static string RemoveDirectorySeparatorAtStart(string path)
        {
            return (path.IndexOf('/') == 0) ? path.Substring(1, path.Length - 1) : path;
        }

        /// <summary>
        /// Remove the extension at the start of a path.
        /// </summary>
        /// <param name="path">The path you want to remove the extension from.</param>
        /// <returns>The path without the extension at the end.</returns>
        public static string RemoveExtension(string path)
        {
            return HasExtension(path, out int extensionInd) ? path.Substring(0, extensionInd) : path;
        }

        /// <summary>
        /// Remove the last component of a path.
        /// </summary>
        /// <param name="path">The path you want to remove the last component from.</param>
        /// <param name="removeSeparatorEnd">Remove the trailing separator at the end.</param>
        /// <returns>The path without the last component.</returns>
        public static string RemoveLastComponent(string path, bool removeSeparatorEnd)
        {
            int directoryInd = path.LastIndexOf('/');
            return directoryInd != -1 ? path.Substring(0, directoryInd + (removeSeparatorEnd ? 0 : 1)) : path;
        }

        /// <summary>
        /// Get the extension at the start of a path.
        /// </summary>
        /// <param name="path">The path you want to get the extension from.</param>
        /// <param name="extension">The extension of the path.</param>
        /// <returns>True if an extension is found and extracted successfully, false otherwise.</returns>
        public static bool TryGetExtension(string path, out string extension)
        {
            bool hasExtension = HasExtension(path, out int extensionInd);
            extension = hasExtension ? path.Substring(extensionInd + 1) : "";
            return hasExtension;
        }

        private static bool HasExtension(string path, out int lastExtensionSeparatorIndex)
        {
            lastExtensionSeparatorIndex = path.LastIndexOf('.');
            int lastDirectorySeparatorIndex = path.LastIndexOf('/');
            return
                lastExtensionSeparatorIndex != -1 &&
                    ((lastDirectorySeparatorIndex != -1 && lastExtensionSeparatorIndex > lastDirectorySeparatorIndex) ||
                    lastDirectorySeparatorIndex == -1);
        }

        #endregion Methods
    }
}
