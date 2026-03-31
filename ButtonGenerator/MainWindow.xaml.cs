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

namespace ButtonGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButtons_Click(object sender, RoutedEventArgs e)
        {
            bool isFromValid = int.TryParse(TxtFrom.Text, out int from);
            bool isToValid = int.TryParse(TxtTo.Text, out int to);
            bool isStepValid = int.TryParse(TxtStep.Text, out int step);

            if (!isFromValid || !isToValid || !isStepValid)
            {
                MessageBox.Show("Помилка! Будь ласка, введіть цілі числа у поля 'від', 'до' та 'з кроком'.",
                                "Некоректний ввід", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (step <= 0)
            {
                MessageBox.Show("Крок має бути більшим за нуль!",
                                "Некоректний крок", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (from > to)
            {
                MessageBox.Show("Значення 'від' не може бути більшим за значення 'до'.",
                                "Помилка меж", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int totalButtons = (to - from) / step + 1;

            if (totalButtons > 1000)
            {
                MessageBox.Show("Забагато кнопок! Ви намагаєтеся створити більше 1000 кнопок за один раз.",
                                "Перевищено ліміт", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = from; i <= to; i += step)
            {
                Button btn = new Button
                {
                    Content = i.ToString(),
                    Width = 40,
                    Height = 30,
                    Margin = new Thickness(2),
                    Tag = false 
                };
                btn.Click += NumberButton_Click;
                ButtonsContainer.Children.Add(btn);
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int num = int.Parse(btn.Content.ToString());
            bool wasClicked = (bool)btn.Tag;

            string status = GetNumberType(num);
            string repeatMsg = wasClicked ? "ЦЕ ПОВТОРНИЙ ВИКЛИК! \n\n" : "";

            MessageBox.Show($"{repeatMsg}Число {num}: {status}", "Інформація про число");

            btn.Tag = true; 
        }

        private string GetNumberType(int n)
        {
            if (n < 0) return "від'ємне число (не є ні простим, ні складеним)";
            if (n < 2) return "не є ні простим, ні складеним";

            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return "складене число";
            }
            return "просте число";
        }

        private void RemoveButtons_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtMultiple.Text, out int divisor) || divisor == 0)
            {
                MessageBox.Show("Введіть коректне число (відмінне від нуля) для вилучення кратних кнопок.",
                                "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            for (int i = ButtonsContainer.Children.Count - 1; i >= 0; i--)
            {
                var btn = (Button)ButtonsContainer.Children[i];
                int val = int.Parse(btn.Content.ToString());

                if (val % divisor == 0)
                {
                    ButtonsContainer.Children.RemoveAt(i);
                }
            }
        }
    }
}