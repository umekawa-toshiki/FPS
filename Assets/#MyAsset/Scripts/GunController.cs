using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunController : MonoBehaviour {
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

    public float timeOut;
    private float timeTrigger;

    Gun gun = new Gun();


    void Start () {
        
        gun.SetGunInfo("SCAR", 20, 30, 1);
        bulletcount = gun.getBulletnumber();
        Bulletcount.text = "残弾数：" + bulletcount;
    }
	

	void Update () {
        

        //弾を撃つ処理
        if (Input.GetMouseButton(0))
        {
            //残弾がなくなったら
            if(bulletcount < 1)
                return;
            //連射速度
            if (Time.time > timeTrigger)
            {
                //撃つ
                Shot();
                timeTrigger = Time.time + timeOut;
                bulletcount -= 1;
                Bulletcount.text = "残弾数：" + bulletcount;
            }
            
            

            
        }
        //リロード
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletcount = gun.getBulletnumber();
            Bulletcount.text = "残弾数：" + bulletcount;
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
