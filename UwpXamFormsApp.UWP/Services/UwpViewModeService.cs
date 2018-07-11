using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Services;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace UwpXamFormsApp.UWP.Services
{
	public class UwpViewModeService : IViewModeService
	{
		public void ExitFullScreenMode()
		{
			ApplicationView.GetForCurrentView().ExitFullScreenMode();
		}

	}
}
