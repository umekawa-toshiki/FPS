﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    private string name = "";   //銃の名前
    private int attack = 0;   //威力
    private int bulletnumber = 0; //残弾
    private float delay = 0;    //次に弾を撃つまでの間隔

    //コンストラクタ
    public Gun(string name,int attack,int bulletnumber,float delay)
    {
        this.name = name;
        this.attack = attack;
        this.bulletnumber = bulletnumber;
        this.delay = delay;
    }

    //getter
    public string getName()
    {
        return name;
    }
    public int getAttack()
    {
        return attack;
    }
    public int getBulletnumber()
    {
       return bulletnumber;
    }

    public float getDelay()
    {
        return delay;
    }
}
