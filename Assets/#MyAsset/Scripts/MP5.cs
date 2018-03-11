using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5 : Gun {
    //コンストラクタの継承
    public MP5()
    {
        setGunInfo("MP5", 10, 40, 0.05f, false);
    }
}
