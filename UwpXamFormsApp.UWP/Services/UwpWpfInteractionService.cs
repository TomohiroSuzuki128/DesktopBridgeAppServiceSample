using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Services;
using UwpXamFormsApp.UWP;

namespace UwpXamFormsApp.UWP.Services
{
    public class UwpWpfInteractionService : IWpfInteractionService
    {
        public async void LaunchWpfApp()
        {
            await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
        }

        public async Task SendTextAsync()
        {
            Debug.WriteLine("UWP : UwpWpfInteractionService : SendTextAsync()");
            await App.Current.SendTextAsync();
        }
    }
}
