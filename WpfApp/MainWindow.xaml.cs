using System;
using System.Collections.Generic;
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
		}

		async void Button_Click(object sender, RoutedEventArgs e)
		{
			// AppServiceについてはここに情報がある
			// https://docs.microsoft.com/ja-jp/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service

			if (_appServiceConnection == null)
			{
				// IDisposable なのでメモリリークするので製品版では注意が必要
				_appServiceConnection = new AppServiceConnection();
				
				// Here, we use the app service name defined in the app service provider's Package.appxmanifest file in the <Extension> section.
				_appServiceConnection.AppServiceName = "InProcessAppService";
				_appServiceConnection.PackageFamilyName = Package.Current.Id.FamilyName;
				var r = await _appServiceConnection.OpenAsync();
				if (r != AppServiceConnectionStatus.Success)
				{
					MessageBox.Show($"Failed: {r}");
					_appServiceConnection = null;
					return;
				}
			}

			var sample = new RecordMeasurement
			{
				Guid = Guid.NewGuid(),
				CardNo = "CardNo",
				RecordNo = "RecordNo",
				MeasuredAt = DateTimeOffset.Now,
				Result1 = "Result1",
				Result2 = "Result2",
				Result3 = "Result3",
				Result4 = "Result4",
				Result5 = "Result5",
			};

			var serialized = JsonConvert.SerializeObject(sample);

			var res = await _appServiceConnection.SendMessageAsync(new ValueSet
			{
				["RecordMeasurement"] = serialized,
			});

			logTextBlock.Text = res.Message["Result"] as string;
		}

	}
}
