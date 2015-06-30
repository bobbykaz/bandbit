using Fitbit.API.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using FitBand.Developer.Secrets;

// The WebView Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace FitBand
{
    public sealed partial class MainPage : Page
    {
        private static readonly Uri LoginUri = new Uri("ms-appx-web:///Html/login.html", UriKind.Absolute);
        private static readonly Uri AuthUri = new Uri("ms-appx-web:///Html/auth.html", UriKind.Absolute);
        private static readonly Uri ErrorUri = new Uri("ms-appx-web:///Html/error.html", UriKind.Absolute);
        private static readonly Uri HomeUri = new Uri("ms-appx-web:///Html/index.html", UriKind.Absolute);
        private static string StoredRefreshToken;
        private static FitbitClient Client;

        public MainPage()
        {
            this.InitializeComponent();
            Client = new FitbitClient(Secrets.ClientID, Secrets.ClientConsumerSecret, Secrets.CallbackURI);
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StoredRefreshToken = await StoredSettings.TryLoadRefreshToken();
            if (StoredRefreshToken == null)
                WebViewControl.Navigate(LoginUri);
            else
                WebViewControl.Navigate(HomeUri);

            HardwareButtons.BackPressed += this.MainPage_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is being navigated away.
        /// </summary>
        /// <param name="e">Event data that describes how this page is navigating.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= this.MainPage_BackPressed;
        }

        /// <summary>
        /// Overrides the back button press to navigate in the WebView's back stack instead of the application's.
        /// </summary>
        private void MainPage_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (WebViewControl.CanGoBack)
            {
                WebViewControl.GoBack();
                e.Handled = true;
            }
        }

        private async void Browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                Debug.WriteLine("Navigation to this page failed, check your internet connection.");
            }

            if (args.Uri.ToString().IndexOf("ms-appx-web://") == 0 && args.Uri.ToString().Contains("auth.html"))
            {
                string authCode = args.Uri.Query.Substring(args.Uri.Query.IndexOf("code=") + 5);
                try
                {
                    var token = await Client.Authenticate(authCode, Secrets.CallbackURI);
                    await StoredSettings.SaveRefreshToken(token.refresh_token);
                }
                catch
                {
                    WebViewControl.Navigate(ErrorUri);
                }
            }
        }

        /// <summary>
        /// Navigates forward in the WebView's history.
        /// </summary>
        private void ForwardAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
            }
        }

        /// <summary>
        /// Navigates to the initial home page.
        /// </summary>
        private void HomeAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            WebViewControl.Navigate(HomeUri);
        }

        private void BeginOAuthButton_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://www.fitbit.com/oauth2/authorize?client_id=" + Secrets.ClientID
                + "&response_type=code&scope=activity&redirect_uri=" + Secrets.CallbackURI;
            WebViewControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Uri uri = new Uri(url);
            WebViewControl.Navigate(uri);
        }
    }
}
