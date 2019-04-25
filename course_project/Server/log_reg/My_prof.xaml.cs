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

namespace log_reg
{
    /// <summary>
    /// Логика взаимодействия для My_prof.xaml
    /// </summary>
    public partial class My_prof : Page
    {
        Repos.UnitOfWork kr;
        static Repos.UserprofRepos uProf;
        static Repos.UserworkRepos uwRep;
        static int hou=0;
        public My_prof()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            uProf = kr.usprof_rep;
            uwRep = kr.uswork_rep;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            User_profile us = uProf.Get(this_us.id_us);
            if (us.hobbies != null)
            {
                string[] k = us.hobbies.Split(',');
                foreach (var z in k)
                {
                    hobies.Text += z + "\n";
                }
            }
            us_n.Content = us.Name+" " + us.S_name;
            d_b.Content = us.b_date;
            d_r.Content = us.receipt_date;
            ph_n.Content = us.phone_num;
            r_n.Content = us.room_num;
            foreach(var b in uwRep.GetAll())
            {
                if (b.stud_id == this_us.id_us) hou += Convert.ToInt32(b.working_off);
            }
            hours.Content = $"{hou}/26";
            hou = 0;
            
        }
    }
}
