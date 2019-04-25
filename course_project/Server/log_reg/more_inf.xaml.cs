using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для more_inf.xaml
    /// </summary>
    /// 

    public partial class more_inf : Page
    {
        Repos.UnitOfWork kr;
        static Repos.UserprofRepos uRep;
        profile more;
        Validation_class val;
        static string hob = null;
        static byte[] img;
        public more_inf(profile more)
        {
            this.more = more;
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            uRep = kr.usprof_rep;
            val = new Validation_class();
            this.DataContext = val;
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count_us = 0;
            foreach (var kl in uRep.GetAll())
            {
                if (kl.room_num.Equals(num_r.Value))
                    count_us++;
            }
            if ((bool)h1.IsChecked) hob = hob + h1.Content + ",";
            if ((bool)h2.IsChecked) hob = hob + h2.Content + ",";
            if ((bool)h3.IsChecked) hob = hob + h3.Content + ",";
            if ((bool)h4.IsChecked) hob = hob + h4.Content + ",";
            if ((bool)h5.IsChecked) hob = hob + h5.Content + ",";
            if ((bool)h6.IsChecked) hob = hob + h6.Content + ",";
            if (h7.Text!= String.Empty) hob = hob + h7.Text + ",";

            if (Validation_class.bdate_ok == false || Validation_class.rdate_ok == false)
                MessageBox.Show("Проверте корректность ввода");
            else if (count_us >= 4)
                MessageBox.Show("Указанная комната заполнена");
            else
            {

                User_profile us = new User_profile();
                us.user_id = this_us.id_us;
                us.b_date = birthday.SelectedDate.Value.Date;
                us.receipt_date = receiptday.SelectedDate.Value.Date;
                us.room_num = (int)num_r.Value;
                us.Name = this_us.firstname;
                us.S_name = this_us.secname;
                us.hobbies = hob;
                us.phone_num = this_us.phonenub;
                us.photo = img;


                uRep.Create(us);
                kr.Save();
                MessageBox.Show("Вы успешно зарегестрированы!");
                this_us.isGood = true;
                more.SetOn(this_us.isGood);
                Content = null;
                more.MainImg(img);
                more.Window_Loaded(this, null);
            }
            hob = String.Empty;
            

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fop = new OpenFileDialog();
            fop.Filter = " Image files (*.jpg)|*.jpg|Png files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp|All files(*.)|*.*";
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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/img/user.png");
            bitmap.EndInit();
            my_ph.Source = bitmap;
            FileStream fs1 = new System.IO.FileStream(@"user.png", FileMode.Open, FileAccess.Read);
            System.Drawing.Image image1 = System.Drawing.Image.FromStream(fs1);
            MemoryStream k = new MemoryStream();
            image1.Save(k, System.Drawing.Imaging.ImageFormat.Png);
            img = k.ToArray();
            birthday.SelectedDate = DateTime.Now.AddDays(-1).Date;
            receiptday.SelectedDate = DateTime.Now.Date;

        }
    }
}
