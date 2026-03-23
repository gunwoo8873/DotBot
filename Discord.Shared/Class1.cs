namespace Discord.Shared;

// public static class PathSolver
// {
//   public static string FindSolutionRoot(string markerFile = "DotBot.slnx")
//   {
//     var dir = new DirectoryInfo(AppContext.BaseDirectory);

//     while (dir is not null)
//     {
//       var markerPath = Path.Combine(dir.FullName, markerFile);
//       if (File.Exists(markerPath))
//       {
//         return dir.FullName;
//       }

//       dir = dir.Parent;
//     }

//     throw new InvalidOperationException($"Solution root not found. Marker: {markerFile}");
//   }

//   public static string ResolveFromSolutionRoot(string relativePath, string markerFile = "DotBot.slnx")
//   {
//     var root = FindSolutionRoot(markerFile);
//     return Path.Combine(root, relativePath);
//   }
// }

// public static class EnvLoader
// {
//   public static void LoadFromSolutionRelativePath(string relativePath, bool overwriteExisting = false)
//   {
//     var envPath = PathSolver.ResolveFromSolutionRoot(relativePath);
//     if (!File.Exists(envPath))
//     {
//       return;
//     }

//     foreach (var rawLine in File.ReadLines(envPath))
//     {
//       var line = rawLine.Trim();
//       if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
//       {
//         continue;
//       }

//       var separatorIndex = line.IndexOf('=');
//       if (separatorIndex <= 0)
//       {
//         continue;
//       }

//       var key = line[..separatorIndex].Trim();
//       var value = line[(separatorIndex + 1)..].Trim();

//       if ((value.StartsWith('"') && value.EndsWith('"')) || (value.StartsWith('\'') && value.EndsWith('\'')))
//       {
//         value = value[1..^1];
//       }

//       if (!overwriteExisting && !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
//       {
//         continue;
//       }

//       Environment.SetEnvironmentVariable(key, value);
//     }
//   }

//   public static string GetRequired(string key)
//   {
//     var value = Environment.GetEnvironmentVariable(key);
//     if (string.IsNullOrWhiteSpace(value))
//     {
//       throw new InvalidOperationException($"Error: {key} is not set in environment variables.");
//     }

//     return value;
//   }
// }
