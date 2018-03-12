using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人物クラス
public class Person : MonoBehaviour {

    // 体力
    protected int hp;
    // 移動速度
    protected float speed;

    // 各プロパティ
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
