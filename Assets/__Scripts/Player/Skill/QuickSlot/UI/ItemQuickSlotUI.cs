using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemQuickSlotUI : ItemSlotUI
{
    [SerializeField] private Image m_FillImage;
    [SerializeField] private TextMeshProUGUI m_coolText;
    public void SetQuickSlotDetailUI(float fillAmount,string curCoolDown)
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
}
