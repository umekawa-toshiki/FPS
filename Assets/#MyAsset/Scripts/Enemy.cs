using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// SearchingBehaviorを使用するスクリプト
// SearchingBehaviorで何かを見つけたら色が変化
public class Enemy : Person
{
    // 敵の状態を列挙
    enum EnemyState{
        Walk,
        Active,
        Dead
    };
    // 敵の状態
    private EnemyState state;
    // プレイヤーの位置
    private Transform playerTransform;

    // 検知用
    [SerializeField]
    private Material defaultMaterial = null;
    [SerializeField]
    private Material foundMaterial = null;
    // マテリアルをいじるために使用
    private new Renderer renderer = null;
    // リストの宣言
    private List<GameObject> targets = new List<GameObject>();

    //巡回用
    //目的地の格納場所
    public Transform[] target;
    //次の目的地の番号
    private int nextPoint = 0;
    //NavMesh
    private NavMeshAgent agent;

    // 弾を撃つ処理
    // 銃クラスの取得
    Gun enemyGun;
    // 弾のprefab
    public GameObject enemyBullet;
    // 弾の速度
    public float bulletSpeed = 300;
    // 時間経過を保存
    private float timeOut = 0.2f;
    private float timeTrigger;
    // 敵が次の方向に動くための時間
    private float timeEnemy = 2.0f;
    


    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        //子オブジェクトからSearchingを格納
        var searching = GetComponentInChildren<Searching>();
        searching.OnFound += OnFound;
        searching.OnLost += OnLost;

        enemyGun = GetComponentInChildren<Gun>();

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        Roop();

        SetState("walk");
    }

    // 見つけた時
    private void OnFound(GameObject i_foundObject)
    {
        targets.Add(i_foundObject);
        renderer.material = foundMaterial;
        SetState("active");  
    }

    // 見失ったとき
    private void OnLost(GameObject i_lostObject)
    {
        targets.Remove(i_lostObject);
        agent.destination = target[nextPoint].position;
        if (targets.Count == 0)
        {
            renderer.material = defaultMaterial;
            SetState("walk");
        }
    }
    private void Update()
    {
        // 索敵中
        if(state == EnemyState.Walk)
        {
            if (agent.remainingDistance < 0.5f)
            {
                Roop();
                Debug.Log(nextPoint);
            }
                
        }
        //プレイヤー発見時
        else if (state == EnemyState.Active)
        {
            agent.destination = playerTransform.position;
            // 攻撃
            if (Time.time > timeTrigger)
            {
                // 撃つ
                Shot();
                // 次の弾を撃つまでの時間
                timeTrigger = Time.time + timeOut;
            }
        }
    }

    // 弾を撃つ
    void Shot()
    {
        // 弾の複製
        GameObject bullets = GameObject.Instantiate(enemyBullet, enemyGun.Muzzle.transform.position, enemyGun.Muzzle.transform.rotation);
        // 弾に力を加える
        Rigidbody bulletsRigidbody = bullets.GetComponent<Rigidbody>();
        bulletsRigidbody.AddForce(enemyGun.Muzzle.transform.forward * bulletSpeed);
        // 弾の削除 2秒後
        Destroy(bullets, 2.0f);
    }
    // 敵キャラクターの状態変更メソッド
    private void SetState(string mode)
    {
        if(mode == "walk")
        {
            state = EnemyState.Walk;
        }
        else if(mode == "active")
        {
            state = EnemyState.Active;
        }
        else if(mode == "dead")
        {
            state = EnemyState.Dead;
        }
    }
    //巡回用
    private void Roop()
    {
        if (target.Length == 0)
        {
            return;
        }

        agent.destination = target[nextPoint].position;
        nextPoint = (nextPoint + 1) % target.Length;
    }
}
