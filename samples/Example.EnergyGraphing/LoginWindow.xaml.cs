using LiveCharts;
using LiveCharts.Configurations;
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
using WifiPlug.Api;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;

namespace Example.EnergyGraphing
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow() {
            InitializeComponent();
            
            var mapper = Mappers.Xy<HistoricalEnergyReadingEntity>()
                .X(model => model.Timestamp.Ticks)
                .Y(model => Math.Round(model.Power, 4));

            // lets save the mapper globally.
            Charting.For<HistoricalEnergyReadingEntity>(mapper);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e) {
            // validation
            if (txtAccessToken.Text.Length == 0) {
                MessageBox.Show("You must enter a valid oAuth2 bearer token", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (txtApiKey.Text.Length == 0) {
                MessageBox.Show("You must enter a valid API key", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (txtApiSecret.Text.Length == 0) {
                MessageBox.Show("You must enter a valid API secret", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // disable
            btnLogin.IsEnabled = false;

            // setup client
            ApiClient client = new ApiClient(txtApiKey.Text, txtApiSecret.Text);
            client.Authentication = new BearerAuthentication(txtAccessToken.Text);

            // try and get user information
            UserEntity user = null;

            try {
                user = await client.Users.GetCurrentUserAsync();
            } catch(ApiException ex) {
                MessageBox.Show(ex.Errors[0].Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                btnLogin.IsEnabled = true;
                return;
            }

            // setup window
            EnergyWindow window = new EnergyWindow(client);
            window.Title = $"Energy - {user.EmailAddress}";

            // show energy window
            Hide();
            window.ShowDialog();
            btnLogin.IsEnabled = true;
            Show();
        }
    }
}
