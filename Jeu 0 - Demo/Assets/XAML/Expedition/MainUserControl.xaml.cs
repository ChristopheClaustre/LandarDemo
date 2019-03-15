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
    public class ExpeditionModeMVVM
    {
        private List<CharacterMVVM> m_list;

        public List<CharacterMVVM> List
        {
            get
            {
                return m_list;
            }
        }

        public bool TimePaused
        {
            get
            {
#if NOESIS
                return PauseTime.Paused;
#else
                return false;
#endif
            }
            set
            {
#if NOESIS
                PauseTime.Paused = value;
#endif
            }
        }

        public ExpeditionModeMVVM()
        {
            m_list = new List<CharacterMVVM>();
        }

        public void Add(CharacterMVVM p_personnage)
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
            ExpeditionModeMVVM data = new ExpeditionModeMVVM();

            foreach (GameObject go in ExpeditionManager.Persos)
            {
                data.Add(new CharacterMVVM(go.GetComponent<PersonnageScript>()));
            }

            DataContext = data;
#endif
        }
    }
}
