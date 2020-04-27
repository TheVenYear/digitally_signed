using ClassLibrary1;
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

namespace digitally_signed
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Настройки в соответствии с вариантом
        Rsa rsa = new Rsa(13, 7, 5, 29, '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-');
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            output.Text = "";
            try
            {
                var list = rsa.Encrypt(input.Text.GetHashCode().ToString());
                key.Text = $"{rsa.D}:{rsa.N}";
                foreach (var item in list)
                {
                    output.Text += item;
                    output.Text += '*';
                }
                output.Text =  output.Text.Remove(output.Text.Length - 1);
            }
            catch
            {
                MessageBox.Show("Вы ввели неверное значение!");
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var list = output.Text.Split('*').Where(item => item != "").ToList();
            var keys = key.Text.Split(':').Select(item => int.Parse(item)).ToArray();
            var hash = rsa.Decrypt(list, keys[0], keys[1]);
            if (hash == input.Text.GetHashCode().ToString())
            {
                MessageBox.Show("Цифровая подпись совпала");
            }
            else
            {
                MessageBox.Show("Цифровая подпись не совпадает");
            }
        }
    }
}
