using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Person {
    public GameObject Player;
    public GameObject Camera;

    private bool have;

    // マウスの感度
    public float sensitive = 1.0F;
    // マウス移動
    private Transform CameraTransform;
    private Transform GunTransform;
    private Transform GunTransform_ADS;

    // 弾のprefab
    public GameObject bullet;
    // 弾の速度
    public float bulletSpeed = 1000;
    // 弾数表示
    private Text BulletCount;
    private Text GunName;
    // 打った数を数える
    private int bulletCount;
    // 時間経過を保存
    private float timeOut;
    private float timeTrigger;

    // 銃の種類を取得
    Gun gunType;
    GameObject gun;

    // Use this for initialization
    void Start()
    {
        // コンポーネントの取得
        CameraTransform = this.gameObject.transform.Find("Main Camera");
        GunTransform = this.gameObject.transform.Find("Gun");
        GunTransform_ADS = CameraTransform.transform.Find("Gun_ADS");

        // Text
        BulletCount = GameObject.Find("Canvas/BulletCount").GetComponent<Text>();
        GunName = GameObject.Find("Canvas/GunName").GetComponent<Text>();
        
<<<<<<< HEAD
        //移動速度
=======

        // 移動速度
>>>>>>> 24d6038f9cd7fc152fbc0a523ddd30de3f684801
        Speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // 視点移動
        float X_Rotation = sensitive * Input.GetAxis("Mouse X");
        float Y_Rotation = sensitive * Input.GetAxis("Mouse Y");
        transform.Rotate(0, X_Rotation, 0);
        CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
        // 銃の向き変更
        GunTransform.transform.Rotate(-Y_Rotation, 0, 0);
        
        if(have == true) {
            gun.transform.Rotate(-Y_Rotation, 0, 0);
        }
        

        // 移動する向きの角度の計算
        // Math...一般的な数学関数を扱う
        // Mathf.PI...円周率
        // transform.eulerAngles.y...オイラー核としての角度
        float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));

        // WASDで移動
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += dir1 * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += dir2 * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += -dir2 * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -dir1 * Speed * Time.deltaTime;
        }
        // 走る
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 5;
        }
        else
        {
            Speed = 3;
        }

        //ADS
        if (Input.GetMouseButton(1))
        {
            gun.transform.position = GunTransform.position;
            gun.transform.rotation = CameraTransform.rotation;
        }


        // 弾を撃つ処理
        if (Input.GetMouseButton(0))
        {
            // 残弾がなくなったら
            if (bulletCount < 1)
                return;
            // 連射速度
            if (Time.time > timeTrigger)
            {
                // 撃つ
                Shot();
                // 次の弾を撃つまでの時間
                timeTrigger = Time.time + timeOut;

                // 残弾数を減らす
                bulletCount -= 1;
                BulletCount.text = "残弾数：" + bulletCount;
            }
        }

        // リロード
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletCount = gunType.Bulletnumber;
            BulletCount.text = "残弾数：" + bulletCount;
        }
    }

    // 銃を拾う時
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Gun")
        {

            gun = collision.gameObject;
            gunType = gun.GetComponent<Gun>();

            // 銃の位置を決める
            gun.transform.position = GunTransform.position;
            gun.transform.rotation = CameraTransform.rotation;

            gun.transform.parent = Player.transform;
            have = true;

            bulletCount = gunType.Bulletnumber;
            BulletCount.text = "残弾数：" + bulletCount;
            GunName.text = gunType.Name;
            timeOut = gunType.Delay;
        }
    }

    // 弾を撃つ
    void Shot()
    {
        // 弾の複製
        GameObject bullets = GameObject.Instantiate(bullet, gunType.Muzzle.transform.position, gunType.Muzzle.transform.rotation);
        // 弾に力を加える
        Rigidbody bulletsRigidbody = bullets.GetComponent<Rigidbody>();
        bulletsRigidbody.AddForce(gunType.Muzzle.transform.forward * bulletSpeed);
        // 弾の削除 2秒後
        Destroy(bullets, 2.0f);
    }
}