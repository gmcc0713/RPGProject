using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    protected int m_iID; //��ų ���̵�
    protected int m_iSlotNum = -1; //�������� ��ȣ
    [SerializeField] protected string code;
    protected float m_iCurItemCoolTime;
    protected float m_iMaxItemCoolTime;       //��Ÿ�� �ִ�ġ
    protected bool m_bIsEmpty;
    [SerializeField] protected bool m_bCanUse = true;
    public float fillAmountValue;


    public bool _bIsEmpty => m_bIsEmpty;
    public int _iSlotNum => m_iSlotNum;
    public float _iCurItemCoolTime => m_iCurItemCoolTime;
    public float _iMaxItemCoolTime => m_iMaxItemCoolTime;
    public virtual void Initialize(int i, string a) { }
    public virtual void IsPressedKey() { }
    public virtual void CoolTimeRunTextUIUpdate() { }
    public virtual void CoolTimeRunFillUIUpdate() { }
    public virtual void CoolTimeRunUIUpdateEnd(){}
    public void TimerOneSec()
    {
        m_iCurItemCoolTime--;
    }
    public void StartTimer()
    {
        fillAmountValue = 1;
        m_iCurItemCoolTime = m_iMaxItemCoolTime;
    }
}
