/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
[System.Serializable]
public class ListWrapper<T>
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

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/

    /********  INSPECTOR        ************************/

    [SerializeField] protected List<T> m_list;

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    // Contructor
    public ListWrapper() : this(new List<T>())
    {
    }
    // Contructor with parameter
    public ListWrapper(List<T> p_init)
    {
        m_list = p_init;
    }

    // Getter by indice
    public virtual T At(int p_indice)
    {
        return m_list[p_indice];
    }

    // Add
    public virtual ListWrapper<T> Add(T p_element)
    {
        m_list.Add(p_element);

        return this;
    }

    public virtual ListWrapper<T> AddRange(IEnumerable<T> p_enumerable)
    {
        m_list.AddRange(p_enumerable);

        return this;
    }
    public virtual ListWrapper<T> AddRange(ListWrapper<T> p_listWrapper)
    {
        return AddRange(p_listWrapper.m_list);
    }

    // Remove
    public virtual ListWrapper<T> Remove(T p_element)
    {
        m_list.Remove(p_element);

        return this;
    }

    public virtual ListWrapper<T> RemoveRange(IEnumerable<T> p_enumerable)
    {
        foreach (T t in p_enumerable)
        {
            if (m_list.Contains(t))
            {
                Remove(t);
            }
        }
        //AddRange(p_enumerable);

        return this;
    }
    public virtual ListWrapper<T> RemoveRange(ListWrapper<T> p_listWrapper)
    {
        return RemoveRange(p_listWrapper.m_list);
    }

    // Clear
    public virtual ListWrapper<T> Clear()
    {
        m_list.Clear();

        return this;
    }

    /********  PROTECTED        ************************/

    // Getter by type
    protected virtual List<U> GetFromType<U>() where U : T
    {
        return m_list.FindAll(t => t is U) as List<U>;
    }

    /********  PRIVATE          ************************/

    #endregion
}
