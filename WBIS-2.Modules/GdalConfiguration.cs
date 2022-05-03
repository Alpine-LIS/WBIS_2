using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using OSGeo.GDAL;
using OSGeo.OGR;
using Driver = OSGeo.GDAL.Driver;

namespace WBIS_2.Modules
{
    public static class GdalConfigurationRMS
    {

        private static volatile bool _configuredOgr;
        private static volatile bool _configuredGdal;
        private static volatile bool _usable;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDefaultDllDirectories(uint directoryFlags);

        //               LOAD_LIBRARY_SEARCH_USER_DIRS | LOAD_LIBRARY_SEARCH_SYSTEM32
        private const uint DllSearchFlags = 0x00000400 | 0x00000800;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddDllDirectory(string lpPathName);


        /// <summary>
        /// Construction of Gdal/Ogr
        /// </summary>
        static GdalConfigurationRMS()
        {

            try
            {
                if (!IsWindows)
                {
                    const string notSet = "_Not_set_";
                    string tmp = Gdal.GetConfigOption("GDAL_DATA", notSet);
                    _usable = tmp != notSet;
                    return;
                }

                string executingAssemblyFile = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
                string executingDirectory = Path.GetDirectoryName(executingAssemblyFile);

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
                var gdalWrapDll = Path.Combine(csharp, "gdal_wrap.dll");
                if (!File.Exists(gdalWrapDll))
                {
                    throw new FileNotFoundException(gdalWrapDll);
                }

                string plugins = Path.Combine(gdal, "plugins");
                if (!Directory.Exists(plugins))
                {
                    throw new DirectoryNotFoundException(plugins);
                }

                AddDllDirectory(plugins);
                Environment.SetEnvironmentVariable("GDAL_DRIVER_PATH", plugins);
                Gdal.SetConfigOption("GDAL_DRIVER_PATH", plugins);

                // Set the additional GDAL environment variables.
                string gdalData = Path.Combine(root, "gdal-data");
                if (!Directory.Exists(gdalData))
                {
                    throw new DirectoryNotFoundException(gdalData);
                }

                Environment.SetEnvironmentVariable("GDAL_DATA", gdalData);
                Gdal.SetConfigOption("GDAL_DATA", gdalData);

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
                Gdal.SetConfigOption("PROJ_LIB", proj7share);
                OSGeo.OSR.Osr.SetPROJSearchPath(proj7share);


                /*
                Environment.SetEnvironmentVariable("GEOTIFF_CSV", gdalData);
                Gdal.SetConfigOption("GEOTIFF_CSV", gdalData);
                */


#if DEBUG
                Gdal.SetConfigOption("CPL_LOG",
                    Path.Combine(Path.GetTempPath(), $"gdal_{DateTime.Now:yyyy.MM.dd.HH.mm.ss}.log"));
                Gdal.SetConfigOption("CPL_DEBUG", "ON");
                Gdal.SetConfigOption("CPL_LOG_ERRORS", "ON");
                Gdal.SetConfigOption("CPL_TIMESTAMP ", "ON");
                Gdal.SetConfigOption("PROJ_DEBUG", "3");
#endif

                _usable = true;
            }
            catch (Exception e)
            {
                _usable = false;
                //LogHelper.Instance.LogError(e, "error");
            }
        }

        /// <summary>
        /// Gets a value indicating if the GDAL package is set up properly.
        /// </summary>
        public static bool Usable => _usable;

        /// <summary>
        /// Function to determine which platform we're on
        /// </summary>
        private static string GetPlatform()
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
        }

        /// <summary>
        /// Gets a value indicating if we are on a windows platform
        /// </summary>
        private static bool IsWindows
        {
            get
            {
                var res = !(Environment.OSVersion.Platform == PlatformID.Unix ||
                            Environment.OSVersion.Platform == PlatformID.MacOSX);

                return res;
            }
        }

        #region Drivers

        //private class SupportedFormat : ISupportedFormat
        //{
        //    public string Name { get; set; }
        //    public string Description { get; set; }
        //    public string Extensions { get; set; }

        //    public IEnumerable<string> ParseExtensions()
        //    {
        //        foreach (var ext in Extensions.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            yield return $"*.{ext}";
        //        }
        //    }
        //}

        //private static readonly List<SupportedFormat> _supportedRasterFormats = new List<SupportedFormat>();
        //public static IReadOnlyList<ISupportedFormat> SupportedRasterFormats => _supportedRasterFormats;

        //private static readonly List<SupportedFormat> _supportedVectorFormats = new List<SupportedFormat>();
        //public static IReadOnlyList<ISupportedFormat> SupportedVectorFormats => _supportedVectorFormats;

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        public static void ConfigureFull()
        {
            if (!_usable) return;

            if (!_configuredOgr)
            {
                // Register drivers
                Ogr.RegisterAll();
                _configuredOgr = true;
            }

            if (!_configuredGdal)
            {
                // Register drivers
                Gdal.AllRegister();
                _configuredGdal = true;
            }
        }

        #endregion Drivers
    }
}
