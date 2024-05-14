    using System.Windows;

namespace Nail_Salon_MVVM
{
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel(connectionString, ScheduleDatePicker);
            DataContext = viewModel;

            ScheduleDatePicker.SelectedDateChanged += (sender, e) =>
            {
                viewModel.DatePickerSelectedDateChangedCommand.Execute(null);
            };
        }
    }
}

