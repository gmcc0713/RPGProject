using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private string[] m_ment;
    private int m_iIndex;
    
    public void NextTextClick()
    {
        m_iIndex++;
        if(m_iIndex >=m_ment.Length)
        {
            m_iIndex = 0;
            this.gameObject.SetActive(false);
        }
        m_text.text = m_ment[m_iIndex];
    }
}
