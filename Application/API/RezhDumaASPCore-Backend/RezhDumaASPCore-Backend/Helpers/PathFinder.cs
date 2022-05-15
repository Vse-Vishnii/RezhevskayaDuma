using System;
using System.IO;
using System.Linq;

namespace RezhDumaASPCore_Backend.Helpers
{
    public static class PathFinder
    {
        public static string GetConnectionPath()
        {
            var directory = Environment.CurrentDirectory.Split("\\").Last();
            if (directory == "RezhDumaASPCore-Backend")
                return "csdev";
            if (directory == "netcoreapp3.1")
                return "cs";
            throw new Exception("Папка запуска неверна");
        }
    }
}
