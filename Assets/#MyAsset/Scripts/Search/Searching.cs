using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : MonoBehaviour {
    // 対象者を見つけたり見失ったりしたら、イベントを返す
    public event System.Action<GameObject> OnFound = (obj) => { };
    public event System.Action<GameObject> OnLost = (obj) => { };

    //索敵角度
    [SerializeField]
    private float searchAngle = 90.0f;
    //コライダーの宣言
    private SphereCollider sphereCollider = null;

    private void Awake()
    {
        //インスペクター上のコライダーを格納
        sphereCollider = GetComponent<SphereCollider>();
    }
    //コライダーと接触した時
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        OnFound(enterObject);
    }
    //コライダーから離れた時
    private void OnTriggerExit(Collider other)
    {
        GameObject exitObject = other.gameObject;
        OnLost(exitObject);
    }
}
