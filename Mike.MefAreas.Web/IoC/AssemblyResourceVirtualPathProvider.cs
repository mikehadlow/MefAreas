using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Mike.MefAreas.Web.IoC
{
    // From http://www.codeproject.com/KB/aspnet/ASP2UserControlLibrary.aspx

    public class AssemblyResourceVirtualPathProvider : VirtualPathProvider
    {
        private readonly Dictionary<string, Assembly> nameAssemblyCache;

        public AssemblyResourceVirtualPathProvider()
        {
            nameAssemblyCache = new Dictionary<string, Assembly>(StringComparer.InvariantCultureIgnoreCase);
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            string checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/App_Resource/",
                                        StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) ||
                    base.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return new AssemblyResourceFile(nameAssemblyCache, virtualPath);
            }

            return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return null;
            }

            return base.GetCacheDependency(virtualPath,
                                           virtualPathDependencies, utcStart);
        }

        private class AssemblyResourceFile : VirtualFile
        {
            private readonly IDictionary<string, Assembly> nameAssemblyCache;
            private readonly string assemblyPath;

            public AssemblyResourceFile(IDictionary<string, Assembly> nameAssemblyCache, string virtualPath) :
                base(virtualPath)
            {
                this.nameAssemblyCache = nameAssemblyCache;
                assemblyPath = VirtualPathUtility.ToAppRelative(virtualPath);
            }

            public override Stream Open()
            {
                // ~/App_Resource/WikiExtension.dll/WikiExtension/Presentation/Views/Wiki/Index.aspx
                string[] parts = assemblyPath.Split(new[] { '/' }, 4);

                // TODO: should assert and sanitize 'parts' first
                string assemblyName = parts[2];
                string resourceName = parts[3].Replace('/', '.');

                Assembly assembly;

                lock (nameAssemblyCache)
                {
                    if (!nameAssemblyCache.TryGetValue(assemblyName, out assembly))
                    {
                        var path = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
                        assembly = Assembly.LoadFrom(path);

                        // TODO: Assert is not null
                        nameAssemblyCache[assemblyName] = assembly;
                    }
                }

                Stream resourceStream = null;

                if (assembly != null)
                {
                    resourceStream = assembly.GetManifestResourceStream(resourceName);
                }

                return resourceStream;
            }
        }
    }
}