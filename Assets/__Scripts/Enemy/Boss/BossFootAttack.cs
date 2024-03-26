using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class BossFootAttack : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_StartPosition;

    [SerializeField] private BossRockLauncher m_bossRockLauncher;
    private float m_fdefaultPositionY;
    private float m_fSpeed = 10;
    [SerializeField] private ObjectPool<BossRockBullet> m_bossRockPool;
    
    void Start()
    {
        m_bossRockPool.Initialize();
        m_bossRockPool.SettingParent(transform);
    }
    IEnumerator FootAttackRocksUp()
    {
        yield return new WaitForSeconds(1.7f);
        int count = 0;
        
        while (true)
        {
            BossRockBullet clone;
            m_bossRockPool.GetObject(out clone);

            Vector3 pos = (m_target.position - m_StartPosition).normalized* count; 
            Debug.Log(pos);
            clone.SetPosition(m_StartPosition + pos);
            Debug.Log(m_StartPosition);

            StartCoroutine(OneRockRisingUp(clone.gameObject));
            StartCoroutine(RemoveDisable(clone));
            count++;
            if (count > 15)
                yield break;
            yield return new WaitForSeconds(0.2f);
        
        }

        yield return new WaitForSeconds(1.0f);

        m_bossRockLauncher.FootAttackEnd();
    }
    public void ResetData()
    {
      
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
        m_StartPosition = transform.position;
        m_fdefaultPositionY = m_StartPosition.y-0.5f    ;
    }
    public void Run(Transform target)
    {
        m_target = target;
        m_StartPosition = transform.position;
        m_fdefaultPositionY = m_StartPosition.y - 5.0f;
        StartCoroutine(FootAttackRocksUp());
    }
    IEnumerator OneRockRisingUp(GameObject obj)
    {
        while (obj.transform.position.y< m_fdefaultPositionY + 2.0f) 
        {
            Vector3 velo = Vector3.zero;
            velo.y = m_fSpeed * 0.01f;
            obj.transform.position += velo;
            yield return null;
        }
    }
    IEnumerator RemoveDisable(BossRockBullet clone)
    {
        yield return new WaitForSeconds(2.0f);
        m_bossRockPool.PutInPool(clone);
    }
}
