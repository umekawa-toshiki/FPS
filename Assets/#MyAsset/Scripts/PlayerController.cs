using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject Player;
    public GameObject Camera;
    private float speed;

    Person player = new Person();
    public GameObject gun;
    

    //マウスの感度
    public float sensitive = 1.0F;

    private Transform PlayerTransform;
    private Transform CameraTransform;
    private Transform GunTransform;

    

    // Use this for initialization
    void Start()
    {
        //コンポーネントの取得
        PlayerTransform = GetComponent<Transform>();
        CameraTransform = this.gameObject.transform.Find("Main Camera").gameObject.GetComponent<Transform>();
        GunTransform = this.gameObject.transform.Find("Gun").gameObject.GetComponent<Transform>();

        player.Speed = 3;
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
            PlayerTransform.transform.position += dir1 * player.Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.transform.position += dir2 * player.Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.transform.position += -dir2 * player.Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerTransform.transform.position += -dir1 * player.Speed * Time.deltaTime;
        }

        //走る
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.Speed = 5;
        }
        else
        {
            player.Speed = 3;
        }
    }

    //銃を拾う時
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Gun")
        {
            gun = collision.gameObject;
            
            gun.transform.position = GunTransform.position;
           
            gun.transform.parent = Player.transform;
        }
    }
}

