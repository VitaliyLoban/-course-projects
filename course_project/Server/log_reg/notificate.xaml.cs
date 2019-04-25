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
    /// Логика взаимодействия для notificate.xaml
    /// </summary>
    public partial class notificate : Page
    {
        Repos.UnitOfWork kr;
        static Repos.NoteRepos nRep;
        public notificate()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            nRep = kr.note_rep;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (date_take.Value < DateTime.Now)
            {
                MessageBox.Show("Неверная дата");
            }
            else
            {
                if (date_take.Value != null && mes.Text != String.Empty)
                {
                    Note note = new Note();
                    note.date_of_create = DateTime.Now;
                    note.notification = mes.Text;
                    note.date_of_takeplace = date_take.Value;
                    nRep.Create(note);
                    kr.Save();
                    MessageBox.Show("Уведомление добавлено");
                }
                else MessageBox.Show("Сперва заполните все поля");
            }
        }
    }
}
