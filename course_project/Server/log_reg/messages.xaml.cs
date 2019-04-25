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
    /// Логика взаимодействия для messages.xaml
    /// </summary>
    public partial class messages : Page
    {
        Repos.UnitOfWork kr;
        static Repos.NoteRepos nRep;
        public messages()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            nRep = kr.note_rep;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var l in nRep.GetAll())
            {
                if(l.date_of_takeplace>DateTime.Now)
                {
                    TextBlock m = new TextBlock();
                    Border k = new Border();
                    m.Width = 783;
                    k.Width = 783;
                    k.Height=1;
                    k.BorderThickness = new Thickness(1);
                    k.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFromString("#FFEEE8E1")); 
                    m.Height = 100;
                    m.Text = $"{l.notification} \n\n" +
                        $"Добавлено: {((DateTime)l.date_of_create).ToShortDateString()}\n" +
                        $"Дата проведения: {l.date_of_takeplace.ToString()}";
                    mes_cont.Children.Add(m);
                    mes_cont.Children.Add(k);
                }
            }
        }
    }
}
