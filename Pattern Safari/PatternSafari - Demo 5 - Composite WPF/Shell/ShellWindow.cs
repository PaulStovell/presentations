using System.Windows;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Wpf.Regions;

namespace Shell
{
    public partial class ShellWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public ShellWindow(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            RegionManager.SetRegionManager(this, regionManager);
            InitializeComponent();
        }
    }
}