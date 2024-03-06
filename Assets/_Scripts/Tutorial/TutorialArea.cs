using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialArea : MonoBehaviour
{
    [SerializeField] string[] m_gTutorialText;
    [SerializeField] GameObject m_gameObject;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            m_gameObject.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
        }
    }
}
