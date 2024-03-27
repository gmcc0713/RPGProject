using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillQuickSlotUI : MonoBehaviour
{
    [SerializeField] protected Sprite m_baseImage;
    [SerializeField] protected Image m_itemImage;
    [SerializeField] protected TextMeshProUGUI m_itemAmountText;
    [SerializeField] private Image m_FillImage;
    [SerializeField] private TextMeshProUGUI m_coolText;
    private bool m_bIsEmpty = true;
    public void SetQuickSlotDetailUI(float fillAmount, string curCoolDown)
    {
        m_FillImage.fillAmount = fillAmount;
        m_coolText.text = curCoolDown;
    }
    public void SetCoolDownText(string curCoolDown)
    {

        m_coolText.text = curCoolDown;
    }
    public void SetCoolDownFillImage(float fillAmount)
    {
        m_FillImage.fillAmount = fillAmount;
    }
    public void SetUI(int id)
    {
        //이미지 장착
        m_itemImage.sprite = ItemDataManager.Instance.FindItemImage(id);
        m_bIsEmpty = false;
    }
}
