using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    //人物のスーパークラス

    //体力
    protected int hp;
    //移動速度
    protected float speed;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
}
