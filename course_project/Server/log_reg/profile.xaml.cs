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
using System.Data.Entity;
using System.IO;

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для profile.xaml
    /// </summary>
    public partial class profile : Window
    {
        Repos.UnitOfWork kr;
        static Repos.UserprofRepos uRep;
        static List<User_profile> k = new List<User_profile>();
        public profile()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            uRep = kr.usprof_rep;
        }

        public void SetColor(string color)
        {
            gr1.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr2.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));
            gr3.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(color));

            //ColorConverter.ConvertFromString(color);
        }
        public void MainImg(byte[] kl)
        {
            MemoryStream ms = new MemoryStream(kl);
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            MemoryStream mes = new MemoryStream();
            im.Save(mes, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = mes;
            bitmap.EndInit();
            my_photo.Source = bitmap;
        }

        public void SetOn(bool k)
        {
            ab_me.IsEnabled = k;
            not.IsEnabled = k;
            Set.IsEnabled = k;
        }

        public void SetPhoto()
        {

            User_profile my_us = uRep.Get(this_us.id_us);
            if (my_us.photo != null)
            {
                MainImg(my_us.photo);
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/img/user.png");
                bitmap.EndInit();
                my_photo.Source = bitmap;
            }

        }
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frame2.Content = new UsOnYourFloor();
            if (this_us.isGood != false)
                SetPhoto();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            My_prof k = new My_prof();
            frame1.Content = k;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Settings z = new Settings(this);
            frame1.Content = z;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            frame1.Content = new messages();
            
        }

        private void ab_me_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (this_us.isGood == false) ab_me.IsEnabled = false;
            //else ab_me.IsEnabled = true;
        }

        private void not_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (this_us.isGood == false) not.IsEnabled = false;
            //else not.IsEnabled = true;
        }

        private void Set_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (this_us.isGood == false) Set.IsEnabled = false;
            //else not.IsEnabled = true;
        }
    }
}
