using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpXamFormsApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
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
	
		public string GetCurrentPage()
		{
			var actionPage = App.Current.MainPage;

			if (actionPage.Navigation != null)
				actionPage = actionPage.Navigation.NavigationStack.Last();

			return actionPage.GetType().Name;
		}

		public bool IsCurrentPage
		{
			get
			{
				return GetType().Name == GetCurrentPage();
			}
		}

		public async Task ForceNavigate()
		{
			await NavigationService.NavigateAsync("NavigationPage/PageB");
		}

	}
}
