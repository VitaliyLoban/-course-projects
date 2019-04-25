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
    /// Логика взаимодействия для UsOnYourFloor.xaml
    /// </summary>
    public partial class UsOnYourFloor : Page
    {
        Repos.UnitOfWork kr;
        static Repos.UserprofRepos upRep;
        public UsOnYourFloor()
        {
            InitializeComponent();
            kr = new Repos.UnitOfWork();
            upRep = kr.usprof_rep;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(upRep.IsExist(this_us.id_us))
            {
                foreach(var k in upRep.SortRoom())
                {
                    if((((k.room_num-k.room_num%100)/100)==(upRep.Get(this_us.id_us).room_num-(upRep.Get(this_us.id_us).room_num%100))/100)&& k.user_id != 1)
                    {
                        TextBlock z = new TextBlock();
                        z.Width = 295;
                        z.Height = 60;
                        z.FontFamily= new FontFamily("Rage Italic");
                        z.FontSize = 16;
                        z.Text = $"      Имя: {k.Name}\n" +
                            $"      Фамилия:{k.S_name}\n" +
                            $"      Комната: {k.room_num}";
                        Border p = new Border();
                        Image img = new Image();
                        img.Width = 150;
                        img.Height = 150;
                        img.HorizontalAlignment = HorizontalAlignment.Center;
                        MemoryStream ms = new MemoryStream(k.photo);
                        System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
                        MemoryStream mes = new MemoryStream();
                        im.Save(mes, System.Drawing.Imaging.ImageFormat.Png);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = mes;
                        bitmap.EndInit();
                        img.Source = bitmap;

                        img.Stretch = Stretch.Fill;
                        p.Width = 295;
                        p.Height = 1;
                        p.BorderThickness = new Thickness(1);
                        p.BorderBrush= (SolidColorBrush)(new BrushConverter().ConvertFromString("#FFE2DADA"));
                        friends.Children.Add(z);
                        friends.Children.Add(img);
                        friends.Children.Add(p);
                    }
                }
            }
        }
    }
}
