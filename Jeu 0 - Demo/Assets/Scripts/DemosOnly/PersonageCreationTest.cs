/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PersonageCreationTest :
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

    private int m_type = -1;
    private int m_typeRequested = 0;

    [System.Serializable]
    public struct TextureOverrideData
    {
        public List<Sprite> textures;
        public SpriteRenderer renderer;
    }

    [Header("Textures")]
    public List<TextureOverrideData> m_textureOverrideData;

    [Header("Animations")]
    public List<AnimatorOverrideController> m_animatorOverrideControllers;

    private Animator m_animator;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_type == m_typeRequested)
        {
            return;
        }

        // change textures
        foreach
            (var truc in m_textureOverrideData)
        {
            truc.renderer.sprite = truc.textures[m_typeRequested];
        }

        // change animations
        m_animator.runtimeAnimatorController = m_animatorOverrideControllers[m_typeRequested];

        // assign requested type
        m_type = m_typeRequested;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 30, 200, 30), "test of death"))
        {
            m_typeRequested = (m_typeRequested + 1) % 3;
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
