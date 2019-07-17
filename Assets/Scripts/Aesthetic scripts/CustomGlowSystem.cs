using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class CustomGlowSystem
{
    static CustomGlowSystem m_Instance; // singleton
    static public CustomGlowSystem instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new CustomGlowSystem();
            return m_Instance;
        }
    }

    internal HashSet<CustomGlowObj> m_GlowObjs = new HashSet<CustomGlowObj>();

    public void Add(CustomGlowObj o)
    {
        Remove(o);
        m_GlowObjs.Add(o);
        Debug.Log("added effect " + o.gameObject.name);
    }

    public void Remove(CustomGlowObj o)
    {
        m_GlowObjs.Remove(o);
        Debug.Log("removed effect " + o.gameObject.name);
    }
}