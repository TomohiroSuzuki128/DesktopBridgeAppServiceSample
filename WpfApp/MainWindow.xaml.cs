using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        AppServiceConnection _appServiceConnection;

        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;
            Topmost = true;
        }

        // AppServiceについてはここに情報がある
        // https://docs.microsoft.com/ja-jp/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service
        async Task<bool> ConnectAsync()
        {
            if (_appServiceConnection != null)
                return true;

            var appServiceConnection = new AppServiceConnection
            {
                AppServiceName = "InProcessAppService",
                PackageFamilyName = Package.Current.Id.FamilyName,
            };
            appServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;

            var r = await appServiceConnection.OpenAsync() == AppServiceConnectionStatus.Success;

            if (r)
                _appServiceConnection = appServiceConnection;

            return r;
        }


        async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!(await ConnectAsync()))
            {
                MessageBox.Show($"Failed");
                return;
            }

            var res0 = await _appServiceConnection.SendMessageAsync(new ValueSet
            {
                ["Operation"] = "FullScreen",
            });

            await System.Threading.Tasks.Task.Delay(500);

            WindowState = WindowState.Minimized;
            WindowStyle = WindowStyle.None;
            Topmost = false;

            var sample = new SampleRecord
            {
                Guid = Guid.NewGuid().ToString(),
                Data1 = "Data1",
                Data2 = "Data2",
                Data3 = "Data3",
                Data4 = "Data4",
                Data5 = "Data5",
            };

            var serialized = JsonConvert.SerializeObject(sample);

            var res1 = await _appServiceConnection.SendMessageAsync(new ValueSet
            {
                ["Operation"] = "Data",
                ["SampleRecord"] = serialized,
            });

            //logTextBlock.Text = res1.Message["Result"] as string;
        }


        void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            void setText()
            {
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                Topmost = true;

                uwpTextBlock.Text = (string)args.Request.Message["Text"];
            }

            if (Dispatcher.CheckAccess())
                setText();
            else
                Dispatcher.Invoke(() => setText());
        }

        protected async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ConnectAsync();
        }

    }
}
