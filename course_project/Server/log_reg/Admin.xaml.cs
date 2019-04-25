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
using System.Windows.Shapes;

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }
        public void SetColor(string color)
        {
            gr1.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr2.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr3.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));

            //ColorConverter.ConvertFromString(color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame1.Content = new Del(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            frame1.Content = new notificate();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frame1.Content = new Admin_Settings(this);
        }
    }
}
