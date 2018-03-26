using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Gun : Gun {
    void start()
    {
        // 敵の武器の情報を設定
        setGunInfo("EnemyGun", 20, 1000, 1.0f, true);
    }
}
