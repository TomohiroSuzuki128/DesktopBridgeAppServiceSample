using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UwpXamFormsApp.Models;
using UwpXamFormsApp.Services;

namespace UwpXamFormsApp.ViewModels
{
    public class PageBViewModel : ViewModelBase, INavigationAware
	{
		IWpfInteractionService WpfInteractionService { get; }
		IRealmService RealmService { get; }

		public ICommand SendTextCommand { get; }

		public RecordMeasurement RecordMeasurement
		{
			get => recordMeasurement;
			set => SetProperty(ref recordMeasurement, value);
		}
		RecordMeasurement recordMeasurement;

		public PageBViewModel(
			INavigationService navigationService, 
			IViewModeService viewModeService,
			IWpfInteractionService wpfInteractionService,
			IRealmService realmService
			) 
            : base (navigationService, viewModeService)
        {
            Title = "Page B";
			RealmService = realmService;
			WpfInteractionService = wpfInteractionService;

			SendTextCommand = new DelegateCommand(async () =>
			{
				await WpfInteractionService.SendTextAsync();

				viewModeService.ExitFullScreenMode();
			}, () => true);
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			Debug.WriteLine("Page B : OnNavigatingTo start.");
			RecordMeasurement record = null;
			if (parameters.ContainsKey("recordMeasurement"))
				record = parameters.GetValue<RecordMeasurement>("recordMeasurement");

			// 実験のためわざと一度Realmに保存してから読み出ししている。
			if (record != null)
				RealmService.SaveRecordMeasurement(record);

			RecordMeasurement = RealmService.FindRecordMeasurement(record.Guid);

			Debug.WriteLine("Page B : OnNavigatingTo finish.");

			foreach (var item in RealmService.AllRecordMeasurements())
			{
				Debug.WriteLine("GUID : " + item.Guid + " RecordNo : " + item.RecordNo);
			}

		}

	}
}
