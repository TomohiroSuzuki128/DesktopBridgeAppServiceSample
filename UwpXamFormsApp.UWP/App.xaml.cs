using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
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
using UwpXamFormsApp.Extensions;
using UwpXamFormsApp.Services;
using UwpXamFormsApp.Models;
using Newtonsoft.Json;

namespace UwpXamFormsApp.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
	{
		public static new App Current => (App)Application.Current;

		AppServiceConnection _appServiceConnection;
		BackgroundTaskDeferral _appServiceDeferral;

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {


            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Xamarin.Forms.Forms.Init(e);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

		protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
		{
			base.OnBackgroundActivated(args);

			if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails appService)
			{
				// 必要な処理が終了する前に、AppServiceのアクティブ化が終了しないように
				// これから非同期処理を行うので、その完了報告を待つようにとシステムに知らせる。
				_appServiceDeferral = args.TaskInstance.GetDeferral();

				args.TaskInstance.Canceled += TaskInstance_Canceled;
				_appServiceConnection = appService.AppServiceConnection;
				_appServiceConnection.RequestReceived += AppServiceConnection_RequestReceived;
				_appServiceConnection.ServiceClosed += AppServiceConnection_ServiceClosed;
			}
		}

		void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
		{
			// システムに完了を通知する
			_appServiceDeferral?.Complete();
		}

		// WPFアプリからの指示の受信
		async void AppServiceConnection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
		{
			var deferral = args.GetDeferral();

			var message = args.Request.Message;

			// WPFから受け取るメッセージに処理の種別と内容を設定してあるので
			// 種別に応じて処理を行う
			if ((string)message["Operation"] == "Data")
				await ProcessNortifyInformation(message);
			else if ((string)message["Operation"] == "FullScreen")
				await ProcessFullScreen();

			// ホスト側より応答確認送信する
			await args.Request.SendResponseAsync(new ValueSet
			{
				["Result"] = $"Accept: {DateTime.Now}"
			});

			// システムに完了を通知する
			deferral.Complete();
		}

		void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
		{
			// システムに完了を通知する
			_appServiceDeferral?.Complete();
		}

		// フルスクリーンの処理の実体はViewModelにDIされたサービスで行うため、
		// 拡張メソッド経由でViewModelにアクセスして実行。
		// 同様にパラーメータ付きのページ遷移も
		// 拡張メソッド経由でViewModelにアクセスして実行。
		async Task ProcessNortifyInformation(ValueSet message)
		{
			var serialized = message["SampleRecord"] as string;
			var deserialized = JsonConvert.DeserializeObject<SampleRecord>(serialized);

			var coreWindow = Windows.ApplicationModel.Core.CoreApplication.MainView;
			var dispatcher = coreWindow.CoreWindow.Dispatcher;
			await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
			{
				var app = UwpXamFormsApp.App.Current;
				var navigationPage = (Xamarin.Forms.NavigationPage)app.MainPage;
				var contentPage = (Xamarin.Forms.ContentPage)navigationPage.CurrentPage;
				contentPage.TryEnterFullScreenMode();
				contentPage.PerformNavigateCommand(OpeningPage.PageB, deserialized);
			});
		}

		async Task ProcessMinimasize()
		{
			var coreWindow = Windows.ApplicationModel.Core.CoreApplication.MainView;
			var dispatcher = coreWindow.CoreWindow.Dispatcher;
			await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
			{
				var app = UwpXamFormsApp.App.Current;
				var navigationPage = (Xamarin.Forms.NavigationPage)app.MainPage;
				var contentPage = (Xamarin.Forms.ContentPage)navigationPage.CurrentPage;
				contentPage.TryEnterFullScreenMode();
			});
		}

		// フルスクリーンの処理の実体はViewModelにDIされたサービスで行うため、
		// 拡張メソッド経由でViewModelにアクセスして実行。
		async Task ProcessFullScreen()
		{
			var coreWindow = Windows.ApplicationModel.Core.CoreApplication.MainView;
			var dispatcher = coreWindow.CoreWindow.Dispatcher;
			await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
			{
				var app = UwpXamFormsApp.App.Current;
				var navigationPage = (Xamarin.Forms.NavigationPage)app.MainPage;
				var contentPage = (Xamarin.Forms.ContentPage)navigationPage.CurrentPage;
				contentPage.TryEnterFullScreenMode();
			});
		}

		public async Task SendTextAsync()
		{
			Debug.WriteLine("UWP : App : SendTextAsync()");

			if (_appServiceConnection == null)
			{
				Debug.WriteLine("UWP : App : SendTextAsync()  _appServiceConnection == null");
				return;
			}
			else
			{
				Debug.WriteLine("UWP : App : SendTextAsync()  _appServiceConnection != null");
			}

			await _appServiceConnection.SendMessageAsync(new ValueSet
			{
				["Text"] = "UWPからデータが送られました。",
			});
			Debug.WriteLine("UWP : App : SendMessageAsync()");

		}

	}
}
