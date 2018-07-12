using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WifiPlug.Api;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Schema;

namespace Example.EnergyGraphing
{
    /// <summary>
    /// Interaction logic for EnergyWindow.xaml
    /// </summary>
    public partial class EnergyWindow : Window
    {
        private ApiClient _client;
        private Guid _selectedDevice;
        private Guid _selectedService;
        private CancellationTokenSource _liveSource = new CancellationTokenSource();
        private ChartValues<HistoricalEnergyReadingEntity> _liveValues = new ChartValues<HistoricalEnergyReadingEntity>();

        private async void RefreshLiveLoop() {
            while(!_liveSource.IsCancellationRequested) {
                try {
                    // update
                    if (_selectedDevice != Guid.Empty) {
                        // get reading
                        EnergyReadingEntity reading = await _client.Devices.GetDeviceServiceEnergyAsync(_selectedDevice, _selectedService);

                        // update
                        gaugeCurrent.Value = reading.Current;
                        gaugePower.Value = reading.Power;
                        gaugeVoltage.Value = reading.Voltage;

                        _liveValues.Add(new HistoricalEnergyReadingEntity() {
                            Current = reading.Current,
                            Power = reading.Power,
                            Voltage = reading.Voltage,
                            Timestamp = DateTime.Now
                        });

                        if (_liveValues.Count > 20) _liveValues.RemoveAt(0);
                    }

                    // wait
                    await Task.Delay(2000, _liveSource.Token);
                } catch(OperationCanceledException) {
                    return;
                }
            }
        }

        private async Task RefreshDevicesAsync() {
            btnRefresh.IsEnabled = false;

            // refresh devices
            DeviceEntity[] devices = null;

            try {
                devices = await _client.Devices.ListDevicesAsync();
            } catch(ApiException) {
                btnRefresh.IsEnabled = true;
                throw;
            }

            // clear list
            listDevices.Items.Clear();

            // create items
            foreach (DeviceEntity e in devices) {
                // find energy service
                DeviceServiceEntity energyService = e.Services.FirstOrDefault(s => s.TypeUUID == new Guid("e4b8c402-e3f8-4ba3-8593-8565664a18bf"));

                // create panel
                StackPanel panel = new StackPanel();
                panel.Tag = new Tuple<Guid, Guid, string>(e.UUID, energyService == null ? Guid.Empty : energyService.UUID, e.Name);
                panel.Orientation = Orientation.Horizontal;

                if (energyService == null)
                    panel.IsEnabled = false;

                panel.Children.Add(new Label() {
                    Content = e.Type.Substring(0, 1),
                    FontSize = 16
                });

                panel.Children.Add(new Label() {
                    Content = e.Name,
                });

                listDevices.Items.Add(panel);
            }

            btnRefresh.IsEnabled = true;
        }

        private async Task RefreshHistoricalEnergyAsync() {
            // calculate dates
            DateTime from = DateTime.UtcNow - TimeSpan.FromDays(7);
            from = new DateTime(from.Year, from.Month, from.Day, 0, 0, 0, 0);

            DateTime to = DateTime.UtcNow;
            to = new DateTime(to.Year, to.Month, to.Day, 0, 0, 0, 0);

            // get data
            HistoricalEnergyConsumptionEntity[] data =
                await _client.Devices.GetDeviceServiceEnergyHistoricalConsumptionAsync(_selectedDevice, _selectedService, from, to, HistoricalGrouping.Day);
            
            // update axis
            RefreshDayLabels(from, to);

            // update series
            chartHistorical.Series = new SeriesCollection {
                new ColumnSeries
                {
                    Title = "Consumption (wH)",
                    Values = new ChartValues<double>(data.Select(d => Math.Round(d.KilowattHours * 1000, 4)))
                }
            };

            // update series
            _liveValues = new ChartValues<HistoricalEnergyReadingEntity>();
            chartLive.Series = new SeriesCollection {
                new LineSeries
                {
                    Title = "Power (W)",
                    Values = _liveValues,
                    Stroke = Brushes.DarkRed,
                    StrokeThickness = 4,
                    LineSmoothness = 0.25
                }
            };

            // update labels
            string consumption = Math.Round(data.Sum(c => c.KilowattHours), 4).ToString();
            string cost = Math.Round((data.Sum(c => c.KilowattHours) * 0.16), 2).ToString();

            lblTotalConsumption.Content = $"Last 7 days: {consumption} KwH (£{cost})";
        }

        private void RefreshDayLabels(DateTime from, DateTime to) {
            // update axis
            string[] dayLabels = new string[7];

            for (int i = 0; i < 7; i++) {
                dayLabels[i] = (from + TimeSpan.FromDays(i)).ToString("dd/MM/yy");
            }

            axisHistoricalDay.Labels = dayLabels;
            axisHistoricalDay.Separator.Step = 1;
        }

        public EnergyWindow(ApiClient client) {
            InitializeComponent();
            _client = client;

            // refresh
            RefreshLiveLoop();

            // setup axis
            axisLiveTime.LabelFormatter = (f) => new DateTime((long)f).ToString("HH:mm:sss");
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e) {
            try {
                await RefreshDevicesAsync();
            } catch (ApiException ex) {
                MessageBox.Show(ex.Errors[0].Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            await RefreshDevicesAsync();
        }

        private async void listDevices_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems.Count == 0)
                return;

            if (!((UIElement)e.AddedItems[0]).IsEnabled)
                return;

            listDevices.IsEnabled = false;

            // get tag uuid
            var tuple = (Tuple<Guid,Guid,string>)((StackPanel)e.AddedItems[0]).Tag;
            _selectedDevice = tuple.Item1;
            _selectedService = tuple.Item2;

            // refresh historical
            try {
                await RefreshHistoricalEnergyAsync();
            } catch (ApiException ex) {
                MessageBox.Show(ex.Errors[0].Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _selectedDevice = Guid.Empty;
            }

            // update label
            lblSelected.Content = $"Selected: {tuple.Item3}";

            listDevices.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            _liveSource.Cancel();
        }
    }
}
