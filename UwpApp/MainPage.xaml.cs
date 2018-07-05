using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace UwpApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public static MainPage Current { get; private set; }

		public MainPage()
        {
            this.InitializeComponent();
			Current = this;
		}

		async void Button_Click(object sender, RoutedEventArgs e)
		{
			await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
		}

		public async Task SetTextAsync(string text)
		{
			void setText()
			{
				textBlock.Text = text;
			}

			if (Dispatcher.HasThreadAccess)
			{
				setText();
			}
			else
			{
				await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
				{
					setText();
				});
			}
		}

	}
}
