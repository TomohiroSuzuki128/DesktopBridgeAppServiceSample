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
			var currentView = ApplicationView.GetForCurrentView();
			currentView.ExitFullScreenMode();
			var success = currentView.TryResizeView(new Size { Width = currentView.VisibleBounds.Width, Height = currentView.VisibleBounds.Height });
		}

		public void TryEnterFullScreenMode()
		{
			var currentView = ApplicationView.GetForCurrentView();
			currentView.TryResizeView(new Size { Width = currentView.VisibleBounds.Width, Height = currentView.VisibleBounds.Height });
			var success = currentView.TryEnterFullScreenMode();
			System.Diagnostics.Debug.WriteLine(success);
		}

	}
}
