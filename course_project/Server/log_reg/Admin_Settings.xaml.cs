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

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для Admin_Settings.xaml
    /// </summary>
    public partial class Admin_Settings : Page
    {
        Admin _admin;
        Repos.UnitOfWork kr;
        static Repos.UserRepos userRepos;

        public Admin_Settings(Admin _admin)
        {
            this._admin = _admin;
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            userRepos = kr.us_rep;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User us = userRepos.Get(1);
            if (pas.Password.GetHashCode().ToString() == us.password)
            {
                if (pas1.Password == pas2.Password)
                {
                    us.password = pas1.Password.GetHashCode().ToString();
                    userRepos.Update(us);
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
            _admin.SetColor(tem.SelectedColor.ToString());
        }
    }
}
