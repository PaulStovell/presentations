using System;
using System.Windows;

namespace Common.Infrastructure
{
    public class ApplicationResourcesManager : IApplicationResourcesManager
    {
        public void LoadResources(string path)
        {
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri(path, UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
