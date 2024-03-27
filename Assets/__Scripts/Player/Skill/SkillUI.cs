using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image m_image;
    [SerializeField] private Button m_btn;
    [SerializeField] int m_slotNum;
    [SerializeField] int m_beforeSkillTreeNum;
    [SerializeField] SkillToolTip m_skillToolTip;
    [SerializeField] private bool m_Locked = true;
    SkillType m_skillType = SkillType.Locked;
    public int _beforeSkillTreeNum => m_beforeSkillTreeNum;
    public SkillType _skillType => m_skillType;
    public bool _Locked => m_Locked;


    public void OnPointerEnter(PointerEventData eventData)
    {
        
        m_skillToolTip.SetPosition(eventData.position + new Vector2(150,-170));
        m_skillToolTip.gameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_skillToolTip.gameObject.SetActive(false);
    }
    public void SetUI(bool locked)
    {
        m_Locked = locked;
        m_skillType = SkillType.UnLocked;
        LockUnlockUISet(m_Locked);
    }
    public void LockUnlockUISet(bool locked)
    { 
        if(locked)
        {
            Color c = m_btn.image.color;
            c.a = 0.1f;
            m_btn.image.color = c;
            return;
        }
        m_image.gameObject.SetActive(false);
        m_skillType = SkillType.UnLocked;
        Color b = m_btn.image.color;
        b.a = 0.1f;
        m_btn.image.color = b;
    }
}
