#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using System;
using System.Collections.Generic;
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
    public class PersonnagesGUI
    {
        private List<PersonnageGUI> m_list;

        public List<PersonnageGUI> List
        {
            get
            {
                return m_list;
            }
        }

        public PersonnagesGUI()
        {
            m_list = new List<PersonnageGUI>();
        }

        public void Add(PersonnageGUI p_personnage)
        {
            m_list.Add(p_personnage);
        }
    }

    /// <summary>
    /// Logique d'interaction pour MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
			this.Initialized += OnInitialized;
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/XAML/Expedition/MainUserControl.xaml");
		}
#endif

        private void OnInitialized(object sender, EventArgs args)
        {
#if NOESIS
            PersonnagesGUI data = new PersonnagesGUI();

            foreach (GameObject go in ExpeditionManager.Persos)
            {
                data.Add(new PersonnageGUI(go.GetComponent<PersonnageScript>()));
            }

            DataContext = data;
#endif
        }
    }
}
