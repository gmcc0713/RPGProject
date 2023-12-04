
using UnityEngine;
using TMPro;
using System.Xml;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class QuestLoader : MonoBehaviour
{
   
    public TextAsset xmlFile;
    private List<Quest_Reward> m_lRewards;
    private int m_iNpcCount = 1;
    private void Awake()
    {
        m_lRewards = new List<Quest_Reward>();
    }

    void Save()
    {
        XmlDocument xmlDocument = new XmlDocument();
    }
    public List<Quest_Data> LoadQuests()
    {
        var txtAsset = (TextAsset)Resources.Load("XML/QuestDatas");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(txtAsset.text);
        List<Quest_Data> quests = new List<Quest_Data>();
        for (int i =0;i< m_iNpcCount;i++)
        {
            XmlNodeList npcQuestNodes = xmlDocument.SelectNodes($"/Quests/NPC[@id='{i}']/Quest");
            int rewardID;
            foreach (XmlNode questNode in npcQuestNodes)
            {
                Quest_Data quest = new Quest_Data();
                quest.questID = int.Parse(questNode.SelectSingleNode("ID").InnerText);
                quest.title = questNode.SelectSingleNode("Title").InnerText;
                quest.description = questNode.SelectSingleNode("Description").InnerText;
                rewardID = int.Parse(questNode.SelectSingleNode("Reward").InnerText);
                string xpath = $"/Quests/NPC[@id='{i}']/Quest[ID='{quest.questID}']/Dialog/Step";
                XmlNodeList dialogNodes = questNode.SelectNodes(xpath);

                // 수정: dialogNode를 기준으로 데이터 가져오도록 수정
                foreach (XmlNode dialogNode in dialogNodes)
                {
                    string speaker = dialogNode.SelectSingleNode("Speaker").InnerText;
                    string text = dialogNode.SelectSingleNode("text").InnerText;

                    // 수정: 대화 스텝을 객체로 저장
                    if (quest.dialog == null)
                    {
                        quest.dialog = new List<string[]>();
                    }
                    quest.dialog.Add(new string[] { speaker, text });

                }
                quest.rewards = m_lRewards[rewardID];

                quests.Add(quest);

            }
        }
        return quests;
        // 특정 NPC의 Quest 노드들을 가져온다
       
    }
    
    public void RewardLoad()
    {
        var txtAsset = (TextAsset)Resources.Load("XML/RewardDatas");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(txtAsset.text);

        //List<Quest_Data>  = new List<Quest_Data>();

        XmlNodeList rewards = xmlDocument.SelectNodes($"/Rewards/Reward");
        Quest_Reward rewardTmp = new Quest_Reward();
        foreach (XmlNode reward in rewards)
        {
            rewardTmp.rewardGold = int.Parse(reward.SelectSingleNode("Gold").InnerText);
            rewardTmp.rewardXp = int.Parse(reward.SelectSingleNode("Xp").InnerText);
            string xpath = $"/Rewards/Item";
            XmlNodeList items = reward.SelectNodes(xpath);
            foreach (XmlNode item in items)
            {
                rewardTmp.rewardItem.Add(int.Parse(item.SelectSingleNode("ItemId").InnerText), int.Parse(item.SelectSingleNode("ItemAmount").InnerText));
            }
            m_lRewards.Add(rewardTmp);
            Debug.Log(rewardTmp.rewardGold);
        }


    }

}



