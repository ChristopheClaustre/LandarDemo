#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using System;
using System.Collections.Generic;
#else
using System;
using System.Collections.Generic;
using System.Globalization;
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
#endif

namespace Noesis
{

    /// <summary>
    /// Logique d'interaction pour PersonnageIcon.xaml
    /// </summary>
    public partial class PersonnageIcon : UserControl
    {
        public PersonnageIcon()
        {
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Local/Expedition/PersonnageIcon.xaml");
		}
#endif
    }
}
