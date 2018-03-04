using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject Player;
    public GameObject Camera;
    private float speed;
    public float bulletspeed = 1000;

    //マウスの感度
    public float sensitive = 1.0F;

    private Transform PlayerTransform;
    private Transform CameraTransform;

    //弾のprefab
    public GameObject bullet;

    //弾の発射場所
    public Transform Muzzle;

    Gun gun;

    // Use this for initialization
    void Start()
    {
        //コンポーネントの取得
        PlayerTransform = transform.parent;
        CameraTransform = GetComponent<Transform>();
        gun = GetComponent<Gun>();
        gun.SetGunInfo("SCAR", 20, 30, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //視点移動
        float X_Rotation = sensitive * Input.GetAxis("Mouse X");
        float Y_Rotation = sensitive * Input.GetAxis("Mouse Y");
        PlayerTransform.transform.Rotate(0, X_Rotation, 0);
        CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);

        //移動する向きの角度の計算
        //Math...一般的な数学関数を扱う
        //Mathf.PI...円周率
        //transform.eulerAngles.y...オイラー核としての角度
        float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));

        //WASDで移動
        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.transform.position += dir1 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.transform.position += dir2 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.transform.position += -dir2 * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerTransform.transform.position += -dir1 * speed * Time.deltaTime;
        }

        //走る
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5;
        }
        else
        {
            speed = 3;
        }
        //弾を撃つ処理
        if (Input.GetMouseButton(0))
        {
            //弾の複製
            GameObject bullets = GameObject.Instantiate(bullet) as GameObject;

            Rigidbody bulletsRigidbody = bullets.GetComponent<Rigidbody>();
            bulletsRigidbody.AddForce(transform.forward * bulletspeed);

            //弾の位置を調整
            bullets.transform.position = Muzzle.position;
            //弾の消去

        }
    }
}

