using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * MP5 クラス
 * Gun クラスのサブクラス
 */
public class MP5 : Gun {
    
    // インスタンスの有効後、実行される
    void Start ()
    {
        // MP5 の情報を設定 
        setGunInfo("MP5", 10, 40, 0.05f, false);
    }
}
