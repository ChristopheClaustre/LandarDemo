/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Obsolete("Destination system is deprecated.")]
[System.Serializable]
public class Destination
{
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    public Vector2 Cible
    {
        get { return m_cible; }
        set { m_cible = value; }
    }
    public float OrientationFinale
    {
        get
        {
            return m_orientationFinale;
        }
        set
        {
            m_orientationFinale = MyMathf.PosModulo(value, 360);
        }
    }

    /********  PROTECTED        ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private Vector2 m_cible;
    private float m_orientationFinale;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    // constructeur
    public Destination(Vector2 _c, float _o = float.NaN)
    {
        m_orientationFinale = (float.IsNaN(_o)) ? _o : MyMathf.PosModulo(_o, 360);
        m_cible = _c;
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
