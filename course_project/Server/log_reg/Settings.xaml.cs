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
using System.Data.Entity;
using Microsoft.Win32;
using System.IO;

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    /// 

    //public event EventHandler IsCheck = delegate {};

    public partial class Settings : Page
    {
        Repos.UnitOfWork kr;
        static Repos.UserRepos uRep;
        static Repos.UserprofRepos upRep;
        profile _profile;
        Validation_class val;
        static byte[] img;
        static string hob = String.Empty;
        public Settings(profile _profile)
        {
            this._profile = _profile;
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            uRep = kr.us_rep;
            upRep = kr.usprof_rep;
            val = new Validation_class();
            this.DataContext = val;
        }
        private void Change_pass(object sender, RoutedEventArgs e)
        {
            User us = uRep.Get(this_us.id_us);
            if (pas.Password.GetHashCode().ToString() == us.password)
            {
                if (pas1.Password == pas2.Password)
                {
                    us.password = pas1.Password.GetHashCode().ToString();
                    uRep.Update(us);
                    kr.Save();
                    MessageBox.Show("Пароль изменен");
                }
                else
                    MessageBox.Show("Пароли не совпадают!");
            }
            else
                MessageBox.Show("Старый пароль введен не правильно!");
        } 
        private void col_change(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            //tema.col = tem.SelectedColor.ToString();
            _profile.SetColor(tem.SelectedColor.ToString());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            User_profile us = upRep.Get(this_us.id_us);
            nam.Text = us.Name;
            s_nam.Text = us.S_name;
            receiptday.SelectedDate = us.receipt_date;
            birthday.SelectedDate = us.b_date;
            num_r.Value = us.room_num;
            phone.Text = us.phone_num;
            //my_ph.Source//.Remove(0, 6).Remove(2, 2).Remove(5, 1).Remove(7, 1);
            if (us.photo != null)
            {
                MemoryStream ms = new MemoryStream(us.photo);
                System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
                MemoryStream mes = new MemoryStream();
                im.Save(mes, System.Drawing.Imaging.ImageFormat.Png);
                img = mes.ToArray();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = mes;
                bitmap.EndInit();
                my_ph.Source = bitmap;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/img/user.png");
                bitmap.EndInit();
                my_ph.Source = bitmap;
                FileStream fs1 = new FileStream(@"user.png", FileMode.Open, FileAccess.Read);
                System.Drawing.Image image1 = System.Drawing.Image.FromStream(fs1);
                MemoryStream k = new MemoryStream();
                image1.Save(k, System.Drawing.Imaging.ImageFormat.Png);
                img = k.ToArray();
            }
            foreach (UIElement elem in main_grid.Children)
                elem.Focus();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if (Validation_class.bdate_ok==false || Validation_class.name_ok==false || Validation_class.rdate_ok==false || Validation_class.sname_ok==false || Validation_class.phone_ok==false)
            {
                MessageBox.Show("Проверте корректность вввода");
            }
            else
            {
                User_profile user_ = upRep.Get(this_us.id_us);
                user_.Name = nam.Text;
                user_.S_name = s_nam.Text;
                user_.receipt_date = receiptday.SelectedDate;
                user_.b_date = birthday.SelectedDate;
                user_.room_num = num_r.Value;
                user_.phone_num = phone.Text;
                if ((bool)h1.IsChecked) hob = hob + h1.Content + ",";
                if ((bool)h2.IsChecked) hob = hob + h2.Content + ",";
                if ((bool)h3.IsChecked) hob = hob + h3.Content + ",";
                if ((bool)h4.IsChecked) hob = hob + h4.Content + ",";
                if ((bool)h5.IsChecked) hob = hob + h5.Content + ",";
                if ((bool)h6.IsChecked) hob = hob + h6.Content + ",";
                if (h7.Text != String.Empty) hob = hob + h7.Text + ",";
                user_.hobbies = hob;
                user_.photo = img;
                upRep.Update(user_);
                kr.Save();
                MessageBox.Show("Профиль  изменен");
                _profile.MainImg(img);
                _profile.frame2.Content = new UsOnYourFloor();
                hob = String.Empty;
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fop = new OpenFileDialog();
            fop.Filter = " Png files (*.png)|*.png|Image files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|All files(*.)|*.*";
            if (fop.ShowDialog() == true)
            {

                System.Drawing.Image image = System.Drawing.Image.FromFile(fop.FileName);
                MemoryStream k = new MemoryStream();
                image.Save(k, System.Drawing.Imaging.ImageFormat.Png);
                img = k.ToArray();
                MemoryStream ms = new MemoryStream(img);
                System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
                MemoryStream mes = new MemoryStream();
                im.Save(mes, System.Drawing.Imaging.ImageFormat.Png);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = mes;
                bitmap.EndInit();
                my_ph.Source = bitmap;

            }
        }
    }
}
