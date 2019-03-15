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
    public class PersonnageGUI
    {
#if NOESIS
        private PersonnageScript m_personnageScript;
#else
        private bool m_isSelected = false;
        private int m_maxLife = 150;
        private int m_life = 150;

        private int m_type = 0;
        // - 0: soldier
        // - 1: engineer
        // - 2: worker
#endif

#if NOESIS
        public bool Selected
        {
            get
            {
                return m_personnageScript.Selected;
            }
            set
            {
                m_personnageScript.Selected = value;
            }
        }

        public int LifeInPercent
        {
            get
            {
                float vie = m_personnageScript.Perso.Vie;
                int max = 100;//m_personnageScript.Perso.Carac.VieMax;
                float percent = vie / max;

                return Mathf.RoundToInt(percent * 100);
            }
        }

        public bool IsASoldier
        {
            get
            {
                return false;
            }
        }

        public bool IsAnEngineer
        {
            get
            {
                return true;
            }
        }

        public bool IsAWorker
        {
            get
            {
                return false;
            }
        }
#else
        public bool Selected
        {
            get
            {
                return m_isSelected;
            }
            set
            {
                m_isSelected = value;
            }
        }

        public int LifeInPercent
        {
            get
            {
                return (int)((float)m_life / m_maxLife * 100);
            }
        }

        public bool IsASoldier
        {
            get
            {
                return m_type == 0;
            }
        }

        public bool IsAnEngineer
        {
            get
            {
                return m_type == 1;
            }
        }

        public bool IsAWorker
        {
            get
            {
                return m_type == 2;
            }
        }
#endif

#if NOESIS
        public PersonnageGUI(PersonnageScript p_personnageScript)
        {
            m_personnageScript = p_personnageScript;
        }
#else
        public PersonnageGUI(bool p_isSelected, int p_maxLife, int p_life, int p_type)
        {
            m_isSelected = p_isSelected;
            m_maxLife = p_maxLife;
            m_life = p_life;
            m_type = p_type;
        }
#endif
    }

    /// <summary>
    /// Logique d'interaction pour PersonnageUserControl.xaml
    /// </summary>
    public partial class PersonnageUserControl : UserControl
    {
        public PersonnageUserControl()
        {
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/XAML/Expedition/PersonnageUserControl.xaml");
		}
#endif
    }
}
