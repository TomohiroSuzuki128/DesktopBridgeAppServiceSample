﻿using Prism.Commands;
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
		IRealmService realmService;
		RecordMeasurement recordMeasurement;
		public RecordMeasurement RecordMeasurement
		{
			get => recordMeasurement;
			set => SetProperty(ref recordMeasurement, value);
		}

		public PageBViewModel(
			INavigationService navigationService, 
			IViewModeService viewModeService,
			IRealmService realmService
			) 
            : base (navigationService, viewModeService)
        {
            Title = "Page B";
			this.realmService = realmService;
		}

		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			RecordMeasurement record = null;
			if (parameters.ContainsKey("recordMeasurement"))
				record = parameters.GetValue<RecordMeasurement>("recordMeasurement");

			// 実験のためわざと一度Realmに保存してから読み出ししている。
			if (record != null)
				realmService.SaveRecordMeasurement(record);

			recordMeasurement = realmService.ReadRecordMeasurement(record.Guid);
		}

	}
}
