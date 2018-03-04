using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : Gun {
    //弾のprefab
    public GameObject bullet;
    //弾の発射場所
    public Transform Muzzle;
    //弾の速度
    public float bulletspeed = 1000;
    //弾数表示
    public Text Bulletcount;
    //打った数を数える
    private int bulletcount;

    //銃クラスのインスタンス生成
    Gun gun = new Gun();


    void Start () {
        SetGunInfo("SCAR", 20, 30, 1);
        bulletcount = gun.Bulletnumber;
        
        //Bulletcount.text = "残弾数：" + gun.Bulletnumber;
    }
	

	void Update () {
        //弾を撃つ処理
        if (Input.GetMouseButton(0))
        {
            if(bulletcount < 1)
                return;

            Shot();

            bulletcount -= 1;
        }
    }

    //弾を撃つ
    void Shot()
    {
        //弾の複製
        GameObject bullets = GameObject.Instantiate(bullet) as GameObject;
        //弾に力を加える
        Rigidbody bulletsRigidbody = bullets.GetComponent<Rigidbody>();
        bulletsRigidbody.AddForce(transform.forward * bulletspeed);
        //弾の位置を調整
        bullets.transform.position = Muzzle.position;
        //弾の削除　2秒後
        Destroy(bullets, 2.0f);
    }
}
