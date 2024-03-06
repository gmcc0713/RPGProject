using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickSlot : InventorySlot
{
    [SerializeField] private int m_iInventorySlotNum;
    [SerializeField] private string code;
    private float m_iCurItemCoolTime = 5;
    private float m_iMaxItemCoolTime = 5;
    [SerializeField] private Image m_FillImage;
    [SerializeField] private TextMeshProUGUI m_coolText;
    [SerializeField] private bool m_bCanUse = true;
    public void SetIVSlotNum(int slotNum)
    {
        m_iInventorySlotNum = slotNum;
    }
    public void IsPressedKey()
    {
        if(isEmpty || !m_bCanUse)
        {
            return;
        }
        if(Input.GetKeyUp(code))
        {
            m_bCanUse = false;
            Debug.Log("Press Quick Slot Key" + m_iInventorySlotNum);
            CoolTimeStart();
            Inventory.Instance.ItemUse(m_iInventorySlotNum,slotNum);
        }
      
    }
    public void CoolTimeStart()
    {

        m_FillImage.fillAmount = 1;
        StartCoroutine(Cooltime());
        m_iCurItemCoolTime = m_iMaxItemCoolTime;
        m_coolText.text = "" + m_iCurItemCoolTime;
        StartCoroutine(CoolDown());
    }

    public IEnumerator Cooltime()
    {
        while (m_FillImage.fillAmount > 0)
        {
            m_FillImage.fillAmount -= 1 * Time.smoothDeltaTime / m_iMaxItemCoolTime;
            yield return null;
        }

        yield break;
    }
    public IEnumerator CoolDown()
    {
        while (m_iCurItemCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            m_iCurItemCoolTime -= 1.0f;
            m_coolText.text = "" + m_iCurItemCoolTime;
        }
        Debug.Log("cool time end");
        m_bCanUse = true;
        m_coolText.text = "";
        yield break;
    }
}
