using System;
using System.Deployment.Application;
using System.IO;
using System.Reflection;

namespace ClickOnce
{
    public class ClickOnceHelper
    {
        public enum ShortcutType
        {
            Application,
            Url
        }

        public static bool IsApplicationNetworkDeployed
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return true;
                }
                return false;
            }
        }

        public static Uri ActivationUri
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.ActivationUri;
                }
                return null;
            }
        }

        public static String StartUpUri
        {
            get
            {
                string startUpURL = String.Empty;

                if (ApplicationDeployment.IsNetworkDeployed &&
                    ApplicationDeployment.CurrentDeployment.UpdateLocation != null)
                {
                    startUpURL = ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri;

                    if (!string.IsNullOrEmpty(ApplicationDeployment.CurrentDeployment.UpdateLocation.Query))
                    {
                        startUpURL = startUpURL.Replace(ApplicationDeployment.CurrentDeployment.UpdateLocation.Query,
                                                        String.Empty);
                    }
                }

                return Uri.UnescapeDataString(startUpURL);
            }
        }

        public static string IconLocation
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                String iconLocation = assembly.Location;

                if (!string.IsNullOrEmpty(iconLocation))
                    return iconLocation.Replace('\\', '/');

                return string.Empty;
            }
        }

        public static string AssemblyProductName
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                if (!Attribute.IsDefined(assembly, typeof (AssemblyProductAttribute)))
                    return string.Empty;

                var product =
                    (AssemblyProductAttribute) Attribute.GetCustomAttribute(assembly, typeof (AssemblyProductAttribute));

                return product.Product;
            }
        }

        public static string AssemblyCompanyName
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                if (!Attribute.IsDefined(assembly, typeof (AssemblyCompanyAttribute)))
                    return string.Empty;

                var company =
                    (AssemblyCompanyAttribute) Attribute.GetCustomAttribute(assembly, typeof (AssemblyCompanyAttribute));

                return company.Company;
            }
        }

        public static string GetApplicationExecutable()
        {
            return Assembly.GetExecutingAssembly().GetName().CodeBase;
        }

        public static string GetProgramShortcut(String publisher, String product)
        {
            string allProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            string shortcutPath = Path.Combine(allProgramsPath, publisher);
            shortcutPath = Path.Combine(shortcutPath, product) + ".appref-ms";
            return shortcutPath;
        }

        public static string GetStartupShortcut(string product, ShortcutType shortcutType)
        {
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            startupPath = Path.Combine(startupPath, product);

            switch (shortcutType)
            {
                case ShortcutType.Application:
                    startupPath += ".appref-ms";
                    break;
                case ShortcutType.Url:
                    startupPath += ".url";
                    break;
            }

            return startupPath;
        }

        public static Boolean DoesStartupShortcutExist(string product, ShortcutType shortcutType)
        {
            String file = GetStartupShortcut(product, shortcutType);
            return File.Exists(file);
        }
    }
}