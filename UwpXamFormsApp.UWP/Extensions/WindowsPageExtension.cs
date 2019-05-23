using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

namespace UwpXamFormsApp.UWP.Extensions
{
    public static class WindowsPageExtension
    {
        public static void TryEnterFullScreenMode(this WindowsPage page)
        {
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }
    }
}
