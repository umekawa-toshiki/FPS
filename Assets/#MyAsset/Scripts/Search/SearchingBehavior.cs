using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 索敵処理を記述するスクリプト
public class SearchingBehavior : MonoBehaviour {

    // 対象者を見つけたり見失ったりしたら、イベントを返す
    public event System.Action<GameObject> OnFound = (obj) => { };
    public event System.Action<GameObject> OnLost = (obj) => { };

    // 索敵角度の宣言
    [SerializeField, Range(0.0f, 360.0f)]
    private float m_searchAngle = 0.0f;
    // コライダーの格納場所
    private SphereCollider m_sphereCollider = null;
    // cosθの宣言
    private float m_searchCosTheta = 0.0f;
    // 見つけた・見失ったかの判定を入れるList
    private List<FoundData> m_foundList = new List<FoundData>();


    // 索敵範囲を狭める
    // 索敵範囲を決める

    // SphereColliderの取得
    private void Awake()
    {
        m_sphereCollider = GetComponent<SphereCollider>();
        ApplySearchAngle();
    }
    private void Update()
    {
        UpdateFoundObject();
    }

    // リストのクリア
    private void OnDisable()
    {
        m_foundList.Clear();
    }
    // SphereColliderに触れた時
    private void OnTriggerEnter(Collider i_other)
    {
        GameObject enterObject = i_other.gameObject;

        // 念のため多重登録されないようにする。
        if (m_foundList.Find(value => value.Obj == enterObject) == null)
        {
            m_foundList.Add(new FoundData(enterObject));
        }
    }
    // SphereColliderから離れた時
    private void OnTriggerExit(Collider i_other)
    {
        GameObject exitObject = i_other.gameObject;

        var foundData = m_foundList.Find(value => value.Obj == exitObject);
        if (foundData == null)
        {
            return;
        }

        if (foundData.IsCurrentFound())
        {
            OnLost(foundData.Obj);
        }

        m_foundList.Remove(foundData);
    }
    
    // 角度を返す
    public float SearchAngle
    {
        get { return m_searchAngle; }
    }
    // 半径を返す
    public float SearchRadius
    {
        get
        {
            if (m_sphereCollider == null)
            {
                m_sphereCollider = GetComponent<SphereCollider>();
            }
            return m_sphereCollider != null ? m_sphereCollider.radius : 0.0f;
        }
    }
    
    

    // シリアライズされた値がインスペクター上で変更されたら呼ばれます。
    private void OnValidate()
    {
        ApplySearchAngle();
    }

    private void ApplySearchAngle()
    {
        float searchRad = m_searchAngle * 0.5f * Mathf.Deg2Rad;
        m_searchCosTheta = Mathf.Cos(searchRad);
    }
    
    // 見つけた・見失ったを判定する処理
    private void UpdateFoundObject()
    {
        foreach (var foundData in m_foundList)
        {
            GameObject targetObject = foundData.Obj;
            if (targetObject == null)
            {
                continue;
            }

            bool isFound = CheckFoundObject(targetObject);
            foundData.Update(isFound);

            if (foundData.IsFound())
            {
                OnFound(targetObject);
            }
            else if (foundData.IsLost())
            {
                OnLost(targetObject);
            }
        }
    }
    // オブジェクトを見つけた時の処理
    private bool CheckFoundObject(GameObject i_target)
    {
        // 位置の保存
        Vector3 targetPosition = i_target.transform.position;
        Vector3 myPosition = transform.position;
        // ベクトルの乗算
        Vector3 myPositionXZ = Vector3.Scale(myPosition, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 targetPositionXZ = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));
        // normalized...magnitudeを1としたベクトル（読み取り専用　magnitude...ベクトルの長さを返す
        Vector3 toTargetFlatDir = (targetPositionXZ - myPositionXZ).normalized;
        Vector3 myForward = transform.forward;
        if (!IsWithinRangeAngle(myForward, toTargetFlatDir, m_searchCosTheta))
        {
            return false;
        }

        return true;
    }
    // 角度を返す処理
    private bool IsWithinRangeAngle(Vector3 i_forwardDir, Vector3 i_toTargetDir, float i_cosTheta)
    {
        // 方向ベクトルが無い場合は、同位置にあるものだと判断する。
        if (i_toTargetDir.sqrMagnitude <= Mathf.Epsilon)
        {
            return true;
        }
        // 内積
        float dot = Vector3.Dot(i_forwardDir, i_toTargetDir);
        return dot >= i_cosTheta;
    }



    // 角度範囲計算処理はこらーだー内にいるオブジェクトを毎回判定する必要がある
    // つまり、コライダー内にいるオブジェクトを覚えておく必要がある
    // 発見状態を管理するクラス
    private class FoundData
    {
        public FoundData(GameObject i_object)
        {
            m_obj = i_object;
        }

        private GameObject m_obj = null;
        private bool m_isCurrentFound = false;
        private bool m_isPrevFound = false;

        public GameObject Obj
        {
            get { return m_obj; }
        }

        public Vector3 Position
        {
            get { return Obj != null ? Obj.transform.position : Vector3.zero; }
        }

        public void Update(bool i_isFound)
        {
            m_isPrevFound = m_isCurrentFound;
            m_isCurrentFound = i_isFound;
        }

        public bool IsFound()
        {
            return m_isCurrentFound && !m_isPrevFound;
        }

        public bool IsLost()
        {
            return !m_isCurrentFound && m_isPrevFound;
        }

        public bool IsCurrentFound()
        {
            return m_isCurrentFound;
        }
    }
}
