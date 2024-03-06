using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InteractionObject : MonoBehaviour
{
    [SerializeField] private string text;

    [SerializeField] private TextMeshProUGUI m_interactionText;
    [SerializeField] private GameObject m_InteractKey;
    [SerializeField] private Image m_spaceBar;
    [SerializeField] private Sprite[] m_spaceBarSprite;

    private bool m_bIsInInterationArea;
    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        m_interactionText.text = text;
        m_interactionText.gameObject.SetActive(true);
    }
    public void ShowOrHideInteractionObject(bool b)
    {
        m_interactionText.text = text;
        if(b)
        {
            m_InteractKey.SetActive(true);
            StartCoroutine(RunInterectAnimation());
            return;
        }
        m_InteractKey.SetActive(false);
        StopCoroutine(RunInterectAnimation());
    }
    public IEnumerator RunInterectAnimation()
    {
        while (true)
        {
            m_spaceBar.sprite = m_spaceBarSprite[0];
            yield return new WaitForSeconds(0.5f);
            m_spaceBar.sprite = m_spaceBarSprite[1];
            yield return new WaitForSeconds(0.5f);
        }
    }
    public virtual void PressInteractionKey()
    {
        
    }
}
