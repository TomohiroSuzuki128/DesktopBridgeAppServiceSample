using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using UwpXamFormsApp.ViewModels;
using UwpXamFormsApp.Services;

namespace UwpXamFormsApp.Extensions
{
	public static class ContentPageExtension
	{
		public static async void PerformNavigateCommand(this ContentPage page, OpeningPage openingPage)
		{
			var viewModelBase = (ViewModelBase)page.BindingContext;
			await viewModelBase.ForceNavigate(openingPage);
		}

		public static Page GetCuurentPage(this ContentPage page)
		{
			var viewModelBase = (ViewModelBase)page.BindingContext;
			return viewModelBase.GetCurrentPage();
		}

	}
}
