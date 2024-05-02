using Emerson.Edge.Watchit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Emerson.Edge.Watchit
{
    /// <summary>
    /// Simple app demonstrating how to use DeltaV Edge Environment
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationToken ct = new CancellationToken();
        string authUrl = "api/v1/Login/GetAuthToken/profile";
        HttpClient client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<History> GetHistoryAsync()
        {
            var response = await client.GetAsync($"api/v1/history?path={ProviderPrefixTextBox.Text}/{PathTextBox.Text}", ct).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<History>(result);
        }

        private void CreateHttpClient()
        {
            if (client != null) return;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, policyErrors) => { return true; };
            client = new(handler);
            client.BaseAddress = new Uri($"https://{IpAddressTextBox.Text}/edge/");
        }
        private async Task<TokenResponse> AuthenticateAsync()
        {
            //get auth token
            var content = new[]
            {
                    new KeyValuePair<string, string>("Username", UsernameTextBox.Text),
                    new KeyValuePair<string, string>("Password", PasswordBox.Password)
            };

            var response = await client.PostAsync(authUrl, new FormUrlEncodedContent(content), ct).ConfigureAwait(false);

            // Parse the response body
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(result);
            
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CreateHttpClient();
            var auth =  AuthenticateAsync().Result;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

            History history = GetHistoryAsync().Result;
            if (history != null) 
            {
                LatestValueTextBlock.Text = $"Latest Value : {history.FieldHistory.FieldValue[0].Value}";
                HistoryDataGrid.ItemsSource = history.FieldHistory.FieldValue;
            }
            
        }
    }
}
