using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpXamFormsApp.Services;
using Xamarin.Forms;
using UwpXamFormsApp.Models;

namespace UwpXamFormsApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
		protected IViewModeService ViewModeService { get; private set; }


		string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, IViewModeService viewModeService)
        {
            NavigationService = navigationService;
			ViewModeService = viewModeService;
		}

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }

		public Page GetCurrentPage()
		{
			var actionPage = App.Current.MainPage;

			if (actionPage.Navigation != null)
				actionPage = actionPage.Navigation.NavigationStack.Last();

			return actionPage;
		}

		public string GetCurrentPageName()
		{
			return GetCurrentPage().GetType().Name;
		}

		public bool IsCurrentPage
		{
			get
			{
				return GetType().Name == GetCurrentPageName();
			}
		}

		public async Task ForceNavigate(OpeningPage openingPage, RecordMeasurement recordMeasurement)
		{
			var p = new NavigationParameters
			{
				{ "recordMeasurement", recordMeasurement }
			};

			await NavigationService.NavigateAsync("NavigationPage/"+ openingPage.ToString(), p);
		}

		public void TryEnterFullScreenMode()
		{
			ViewModeService.TryEnterFullScreenMode();
		}

	}
}
