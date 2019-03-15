/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class CreationGoto :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/



    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/



    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/



    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /********  OUR MESSAGES     ************************/

    private void OnRightUp()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position[1] = 0; // reset altitude
        if (ClickValidator.Inst != null && ClickValidator.Inst.isOnBlackFoW(position))
            return;

        bool append = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (ExpeditionManager.Instance.Selected.Count > 1)
        {
            List<Vector3> expeditions;
            float shift = 0.25f;
            float radius = CreationGotoAPI.Inst.GetExpeditionRadius(ExpeditionManager.Instance.Selected.Count, shift);
            CreationGotoAPI.Inst.GetExpeditionPositions(position, shift, radius, ExpeditionManager.Instance.Selected.Count, out expeditions);

            Debug.Assert(ExpeditionManager.Instance.Selected.Count == expeditions.Count);

            int i = 0;
            foreach (int indice in ExpeditionManager.Instance.Selected)
            {
                AffectPosition(ExpeditionManager.Persos[indice], expeditions[i], append);
                ++i;
            }
        }
        else if (ExpeditionManager.Instance.Selected.Count == 1)
        {
            AffectPosition(ExpeditionManager.Persos[ExpeditionManager.Instance.Selected[0]], position, append);
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    private void AffectPosition(GameObject p_perso, Vector3 p_position, bool p_append)
    {
        Goto task = new Goto(p_position);
        p_perso.GetComponent<CharacterScript>().SetTask(task, p_append);
    }

    #endregion
}
