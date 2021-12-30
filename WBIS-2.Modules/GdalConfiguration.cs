using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WBIS_2.Modules
{
    public static class GdalConfiguration
    {
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDefaultDllDirectories(uint directoryFlags);
        //               LOAD_LIBRARY_SEARCH_USER_DIRS | LOAD_LIBRARY_SEARCH_SYSTEM32
        private const uint DllSearchFlags = 0x00000400 | 0x00000800;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddDllDirectory(string lpPathName);

        private static bool _init;

        static GdalConfiguration()
        {
            Debug.WriteLine("GdalConfiguration init");
            if (_init)
            {
                return; ;
            }
            string executingAssemblyFile = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            string executingDirectory = Path.GetDirectoryName(executingAssemblyFile);
            Debug.WriteLine(executingAssemblyFile);
            Debug.WriteLine(executingDirectory);
            if (string.IsNullOrEmpty(executingDirectory))
            {
                throw new DirectoryNotFoundException(executingDirectory);
            }

            string root = Path.Combine(executingDirectory, "gdal");
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(root);
            }
            string gdal = Path.Combine(root, "gdal");
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(gdal);
            }

            string csharp = Path.Combine(gdal, "csharp");
            if (!Directory.Exists(csharp))
            {
                throw new DirectoryNotFoundException(csharp);
            }
            AddDllDirectory(csharp);
            SetDefaultDllDirectories(DllSearchFlags);
            AddDllDirectory(root);

            string proj7 = Path.Combine(root, "proj7");
            if (!Directory.Exists(proj7))
            {
                throw new DirectoryNotFoundException(proj7);
            }

            string proj7share = Path.Combine(proj7, "share");
            if (!Directory.Exists(proj7share))
            {
                throw new DirectoryNotFoundException(proj7share);
            }
            Environment.SetEnvironmentVariable("PROJ_LIB", proj7share);

            OSGeo.OSR.Osr.SetPROJSearchPath(proj7share);
            _init = true;
        }
        public static void ConfigureGdal()
        {

        }
    }
}
