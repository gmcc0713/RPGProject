using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DivideItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_inputField;

    private int m_iAmount;
    private int m_iMaxAmount;
    private int m_islotNum;
    public void DIvideAmountSelect(int slotNum,int amount)
    {
        m_islotNum = slotNum;
        m_iMaxAmount = amount;
        m_iAmount = 1;

        m_inputField.text = m_iAmount.ToString();

        gameObject.SetActive(true);

    }
    public void AmountPlusMinus(int i)
    {
        m_iAmount+=i;
        if(m_iAmount < 1)
        {
            m_iAmount = 1;
        }    
        else if( m_iAmount > m_iMaxAmount-1)
        {
            m_iAmount = m_iMaxAmount - 1;
        }
        m_inputField.text = m_iAmount.ToString();
        
    }
    public void Divide()
    {
        if (int.Parse(m_inputField.text) > 1 && int.Parse(m_inputField.text) < m_iMaxAmount)
            ThirdPersonMovement.Instance._Inventory.Divide(m_islotNum, m_iAmount);
        else
            Debug.Log("Cant Divide, input number");
    }


}
