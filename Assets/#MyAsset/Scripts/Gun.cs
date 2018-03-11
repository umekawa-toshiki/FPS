using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    private string name = "";   //銃の名前
    private int attack = 0;   //威力
    private int bulletnumber = 0; //残弾
    private float delay = 0;    //次に弾を撃つまでの間隔
    private bool have;
    [SerializeField]
    private Transform muzzle;

    //初期値代入
    public void setGunInfo(string name,int attack,int bulletnumber,float delay,bool have)
    {
        this.name = name;
        this.attack = attack;
        this.bulletnumber = bulletnumber;
        this.delay = delay;
        this.have = have;
    }

    public string Name
    {
        get
        {
            return name;
        }
        
    }

    public int Attack
    {
        get
        {
            return attack;
        }
    }

    public int Bulletnumber
    {
        get
        {
            return bulletnumber;
        }
       
    }

    public float Delay
    {
        get
        {
            return delay;
        }
        
    }
    public Transform Muzzle
    {
        get
        {
            return muzzle;
        }
    }

    public bool Have
    {
        set{
            have = value;
        }
        get
        {
            return have;
        }
    }
}
