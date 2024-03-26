using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBulletFallRock : MonoBehaviour,IPoolingObject
{
    private BossRockRainSpawner m_bossRock;

    private float m_fallSpeed = 5;
    [SerializeField] private GameObject m_warningCircle;
    private SpriteRenderer m_spriteRenderer;
    [SerializeField] private GameObject m_smallRock;
    [SerializeField] private Vector3 m_target;
    [SerializeField] private float m_mag;
    public float lerpSpeed = 2f;
    void Start()
    {
        m_spriteRenderer = m_warningCircle.GetComponent<SpriteRenderer>();
    }
    public void SetInit(BossRockRainSpawner bossrockrain)
    {
        m_bossRock = bossrockrain;
        Vector3 pos = transform.position;
        pos.y = 0;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if(hit.collider.CompareTag("Ground"))
            {
                m_warningCircle.SetActive(true);
                m_warningCircle.transform.position = hit.point + Vector3.up * 0.15f;
                m_target = hit.point;
                m_mag = (m_target - m_smallRock.transform.position).sqrMagnitude;
            }
        }
        Debug.Log("Init");
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;


    }

    public void Update()
    {
        //lerp자체를 이용해서 돌을 떨어뜨린다.
        // 이때 lerp의 값 자체로 alpha값을 변경 시킨다.
        Debug.Log(1 - (m_target - m_smallRock.transform.position).sqrMagnitude / m_mag);
        m_smallRock.transform.Translate(Vector3.down * Time.deltaTime * m_fallSpeed);
        m_spriteRenderer.color =new Color(1,0,0,1- (m_target - m_smallRock.transform.position).sqrMagnitude / m_mag);
        Debug.DrawRay(transform.position, Vector3.down * 15, Color.red);

    }


    public void ResetData()
    {
        m_warningCircle.SetActive(false);
        m_smallRock.transform.position = Vector3.zero;
        m_spriteRenderer.color = new Color(1,0,0,0);
    }
    public void RemoveRock()
    {
        ResetData();
        m_bossRock.RemoveStone(this);

    }
}
