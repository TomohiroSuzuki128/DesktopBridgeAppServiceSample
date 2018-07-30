﻿using Prism;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UwpXamFormsApp.Services;
using UwpXamFormsApp.UWP.Services;
using Windows.UI.ViewManagement;
using UwpXamFormsApp.UWP.Extensions;

namespace UwpXamFormsApp.UWP
{
	public sealed partial class MainPage
	{
		public MainPage()
		{
			this.InitializeComponent();

			LoadApplication(new UwpXamFormsApp.App(new UwpInitializer()));

			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
		}
	}

	public class UwpInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IDataShareService, UwpDataShareService>();
			containerRegistry.RegisterSingleton<IWpfLaunchService, UwpWpfLaunchService>();
			containerRegistry.RegisterSingleton<IViewModeService, UwpViewModeService>();
			containerRegistry.RegisterSingleton<IRealmService, RealmService>();
		}
	}

}
