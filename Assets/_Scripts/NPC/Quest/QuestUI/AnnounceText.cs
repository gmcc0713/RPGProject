using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnounceText : MonoBehaviour, IPoolingObject
{
    [SerializeField] private TextMeshProUGUI m_text;
    private string m_sAnnounceText;
    private QuestAnnounceSetting m_questAnnounceSetting;
    void Start()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(PadeOut());
    }
    public void Initialize(QuestAnnounceSetting questAnnounceSetting)
    {
        m_questAnnounceSetting = questAnnounceSetting;
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetText(string text)
    {
        m_text.text = text;
    }
    IEnumerator PadeOut()
    {
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 1);
        while (m_text.color.a > 0.0f)
        {
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, m_text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 1);
        m_questAnnounceSetting.PutInPoolAnnounceText(this);
        yield break;
    }
}
