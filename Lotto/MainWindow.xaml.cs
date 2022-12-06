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
using System.Windows.Threading;

namespace Lotto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random random = new Random();
        private List<int> sorsoltSzamok = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSorsol_Click(object sender, RoutedEventArgs e)
        {
            if ((string)buttonSorsol.Content == "Sorsol")
            {
                Sorsol();
            }
            else
            {
                Rendez();
            }
        }

        private void Sorsol()
        {
            int sorsoltSzam = random.Next(90) + 1;
            while (sorsoltSzamok.Contains(sorsoltSzam))
            {
                sorsoltSzam = random.Next(90) + 1;
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,10);
            DateTime inditasiIdopont = DateTime.Now;
            timer.Tick += (sender, evenArgs) =>
            {
                int veletlenSzam = random.Next(90) + 1;
                labelSorsoltSzam.Content = veletlenSzam;
                DateTime aktualisIdopont = DateTime.Now;
                TimeSpan eleteltIdo = aktualisIdopont - inditasiIdopont;
                if (eleteltIdo.Seconds >= 2)
                {
                    timer.Stop();
                    buttonSorsol.IsEnabled = true;
                    sorsoltSzamok.Add(sorsoltSzam);
                    if (sorsoltSzamok.Count >= 5)
                    {
                        buttonSorsol.Content = "Rendez";
                    }
                    labelSorsoltSzam.Content = sorsoltSzam;
                    labelSzamok.Content = String.Join(" ", sorsoltSzamok);
                }
            };
            buttonSorsol.IsEnabled = false;
            timer.Start();
        }

        private void Rendez()
        {
            sorsoltSzamok.Sort();
            labelSzamok.Content = String.Join(" ", sorsoltSzamok);
            buttonSorsol.Content = "Sorsol";
            sorsoltSzamok.Clear();
        }
    }
}
