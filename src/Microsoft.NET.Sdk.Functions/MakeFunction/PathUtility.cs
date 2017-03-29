﻿using System;
using System.IO;

namespace MakeFunctionJson
{
    internal class PathUtility
    {
        internal static string MakeRelativePath(string fromPath, string toPath)
        {
            if (!fromPath.EndsWith(@"\"))
            {
                fromPath += @"\";
            }

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

#if NET46
            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
#else
            if (toUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
#endif
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}