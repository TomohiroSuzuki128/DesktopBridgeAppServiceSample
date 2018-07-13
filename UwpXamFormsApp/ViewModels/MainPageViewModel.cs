using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UwpXamFormsApp.Services;

namespace UwpXamFormsApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{

		IDataShareService DataShareService { get; }
		IWpfLaunchService WpfLaunchService { get; }

		public ICommand LaunchCommand { get; }

		public MainPageViewModel(
			INavigationService navigationService,
			IDataShareService dataShareService,
			IWpfLaunchService wpfLaunchService,
			IViewModeService viewModeService
			)
			: base (navigationService, viewModeService)
        {
            Title = "Main Page";
			DataShareService = dataShareService;
			WpfLaunchService = wpfLaunchService;

			LaunchCommand = new DelegateCommand(async () =>
			{
				//wpfLaunchService.LaunchWpfApp();

				await System.Threading.Tasks.Task.Delay(500);

				viewModeService.ExitFullScreenMode();
			}, () => true);

		}

	}
}
