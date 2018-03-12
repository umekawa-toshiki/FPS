using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 銃クラス
public class Gun : MonoBehaviour {
    
    // 銃の名前
    private string name = "";

    // 威力
    private int power = 0;

    // 残弾
    private int bulletNumber = 0;

    // 次に弾を撃つまでの間隔 
    private float delay = 0;

    // 銃が持たれていれば true
    private bool isHaved = null;

    [SerializeField]
    private Transform muzzle;

    // 銃の情報を一括で設定するメソッド
    public void setGunInfo(string name, int power, int bulletNumber, float delay, bool isHaved)
    {
        this.name = name;
        this.power = power;
        this.bulletNumber = bulletNumber;
        this.delay = delay;
        this.isHaved = isHaved;
    }

	// 各プロパティ
    public string Name
    {
		set { name = value; }
        get { return name; }
    }
    public int Power
    {
		set { power = value; }
        get { return power; }
    }
    public int Bulletnumber
    {
		set { bulletNumber = value; }
        get { return bulletNumber; }
    }

    public float Delay
    {
		set { delay = value; }
        get { return delay; }
    }
    public Transform Muzzle
    {
		set { muzzle = value; }
        get { return muzzle; }
    }
    public bool IsHaved
    {
        set { isHaved = value; }
        get { return isHaved; }
    }
}
