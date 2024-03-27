using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum QuickSlotType
{
    Skill,
    Item
}

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

    private ItemQuickSlot[] m_quickSlotItems;
    [SerializeField] private string[] m_quickSlotKeyCode;

    private SkillQuickSlot[] m_quickSlotSkills;
    [SerializeField] private string[] m_quickSlotSkillKeyCode;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        m_quickSlotItems = new ItemQuickSlot[4];
        for (int i = 0; i < m_quickSlotItems.Length; i++)
        {
            m_quickSlotItems[i] = new ItemQuickSlot();
            m_quickSlotItems[i].Initialize(i, m_quickSlotKeyCode[i]);
        }
        m_quickSlotSkills = new SkillQuickSlot[4];
        for (int i = 0; i < m_quickSlotItems.Length; i++)
        {
            m_quickSlotSkills[i] = new SkillQuickSlot();
            m_quickSlotSkills[i].Initialize(i, m_quickSlotKeyCode[i]);
        }
    }
    public void ItemAddQuickSlotUpdate(int itemId) //입력받는 id 값의 아이템이 추가되어서 해당 아이템이 장착 되었는지 확인 및 업데이트
    {

        foreach(ItemQuickSlot slot in m_quickSlotItems)
        {
            if(slot.IsItemIDSame(itemId))
            {
                slot.UpdateItemInLinkedIvslot();
                break;
            }
        }
    }

    public void SetItemInQuickSlot(int quickSlotNum,int ivSlotNum) //퀵슬롯의 번호를 받아 해당 번호의 퀵슬롯에 아이템 장착
    {
        m_quickSlotItems[quickSlotNum].UpdateItemSlotAddItem(ivSlotNum);
    }
    public void UpdateQuickSlotByQuickSlotNum(int quickSlotNum) //퀵슬롯의 번호를 입력받으면 해당 번호의 퀵슬롯만 업데이트 된다.
    {
        m_quickSlotItems[quickSlotNum].UpdateItemInLinkedIvslot();
    }

    public void SetSkillDataInQuickSlot(int quickSlotNum, int skillID)
    {
        m_quickSlotSkills[quickSlotNum].SetSkillDataInQuickSlot(skillID);
    }

    private void Update()
    {

        foreach (ItemQuickSlot slot in m_quickSlotItems)
        {           
            if(!slot._bIsEmpty)
            {
                slot.IsPressedKey();
            }

        }
        foreach (SkillQuickSlot slot in m_quickSlotSkills)
        {
            if (!slot._bIsEmpty)
            {

                slot.IsPressedKey();
            }

        }
    }

    public void CoolTimeStart(QuickSlot slot) //쿨타임 시작
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
