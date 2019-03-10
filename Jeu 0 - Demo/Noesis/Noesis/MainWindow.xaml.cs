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

namespace Noesis
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Initialized += OnInitialized;
            InitializeComponent();
        }

        private void OnInitialized(object sender, EventArgs args)
        {
            PersonnagesGUI data = new PersonnagesGUI();

            data.Add(new PersonnageGUI(true, 150, 150, 2));
            data.Add(new PersonnageGUI(false, 150, 125, 1));
            data.Add(new PersonnageGUI(true, 150, 100, 0));
            data.Add(new PersonnageGUI(false, 150, 75, 2));
            data.Add(new PersonnageGUI(true, 150, 50, 1));
            data.Add(new PersonnageGUI(false, 150, 25, 0));
            data.Add(new PersonnageGUI(true, 150, 1, 2));

            data.Add(new PersonnageGUI(true, 150, 75, 2));
            data.Add(new PersonnageGUI(true, 150, 75, 2));

            DataContext = data;
        }
    }
}
