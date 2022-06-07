using System.Windows;

namespace Starter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new WindowViewModel(this);
            InitializeComponent();
        }
    }
}
