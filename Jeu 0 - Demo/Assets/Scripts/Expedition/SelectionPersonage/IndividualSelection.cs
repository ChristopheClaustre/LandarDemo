/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class IndividualSelection :
    MonoBehaviour
{
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    /********  OUR MESSAGES     ************************/

    void OnMouseUpAsButton()
    {
        if (!Input.GetMouseButton(1))
        {
            ExpeditionManager.Instance.monoSelection(ExpeditionManager.Persos.IndexOf(this.gameObject));
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
