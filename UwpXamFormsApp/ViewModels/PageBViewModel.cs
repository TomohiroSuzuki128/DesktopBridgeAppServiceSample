using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UwpXamFormsApp.ViewModels
{
    public class PageBViewModel : ViewModelBase
    {
        public PageBViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Page B";
        }
    }
}
