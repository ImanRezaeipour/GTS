using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using NHibernate.Cfg;

namespace GTS.Clock.Infrastructure.NHibernateFramework
{
    public class NhibernateCfgFileCache
    {
        private readonly string _cacheFile;
        private readonly string _definitionsAssembly;

        private bool isNotWeb
        {
            get
            {
                return HttpContext.Current != null;
            }
        }

        public NhibernateCfgFileCache(string definitionsAssembly)
        {
            _definitionsAssembly = definitionsAssembly;
            _cacheFile = "nh.cfg";
            if (isNotWeb) //for the web apps
                _cacheFile = HttpContext.Current.Server.MapPath(
                                string.Format("~/App_Data/{0}", _cacheFile)
                                );
        }

        public void DeleteCacheFile()
        {
            if (File.Exists(_cacheFile))
                File.Delete(_cacheFile);
        }

        public bool IsConfigurationFileValid
        {
            get
            {
                if (!File.Exists(_cacheFile))
                    return false;
                var configInfo = new FileInfo(_cacheFile);
                var asmInfo =
                    !isNotWeb
                    ? new FileInfo(
                        _definitionsAssembly +".dll")
                    : new FileInfo(
                        HttpContext.Current.Server.MapPath(string.Format("~/Bin/{0}.dll", _definitionsAssembly)))
                    ;

                if (configInfo.Length < 5 * 1024)
                    return false;

                return configInfo.LastWriteTime >= asmInfo.LastWriteTime;
            }
        }

        public void SaveConfigurationToFile(Configuration configuration)
        {
            using (var file = File.Open(_cacheFile, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, configuration);
            }
        }

        public Configuration LoadConfigurationFromFile()
        {
            if (!IsConfigurationFileValid)
                return null;

            using (var file = File.Open(_cacheFile, FileMode.Open, FileAccess.Read))
            {
                var bf = new BinaryFormatter();
                return bf.Deserialize(file) as Configuration;
            }
        }

        public string CfgFileFullPath
        {
            get
            {
                return _cacheFile;
            }
        }
    }
}
