using DataAccess;
using System.Windows;

namespace Nail_Salon_MVVM
{
    public partial class ClientRecordingWindow : Window
    {
        public ClientRecordingViewModel ViewModel { get; set; }

        public ClientRecordingWindow(IRepositoryFactory repositoryFactory)
        {
            InitializeComponent();
            ViewModel = new ClientRecordingViewModel(repositoryFactory);
            DataContext = ViewModel;
        }
    }
}

