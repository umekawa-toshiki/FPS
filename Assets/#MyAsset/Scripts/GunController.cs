

<<<<<<< HEAD
=======

public class GunController : MonoBehaviour {
    
    // 弾のprefab
    public GameObject bullet;
    // 弾の発射場所
    public Transform Muzzle;
    // 弾の速度
    public float bulletspeed = 1000;
    // 弾数表示
    private Text Bulletcount;
    private Text Gunname;
    // 打った数を数える
    private int bulletcount;

    // 時間経過を保存
    private float timeOut;
    private float timeTrigger;

    // 銃の種類
    Gun gunType;

    void Start () {
        
    }
	
    
	void Update () {
       
    }
    
}
>>>>>>> 24d6038f9cd7fc152fbc0a523ddd30de3f684801
