using System;
using System.Collections.Generic;
using System.IO;
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
namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для Del.xaml
    /// </summary>
    public partial class Del : Page
    {
        #region переменные
        Repos.UnitOfWork kr;
        static Repos.UserRepos userRepos;
        static Repos.UserprofRepos uprofRepos;
        static Repos.UserworkRepos uworkRepos;
        static Repos.UseractRepos uactRepos;
        List<User_profile> us_list = new List<User_profile>();
        bool IsEmpty = false;
        //bool IsOk = false;
        bool IsBad = false;
        static string hob = null;
        static string hobbies;
        static int id;
        static int hour = 0;
        static string[] k;
        //List<string> act_list = new List<string>();
        #endregion
        public Del()
        {
            // LogicalTreeHelper.FindLogicalNode(stack, "name button1") as Button;
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            userRepos = kr.us_rep;
            uprofRepos = kr.usprof_rep;
            uworkRepos = kr.uswork_rep;
            uactRepos = kr.usact_rep;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            date_act.SelectedDate = DateTime.Now;
            stack.Children.Clear();
            foreach (var z in uprofRepos.Sort())
            {
                if (z.Name != uprofRepos.Get(1).Name)
                {
                    Button k = new Button();
                    k.Width = 393;
                    k.Height = 30;
                    k.Name = $"but{z.user_id}";
                    k.Click += Show_prof_Click;
                    k.Content = $"{z.Name} {z.S_name}";
                    stack.Children.Add(k);
                }
            }


        }
        private void Show_prof_Click(object sender, RoutedEventArgs e)
        {
            hobb.Text = null;
            aktiv_list.Items.Clear();
            var a = sender as Button;
            k = null;
            id = Convert.ToInt32(a.Name.Substring(3));
            User_profile check_us = uprofRepos.Get(id);
            //user_work work_check= db.user_work.Where(c => c.stud_id == id).First();
            if (check_us.hobbies != null)
            {
                k = check_us.hobbies.Split(',');
                foreach (var z in k)
                {
                    hobbies += z + "\n";
                }

            }
            if (check_us.photo != null)
            {
                MemoryStream ms = new MemoryStream(check_us.photo);
                System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
                MemoryStream mes = new MemoryStream();
                im.Save(mes, System.Drawing.Imaging.ImageFormat.Png);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = mes;
                bitmap.EndInit();
                check_user.Source = bitmap;
            }
            fio.Content = $"{check_us.S_name} {check_us.Name}";
            room_num.Content = check_us.room_num;
            phone.Content = check_us.phone_num;
            birth.Content = check_us.b_date;
            reciept.Content = check_us.receipt_date;
            phone.Content = check_us.phone_num;
            if (hobbies != String.Empty) hobb.Text = hobbies;

            hobbies = String.Empty;

            foreach (var x in uworkRepos.GetAll())
            {
                if (x.stud_id == id) hour += Convert.ToInt32(x.working_off);
            }
            hours.Content = hour;
            hour = 0;
            foreach (var b in uactRepos.GetAll())
            {
                if (b.id_stud == id) aktiv_list.Items.Add($"{b.participation} {((DateTime)b.date).ToShortDateString()}");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            foreach (var z in uprofRepos.Sort())
            {

                if (f_name.Text != String.Empty)
                {
                    if (f_sname.Text != String.Empty)
                    {
                        if (num_r.Value != null)
                        {
                            if (z.Name == f_name.Text && z.S_name == f_sname.Text && z.room_num == num_r.Value && z.user_id != 1) us_list.Add(z);
                        }
                        else
                        {
                            if (z.Name == f_name.Text && z.S_name == f_sname.Text && z.user_id != 1) us_list.Add(z);
                        }
                    }
                    else
                    {
                        if (num_r.Value != null)
                        {
                            if (z.Name == f_name.Text && z.room_num == num_r.Value && z.user_id != 1) us_list.Add(z);
                        }
                        else
                        {
                            if (z.Name == f_name.Text && z.user_id != 1) us_list.Add(z);
                        }
                    }
                }
                else
                {
                    if (f_sname.Text != String.Empty)
                    {
                        if (num_r.Value != null)
                        {
                            if (z.S_name == f_sname.Text && z.room_num == num_r.Value && z.user_id != 1) us_list.Add(z);
                        }
                        else
                        {
                            if (z.S_name == f_sname.Text && z.user_id != 1) us_list.Add(z);
                        }
                    }
                    else
                    {
                        if (num_r.Value != null)
                        {
                            if (z.room_num == num_r.Value && z.user_id != 1) us_list.Add(z);
                        }
                        else
                        {
                            IsEmpty = true;
                        }
                    }
                }

            }


            if (IsEmpty == true)
            {
                MessageBox.Show("Сперва добавте информацию");

                IsEmpty = false;
            }

            else
            {
                if (us_list.Count != 0)
                {
                    stack.Children.Clear();
                    foreach (var k in us_list)
                    {
                        Button z = new Button();
                        z.Width = 393;
                        z.Height = 30;
                        z.Name = $"but{k.user_id}";
                        z.Click += Show_prof_Click;
                        z.Content = $"{k.Name} {k.S_name}";

                        stack.Children.Add(z);

                    }
                }
                else MessageBox.Show("Поиск не дал результатов");
            }
            us_list.Clear();

        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)h1.IsChecked) hob = hob + h1.Content + ",";
            if ((bool)h2.IsChecked) hob = hob + h2.Content + ",";
            if ((bool)h3.IsChecked) hob = hob + h3.Content + ",";
            if ((bool)h4.IsChecked) hob = hob + h4.Content + ",";
            if ((bool)h5.IsChecked) hob = hob + h5.Content + ",";
            if ((bool)h6.IsChecked) hob = hob + h6.Content + ",";
            if (h7.Text != String.Empty) hob = hob + h7.Text + ",";
            if (!(bool)h1.IsChecked && !(bool)h2.IsChecked && !(bool)h3.IsChecked && !(bool)h4.IsChecked && !(bool)h5.IsChecked && !(bool)h6.IsChecked && h7.Text == String.Empty)
            {
                MessageBox.Show("Сперва добавте информацию");
            }
            else
            {
                foreach (var z in uprofRepos.GetAll())
                {
                    foreach (var k in hob.Split(','))
                    {
                        if (z.hobbies != null)
                        {
                            if (!z.hobbies.Contains(k)) IsBad = true;

                        }
                    }
                    if (IsBad != true && z.user_id != 1) us_list.Add(z);
                    IsBad = false;
                }
                if (us_list.Count != 0)
                {
                    stack.Children.Clear();
                    foreach (var k in us_list)
                    {
                        Button z = new Button();
                        z.Width = 393;
                        z.Height = 30;
                        z.Name = $"but{k.user_id}";
                        z.Click += Show_prof_Click;
                        z.Content = $"{k.Name} {k.S_name}";

                        stack.Children.Add(z);

                    }
                }
                else MessageBox.Show("Поиск не дал результатов");
            }
            us_list.Clear();
            hob = String.Empty;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (id != 0 && id != 1 && uprofRepos.IsExist(id))
            {
                user_work us_work = new user_work();
                us_work.stud_id = id;
                us_work.working_off = num_hour.Value.ToString();
                us_work.date = DateTime.Now;
                uworkRepos.Create(us_work);
                kr.Save();
                foreach (var x in uworkRepos.GetAll())
                {
                    if (x.stud_id == id) hour += Convert.ToInt32(x.working_off);
                }
                hours.Content = hour;
                hour = 0;
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (id != 0 && id != 1&& uprofRepos.IsExist(id))
            {
                if (activ.Text == String.Empty)
                {
                    MessageBox.Show("Сперва добавте информацию! ");
                }
                else
                {
                    aktiv_list.Items.Clear();
                    User_activity us_act = new User_activity();
                    us_act.date = date_act.SelectedDate.Value.Date;
                    us_act.id_stud = id;
                    us_act.participation = activ.Text;

                    uactRepos.Create(us_act);
                    kr.Save();
                    foreach (var b in uactRepos.GetAll())
                    {
                        if (b.id_stud == id) aktiv_list.Items.Add($"{b.participation} {((DateTime)b.date).ToShortDateString()}");
                    }
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (id != 0 && id != 1 && uprofRepos.IsExist(id))
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Вы действительно хотите удалить данного подьзователя", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var r in uprofRepos.GetAll())
                    {
                        if (r.user_id == id)
                        {
                            uprofRepos.DeleteUser(r);

                        }
                    }
                    foreach (var r in uactRepos.GetAll())
                    {
                        if (r.id_stud == id)
                        {
                            uactRepos.DeleteUser(r);

                        }
                    }
                    foreach (var r in uworkRepos.GetAll())
                    {
                        if (r.stud_id == id)
                        {
                            uworkRepos.DeleteUser(r);

                        }
                    }
                    userRepos.Delete(id);
                    kr.Save();
                    Page_Loaded(this, null);
                }
            }
        }

        private void del_all(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Вы действительно хотите удалить всех подьзователей", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                foreach(var b in uprofRepos.GetAll())
                {
                    if (b.user_id != 1)
                        uprofRepos.DeleteUser(b);
                }
                foreach (var qw in uactRepos.GetAll())
                {
                    if (qw.id_stud != 1)
                        uactRepos.DeleteUser(qw);
                }
                foreach (var b in uworkRepos.GetAll())
                {
                    if (b.stud_id != 1)
                        uworkRepos.DeleteUser(b);
                }
                foreach (var b in userRepos.GetAll())
                {
                    if (b.id != 1)
                        userRepos.Delete(b.id);
                }
                kr.Save();
                Page_Loaded(this, null);
            }
        }
    }
}