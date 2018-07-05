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
			if (_appServiceConnection == null)
			{
				_appServiceConnection = new AppServiceConnection();
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

			var res = await _appServiceConnection.SendMessageAsync(new ValueSet
			{
				["Input"] = inputTextBox.Text,
			});
			logTextBlock.Text = res.Message["Result"] as string;
		}

	}
}
