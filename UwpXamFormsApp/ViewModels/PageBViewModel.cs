using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UwpXamFormsApp.Models;
using UwpXamFormsApp.Services;

namespace UwpXamFormsApp.ViewModels
{
    public class PageBViewModel : ViewModelBase, INavigationAware
	{
		RecordMeasurement _recordMeasurement;
		public RecordMeasurement RecordMeasurement
		{
			get => _recordMeasurement;
			set => SetProperty(ref _recordMeasurement, value);
		}

		public PageBViewModel(INavigationService navigationService, IViewModeService viewModeService) 
            : base (navigationService, viewModeService)
        {
            Title = "Page B";
        }

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("recordMeasurement"))
				RecordMeasurement = parameters.GetValue<RecordMeasurement>("recordMeasurement");
		}

	}
}
