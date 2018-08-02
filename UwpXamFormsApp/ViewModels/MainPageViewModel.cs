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
		IWpfInteractionService WpfInteractionService { get; }

		public ICommand LaunchCommand { get; }

		public MainPageViewModel(
			INavigationService navigationService,
			IDataShareService dataShareService,
			IWpfInteractionService wpfInteractionService,
			IViewModeService viewModeService
			)
			: base (navigationService, viewModeService)
        {
            Title = "Main Page";
			DataShareService = dataShareService;
			WpfInteractionService = wpfInteractionService;

			LaunchCommand = new DelegateCommand(async () =>
			{
				WpfInteractionService.LaunchWpfApp();

				await System.Threading.Tasks.Task.Delay(500);

				viewModeService.ExitFullScreenMode();
			}, () => true);

		}

	}
}
