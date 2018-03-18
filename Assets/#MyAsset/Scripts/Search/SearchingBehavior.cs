using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 索敵処理を記述するスクリプト
public class SearchingBehavior : MonoBehaviour {

    // 対象者を見つけたり見失ったりしたら、イベントを返す
    public event System.Action<GameObject> OnFound = (obj) => { };
    public event System.Action<GameObject> OnLost = (obj) => { };

    // SphereColliderに触れた時
    private void OnTriggerEnter(Collider i_other)
    {
        GameObject enterObject = i_other.gameObject;
        OnFound(enterObject);
    }

    // SphereColliderから離れた時
    private void OnTriggerExit(Collider i_other)
    {
        GameObject exitObject = i_other.gameObject;
        OnLost(exitObject);
    }
}
