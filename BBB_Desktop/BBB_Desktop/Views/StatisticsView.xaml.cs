using BBB_Desktop.ViewModels;
using System.Windows.Controls;

namespace BBB_Desktop.Views
{
    /// <summary>
    /// Interaction logic for StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        private StatsViewModel statsVM;

        public StatisticsView()
        {
            InitializeComponent();
            statsVM = new StatsViewModel();
            DataContext = statsVM;
            Loaded += StatisticsView_Loaded;
            StartDate.SelectedDate = DateTime.Today.AddYears(-3);
            EndDate.SelectedDate = DateTime.Today;
            StartDate.SelectedDateChanged += ReloadStatsAsync;
            EndDate.SelectedDateChanged += ReloadStatsAsync;
        }

        private async void StatisticsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await statsVM.LoadAsync();
        }

        private async void ReloadStatsAsync(object?sender, System.Windows.RoutedEventArgs e)
        {
            await statsVM.LoadAsync(StartDate.SelectedDate, EndDate.SelectedDate);
        }
    }
}
