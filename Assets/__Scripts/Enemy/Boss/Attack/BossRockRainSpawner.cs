using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BossRockRainSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool<BossBulletFallRock> m_SmallRockPool;

    private Transform m_Target;
    private float timer = 0f;
    private float interval = 0.3f; // 실행 간격(초)
    void Start()
    {
        m_SmallRockPool.Initialize();
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;

    }
    // Start is called before the first frame update
    public void SetInit(Transform trans)
    {
        m_Target = trans;
    }


    void Update()
    {
        //플레이어 따라서 이동하도록
        float y = transform.position.y;
        Vector3 rainPos = m_Target.position;
        rainPos.y = y;
        transform.position = rainPos;

        //하늘에서 떨어지는 돌을 랜덤으로 적용

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            
            float x = Random.Range(transform.position.x -5,transform.position.x +5);
            float z = Random.Range(transform.position.z - 5,transform.position.z + 5);
            Vector3 pos =  new Vector3(x, transform.position.y, z);
            Debug.Log("MakeRock");
            BossBulletFallRock clone;
            m_SmallRockPool.GetObject(out clone);
            clone.SetPosition(pos);
            clone.SetInit(this);
            timer = 0f;
        }


    }
    public void RemoveStone(BossBulletFallRock clone)
    {
        clone.ResetData();
        m_SmallRockPool.PutInPool(clone);
    }

}
