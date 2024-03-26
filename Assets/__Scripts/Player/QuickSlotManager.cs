using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    public static QuickSlotManager Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    //==================================================================================

    private QuickSlot[] m_quickSlotItems;
    [SerializeField] private string[] m_quickSlotKeyCode;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        m_quickSlotItems = new QuickSlot[4];
        for (int i = 0; i < m_quickSlotItems.Length; i++)
        {
            m_quickSlotItems[i] = new QuickSlot();
            m_quickSlotItems[i].Initialize(i, m_quickSlotKeyCode[i]);
        }
    }
    public void ItemAddQuickSlotUpdate(int itemId) //�Է¹޴� id ���� �������� �߰��Ǿ �ش� �������� ���� �Ǿ����� Ȯ�� �� ������Ʈ
    {

        foreach(QuickSlot slot in m_quickSlotItems)
        {
            if(slot.IsItemIDSame(itemId))
            {
                slot.UpdateItemInLinkedIvslot();
                break;
            }
        }
    }
    public void SetItemInQuickSlot(int quickSlotNum,int ivSlotNum) //�������� ��ȣ�� �޾� �ش� ��ȣ�� �����Կ� ������ ����
    {
        m_quickSlotItems[quickSlotNum].UpdateItemSlotAddItem(ivSlotNum);
    }
    public void UpdateQuickSlotByQuickSlotNum(int quickSlotNum) //�������� ��ȣ�� �Է¹����� �ش� ��ȣ�� �����Ը� ������Ʈ �ȴ�.
    {
        m_quickSlotItems[quickSlotNum].UpdateItemInLinkedIvslot();
    }
    private void Update()
    {

        foreach (QuickSlot slot in m_quickSlotItems)
        {           
            if(!slot._bIsEmpty)
            {
                slot.IsPressedKey();
            }

        }

    }

    public void CoolTimeStart(QuickSlot slot) //��Ÿ�� ����
    {
        slot.StartTimer();
        StartCoroutine(Cooltime(slot));
        StartCoroutine(CoolDown(slot));
    }
    public IEnumerator Cooltime(QuickSlot slot)
    {

        while (slot.fillAmountValue > 0)
        {
            slot.fillAmountValue -= 1 * Time.smoothDeltaTime / slot._iMaxItemCoolTime;
            slot.CoolTimeRunFillUIUpdate();
            yield return null;
        }
        yield break;
    }

    public IEnumerator CoolDown(QuickSlot slot)
    {
        while (slot._iCurItemCoolTime > 0)
        {
            slot.TimerOneSec();
            slot.CoolTimeRunTextUIUpdate();
            yield return new WaitForSeconds(1.0f);
        }
        slot.CoolTimeRunUIUpdateEnd();
        yield break;
    }


}
