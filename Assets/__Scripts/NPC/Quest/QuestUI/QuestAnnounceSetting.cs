using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAnnounceSetting : MonoBehaviour
{
    [SerializeField] private ObjectPool<AnnounceText> pool;

    private void Start()
    {
        initialize();
    }
    
    private void initialize()
    {
        pool.Initialize();
    }
    public void AddQuestAnnounceText(string text)
    {
        AnnounceText announceText;
        pool.GetObject(out announceText);
        announceText.SetText(text);
        announceText.Initialize(this);
        announceText.transform.SetParent(transform);
    }
    public void PutInPoolAnnounceText(AnnounceText text)
    {
        pool.PutInPool(text);
    }
}
//풀링시 content자식에서 작동되게 적용
//게임오브젝트 자체를 풀링하도록 적용