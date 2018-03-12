using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCAR クラス
 * Gun クラスのサブクラス
 */
public class SCAR : Gun {
    
    // インスタンスの有効後、実行される
    public SCAR()
    {
        // SCAR の情報を設定 
        setGunInfo("SCAR", 20, 30, 0.1f, false);
    }
    
}
