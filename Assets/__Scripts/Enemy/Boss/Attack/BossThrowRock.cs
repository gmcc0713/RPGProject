using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
//보스가 던지는 돌
public class BossThrowRock : BossRockBullet,IPoolingObject
{
    private bool m_bDisableRunning;
    public bool _bDisableRunning => m_bDisableRunning;

    [SerializeField] private Rigidbody m_rigid;
    [SerializeField] private MeshRenderer m_MR;
    [SerializeField] private ParticleSystem m_disableParticle;
    private Transform m_targetTransform;

    [SerializeField] private float m_fspeed;
    [SerializeField] private float m_fCurSpeed;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private Vector3 m_pDefaultPos;
    [SerializeField] private bool m_bIsFlyingToTarget;
    private BossRockLauncher m_launcher;

    public void SetPosition(Vector3 pos)
    {
        m_pDefaultPos = pos;
        gameObject.transform.position = m_pDefaultPos;
        
    }
    private void OnEnable()
    {
        StartCoroutine(LauncherDelay());
    }
    public void ResetData()
    {
        m_rigid.velocity = Vector3.zero;
        m_bIsFlyingToTarget = false;
        m_fCurSpeed = 0;
        gameObject.transform.position = m_pDefaultPos;
    }
    IEnumerator LauncherDelay()
    {
       yield return new WaitForSeconds(2.5f);
        m_bIsFlyingToTarget = true;
        
    }
    IEnumerator DisableDelay()
    {
        m_bDisableRunning = true;
        m_MR.enabled = false;
        m_disableParticle.Play();
        m_bIsFlyingToTarget = false;
        yield return new WaitForSeconds(0.6f);
        m_MR.enabled = true;
        m_bDisableRunning = false;
        m_launcher.Remove(this);
    }
    public void SetInitData(Transform trans,float damage,BossRockLauncher launcher)
    {
        m_targetTransform = trans;
        m_fDamage = damage;
        m_launcher = launcher;
    }
    private void Update()
    {

        if (m_bIsFlyingToTarget && m_targetTransform != null)
        {
            if (m_fCurSpeed <= m_fspeed )
            {
                m_fCurSpeed += m_fspeed * Time.deltaTime; 
            }
            transform.position += transform.up * m_fCurSpeed * Time.deltaTime;
            Vector3 t_dir = (m_targetTransform.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);
        }
    }
    public override void RemoveStone()
    {
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(DisableDelay());
    }

}