using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunningButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoodChoiseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have made a good choice!");
            Process.Start(new ProcessStartInfo("https://learn.microsoft.com/uk-ua/dotnet/csharp/") { UseShellExecute = true });
        }
        private void BadChoiseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(this);

            double windowWidth = this.ActualWidth;
            double windowHeight = this.ActualHeight;
            double btnWidth = BadChoiseButton.ActualWidth;
            double btnHeight = BadChoiseButton.ActualHeight;

            double newLeft, newTop;
            bool isTooClose;

            do
            {
                int maxLeft = (int)(windowWidth - btnWidth - 40);
                int maxTop = (int)(windowHeight - btnHeight - 60);

                newLeft = _random.Next(10, Math.Max(11, maxLeft));
                newTop = _random.Next(10, Math.Max(11, maxTop));

                double distanceX = Math.Abs(newLeft + btnWidth / 2 - mousePosition.X);
                double distanceY = Math.Abs(newTop + btnHeight / 2 - mousePosition.Y);

                isTooClose = distanceX < 100 && distanceY < 100;

            } while (isTooClose);

            BadChoiseButton.Margin = new Thickness(newLeft, newTop, 0, 0);
        }

        private void BadChoiseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cheater!");
        }
    }
}