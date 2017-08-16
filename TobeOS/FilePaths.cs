using System.IO;

namespace TobeOS
{
    public static class FilePaths
    {
        public static bool IsAbsolute(string path)
        {
            return path.Length >= 3 && char.IsDigit(path[0]) && path[1] == ':' && path[2] == '\\';
        }

        public static string GetAbsolute(string path, KernelState state)
        {
            if (IsAbsolute(path))
                return path;
            return Combine(state.WorkingDirectory, path);
        }

        public static string Normalize(string path)
        {
            return StripSlashes(CorrectSlashes(path));
        }

        public static string Combine(string a, string b)
        {
            return StripSlashes(a) + '\\' + StripSlashes(b);
        }

        public static string CorrectSlashes(string path)
        {
            var chars = new char[path.Length];

            for (int i = 0; i < path.Length; i++)
            {
                var ch = path[i];
                if (ch == '/') chars[i] = '\\';
                else chars[i] = path[i];
            }

            return new string(chars);
        }

        public static string StripSlashes(string path)
        {
            int start;
            string strippedFront;

            for (start = 0; start < path.Length; start++)
            {
                var ch = path[start];
                if (ch != '/' && ch != '\\') break;
            }

            if (start == 0)
                strippedFront = path;
            else strippedFront = path.Substring(start);

            int idealEnd = strippedFront.Length - 1;
            int end;

            for (end = idealEnd; end >= 0; end--)
            {
                var ch = path[start];
                if (ch != '/' && ch != '\\') break;
            }

            if (end == idealEnd)
                return strippedFront;
            return strippedFront.Substring(0, end);
        }

        public static string Expand(string path)
        {
            if (!IsAbsolute(path)) throw new InvalidDataException($"Path is not absolute: \"{path}\"");
            else if (path.Length == 3) return path;

            var absolute = path.Substring(0, 3); // Get volume
            var segments = path.Substring(3).Split('\\');

            foreach (var segment in segments)
            {
                if (segment.Trim().Length > 0)
                {
                    if (segment == "..")
                    {
                        if (!(absolute.Length == 3 && IsAbsolute(absolute)))
                        {
                            if (!Directory.Exists(absolute))
                                throw new DirectoryNotFoundException($"Directory does not exist: \"{absolute}\"");
                            absolute = Directory.GetParent(absolute).FullName;
                        }
                    }
                    else if (segment == ".")
                    {
                        continue;
                    }
                    else
                    {
                        absolute += '\\' + segment;
                    }
                }
            }

            return absolute;
        }
    }
}
