using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : Gun {
    Gun gun;
    //弾のprefab
    public GameObject bullet;

    //弾の発射場所
    public Transform Muzzle;

    //弾の速度
    public float speed = 1000;

    // Use this for initialization
    void Start () {
        gun = GetComponent<Gun>();
        gun.SetGunInfo("SCAR", 20, 30, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
