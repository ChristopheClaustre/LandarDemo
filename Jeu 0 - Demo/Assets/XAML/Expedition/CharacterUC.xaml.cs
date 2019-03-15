#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
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
    /// Logique d'interaction pour CharacterUC.xaml
    /// </summary>
    public partial class CharacterUC : UserControl
    {
        public CharacterUC()
        {
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/XAML/Expedition/CharacterUC.xaml");
		}
#endif
    }
}
