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
using System.Threading.Tasks;
using Fitbit.API.Model;

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
            StoredRefreshToken = await StoredSettings.TryLoad(StoredSettings.StoredStringValues.RefreshToken);
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

            if (RedirectedToAuthPage(args.Uri.ToString()))
            {
                if (QueryContainsAuthorizationCode(args.Uri.Query))
                {
                    var token = await CheckAuthorizationCodeResult(args);
                }
                else if(FragmentContainsAccessToken(args.Uri.Fragment)) //implicit oauth returns info in fragment and not query
                {
                    var token = await CheckImplicitResult(args.Uri.Fragment);
                }
            }
        }

        private bool RedirectedToAuthPage(string currentPageUri)
        {
            return (currentPageUri.IndexOf("ms-appx-web://") == 0 && currentPageUri.Contains("auth.html"));
        }

        private bool QueryContainsAuthorizationCode(string query)
        {
            return query.Contains("response_type=code") && query.Contains("code=");
        }

        private bool FragmentContainsAccessToken(string fragment)
        {
            return fragment.Contains("access_token=") && fragment.Contains("token_type=Bearer");
        }

        private async Task<AuthTokenResponse> CheckAuthorizationCodeResult(WebViewNavigationCompletedEventArgs args)
        {
            string authCode = args.Uri.Query.Substring(args.Uri.Query.IndexOf("code=") + 5);
            try
            {
                var token = await Client.Authenticate(authCode, Secrets.CallbackURI);
                await StoredSettings.Save(StoredSettings.StoredStringValues.RefreshToken, token.refresh_token);
                return token;
            }
            catch
            {
                WebViewControl.Navigate(ErrorUri);
                return null;
            }
        }

        private async Task<string> CheckImplicitResult(string fragment)
        {
            string tokenStart = fragment.Substring(fragment.IndexOf("access_token=") + 13);
            int tokenEnd = tokenStart.IndexOf('&');
            string token = tokenStart;
            if(tokenEnd > 0) 
                token = tokenStart.Substring(0, tokenEnd);
            try
            {
                await StoredSettings.Save(StoredSettings.StoredStringValues.ImplicitAccessToken, token);
                return token;
            }
            catch
            {
                WebViewControl.Navigate(ErrorUri);
                return null;
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
            var url = GetImplicitAuthURL();
            WebViewControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Uri uri = new Uri(url);
            WebViewControl.Navigate(uri);
        }

        private string GetAuthCodeURL()
        {
            return "https://www.fitbit.com/oauth2/authorize?client_id=" + Secrets.ClientID
                + "&response_type=code&scope=activity&redirect_uri=" + Secrets.CallbackURI;
        }

        private string GetImplicitAuthURL()
        {
            return "https://www.fitbit.com/oauth2/authorize?client_id=" + Secrets.ClientID
                + "&response_type=token&scope=activity&redirect_uri=" + Secrets.CallbackURI
                + "&expires_in=2592000&prompt=consent";
        }
    }
}
