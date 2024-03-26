using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�� ��ü�� �����ϴ� ��ũ��Ʈ
public class BossRockLauncher : MonoBehaviour
{
    [SerializeField] private BossEnemy m_BossEnemy;
    [SerializeField]private GameObject m_goMissile;
    [SerializeField]private Transform[] m_transform;

    [SerializeField] private BossRockRainSpawner m_bossRain;
    private Transform m_target;
    private float m_fDamage;
    [SerializeField] private ObjectPool<BossThrowRock> m_BossRockPool;
    [SerializeField] private BossFootAttack m_bossFootAttack;
    void Start()
    {
        m_BossRockPool.Initialize();
        m_fDamage = 30.0f;
    }
    public void SetTarget(Transform trans)
    {
        m_target = trans;
    }
    // Update is called once per frame
    public void FireBossRock()
    {

        foreach (Transform t in m_transform)
        {
            BossThrowRock clone;

            m_BossRockPool.GetObject(out clone);         //������Ʈ ������
            clone.SetPosition(t.position);              //��ġ ����
            clone.SetInitData(m_target, m_fDamage,this);
            float speed = Random.Range(0.5f, 1.5f);
            clone.GetComponent<Rigidbody>().velocity = Vector3.up * speed;
            StartCoroutine(RemoveDelay(clone));

        }
    }
    IEnumerator RemoveDelay(BossThrowRock clone)
    {
        yield return new WaitForSeconds(5.0f);  //�����̰� �÷��̾ ���󰡴� �ð�
        m_BossEnemy.EndAttack();
        clone.RemoveStone();
    }

    public void Remove(BossThrowRock clone)
    {
        clone.ResetData();
        m_BossRockPool.PutInPool(clone);
        
    }

    public void FireBossRockRain()
    {
        m_bossRain.gameObject.SetActive(true);
        Vector3 pos = m_target.position;
        pos.y = m_target.position.y+15.0f;
        m_bossRain.SetPosition(pos);
        m_bossRain.SetInit(m_target.transform);
        StartCoroutine(WaitForDisable());


    }
    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(5.0f);
        m_BossEnemy.EndAttack();
        m_bossRain.gameObject.SetActive(false);
    }
    public void FireFootAttack()
    {
        m_bossFootAttack.gameObject.SetActive(true);
        m_bossFootAttack.SetPosition(m_BossEnemy.transform.position);
        m_bossFootAttack.Run(m_target);
    }
    public void FootAttackEnd()
    {
        m_bossFootAttack.ResetData();
        
        m_bossFootAttack.gameObject.SetActive(false);
        m_BossEnemy.EndAttack();
    }
}
