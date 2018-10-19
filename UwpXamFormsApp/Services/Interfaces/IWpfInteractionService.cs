using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UwpXamFormsApp.Services
{
    public interface IWpfInteractionService
    {
        void LaunchWpfApp();

        Task SendTextAsync();
    }

}
