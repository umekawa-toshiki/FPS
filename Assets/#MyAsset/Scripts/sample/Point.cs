using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum Etype
    {
        Item,
        Magic,
        Trap,
    }

    [SerializeField]
    private Etype m_type = default(Etype);
    [SerializeField, Range(0.0f, 10.0f)]
    private float m_radius = 0.0f;

    public Etype Type
    {
        get { return m_type; }
    }
    public float Radius
    {
        get { return m_radius; }
    }
}
