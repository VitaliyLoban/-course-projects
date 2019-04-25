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
using System.Net;
using System.Data.Entity;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    //


    static class this_us
    {
        public static string firstname { get; set; }
        public static string secname { get; set; }
        public static string phonenub { get; set; }
        public static int id_us { get; set; }
        public static string log { get; set; }
        public static bool isGood { get; set; }


    }
    public partial class MainWindow : Window
    {
        #region Пременные
        Repos.UnitOfWork kr;
        bool isOk = false;
        Validation_class val;
        static Repos.UserRepos userRepos;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            userRepos = kr.us_rep;
            val = new Validation_class();
            this.DataContext = val;
        }
        private void signin_click(object sender, RoutedEventArgs e)
        {
            User us1 = userRepos.Get(1);
            bool kek = false;
            kek = login.Text == us1.login &&
                pass.Password.GetHashCode().ToString() == us1.password;
            if (kek)
            {
                isOk = true;
                Admin z = new Admin();
                z.Show();
            }
            else
            {
                foreach (var k in userRepos.GetAll())
                {
                    if (login.Text == k.login &&
                        pass.Password.GetHashCode().ToString() == k.password)
                    {
                        this_us.isGood = true;
                        this_us.id_us = k.id;
                        isOk = true;
                        profile z = new profile();
                        z.Show();
                    }
                }
            }
            if (isOk == false)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
            isOk = false;
        }
        private void register_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(f_name.Text) || 
                    String.IsNullOrEmpty(s_name.Text) || 
                    String.IsNullOrEmpty(u_name.Text) || 
                    String.IsNullOrEmpty(Passw.Password) || 
                    String.IsNullOrEmpty(passw1.Password))
                {
                    MessageBox.Show("Сперва заполните все строки!");
                }
                else if (Validation_class.log_ok == false ||
                    Validation_class.name_ok == false ||
                    Validation_class.sname_ok == false || 
                    Validation_class.phone_ok == false)
                    MessageBox.Show("Проверте коректность ввода");
                else if (Passw.Password != passw1.Password)
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
                else if (userRepos.IsExist(u_name.Text))
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                }
                else
                {
                    User_profile prof = new User_profile();
                    User user = new User();
                    this_us.firstname = f_name.Text;
                    this_us.secname = s_name.Text;
                    this_us.phonenub = phone.Text;
                    this_us.log = u_name.Text;
                    this_us.isGood = false;
                    user.login = u_name.Text;
                    user.password = Passw.Password.GetHashCode().ToString();
                    userRepos.Create(user);
                    kr.Save();
                    User this_user = userRepos.FindLog(u_name.Text);
                    this_us.id_us = this_user.id;
                    profile z = new profile();
                    z.ab_me.IsEnabled = false;
                    z.not.IsEnabled = false;
                    z.Set.IsEnabled = false;
                    more_inf k = new more_inf(z);
                    z.frame1.Content = k;
                    z.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

