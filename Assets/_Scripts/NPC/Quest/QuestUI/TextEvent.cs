using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public enum TextEventType
{
    PadeOut,
}
public class TextEvent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TextEventType m_type;
    private void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        switch(m_type)
        {
            case TextEventType.PadeOut:
                StartCoroutine(PadeOut());
                break;
        }

    }
    private IEnumerator PadeOut()
    {
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 1);
        while (m_text.color.a > 0.0f)
        {
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, m_text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 1);
        gameObject.SetActive(false);
        yield break;
    }
}