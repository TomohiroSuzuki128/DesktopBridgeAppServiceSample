using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Services;

namespace UwpXamFormsApp.UWP.Services
{
	public class UwpWpfLaunchService : IWpfLaunchService
	{
		public async void LaunchWpfApp()
		{
			await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
		}
	}
}
