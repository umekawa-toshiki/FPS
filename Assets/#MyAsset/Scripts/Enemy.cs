using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SearchingBehaviorを使用するスクリプト
// SearchingBehaviorで何かを見つけたら色が変化
public class Enemy : MonoBehaviour {
    [SerializeField]
    private Material m_defaultMaterial = null;
    [SerializeField]
    private Material m_foundMaterial = null;
        
    // マテリアルをいじっている？
    private Renderer m_renderer = null;
    // リストの宣言
    private List<GameObject> m_targets = new List<GameObject>();


    private void Awake()
    {
        m_renderer = GetComponentInChildren<Renderer>();

        var searching = GetComponentInChildren<SearchingBehavior>();
        searching.OnFound += OnFound;
        searching.OnLost += OnLost;
    }

    // 見つけた時
    private void OnFound(GameObject i_foundObject)
    {
        m_targets.Add(i_foundObject);
        m_renderer.material = m_foundMaterial;
    }

    // 見失ったとき
    private void OnLost(GameObject i_lostObject)
    {
        m_targets.Remove(i_lostObject);

        if (m_targets.Count == 0)
        {
            m_renderer.material = m_defaultMaterial;
        }
    }
}
