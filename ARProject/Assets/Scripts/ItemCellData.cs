using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 자신만의 셀 데이터를 정의하자.
public class ItemCellData : IReuseCellData {

    #region IReuseCellData
    public int m_Index;
    public int Index {
        get {
            return m_Index;
        }
        set {
            m_Index = value;
        }
    }
    public string m_ImgName;
    public string ImgName
    {
        get
        {
            return m_ImgName;
        }
        set
        {
            m_ImgName = value;
        }
    }
    public int m_damage;
    public int damage
    {
        get
        {
            return m_damage;
        }
        set
        {
            m_damage = value;
        }
    }
    public int m_num;
    public int num
    {
        get
        {
            return m_num;
        }
        set
        {
            m_num = value;
        }
    }
    //public string Value;
    public int m_rank;
    public int rank
    {
        get
        {
            return m_rank;
        }
        set
        {
            m_rank = value;
        }
    }
    public Element m_element;
    public Element element
    {
        get
        {
            return m_element;
        }
        set
        {
            m_element = value;
        }
    }
    public int m_cost;
    public int cost
    {
        get
        {
            return m_cost;
        }
        set
        {
            m_cost = value;
        }
    }
    public bool m_check;
    public bool check
    {
        get
        {
            return m_check;
        }
        set
        {
            m_check = value;
        }
    }
    #endregion

    // user data

}