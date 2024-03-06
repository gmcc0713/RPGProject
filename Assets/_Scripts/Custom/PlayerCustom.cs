using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Hairs;
    [SerializeField] private GameObject[] m_Ears;
    [SerializeField] private GameObject[] m_Faces;

   [SerializeField] private int[] m_lastIdx;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        if(!SaveLoadManager.Instance._bIsFirst)
        {
            m_Hairs[0].SetActive(false);
            m_Ears[0].SetActive(false);
            m_Faces[0].SetActive(false);
            m_lastIdx = SaveLoadManager.Instance.LoadCustomizeIdx();
            m_Hairs[m_lastIdx[0]].SetActive(true);
            m_Ears[m_lastIdx[1]].SetActive(true);
            m_Faces[m_lastIdx[2]].SetActive(true);
        }
       
    }


    public void ChangePlayerCustomAll (int hair,int ear,int faces)
    {
        m_Hairs[m_lastIdx[0]].SetActive(false);
        m_Ears[m_lastIdx[1]].SetActive(false);
        m_Faces[m_lastIdx[2]].SetActive(false);

        m_Hairs[hair].SetActive(true);
        m_Ears[ear].SetActive(true);
        m_Faces[faces].SetActive(true);

        m_lastIdx[0] = hair;
        m_lastIdx[1] = ear;
        m_lastIdx[2] = faces;
    }
    public void ChangePlayerCustom(int type,int index)
    {
        switch(type)
        {
            case 0:
                m_Hairs[m_lastIdx[0]].SetActive(false);
                m_Hairs[index].SetActive(true);
                m_lastIdx[0] = index;
                break;
            case 1:
                m_Ears[m_lastIdx[1]].SetActive(false);
                m_Ears[index].SetActive(true);
                m_lastIdx[1] = index;
                break;
            case 2:
                m_Faces[m_lastIdx[2]].SetActive(false);
                m_Faces[index].SetActive(true);
                m_lastIdx[2] = index;
                break;
        }
    }
}
