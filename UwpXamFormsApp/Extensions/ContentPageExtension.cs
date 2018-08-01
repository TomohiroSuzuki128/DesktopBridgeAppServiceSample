using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using UwpXamFormsApp.ViewModels;
using UwpXamFormsApp.Services;
using UwpXamFormsApp.Models;

namespace UwpXamFormsApp.Extensions
{
	/// <summary>
	/// ContentPageExtension
	/// </summary>
	/// <remarks>
	/// ContentPageは組込みのクラスのため、拡張メソッドで機能を追加する
	/// </remarks>
	public static class ContentPageExtension
	{
		public static async void PerformNavigateCommand(this ContentPage page, OpeningPage openingPage, RecordMeasurement recordMeasurement)
		{
			var viewModelBase = (ViewModelBase)page.BindingContext;
			await viewModelBase.ForceNavigate(openingPage, recordMeasurement);
		}

		public static Page GetCuurentPage(this ContentPage page)
		{
			var viewModelBase = (ViewModelBase)page.BindingContext;
			return viewModelBase.GetCurrentPage();
		}

		public static void TryEnterFullScreenMode(this ContentPage page)
		{
			var viewModelBase = (ViewModelBase)page.BindingContext;
			viewModelBase.TryEnterFullScreenMode();
		}

	}
}
