
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
    private List<Quest_Data> m_lQuests;
    private int m_iNpcCount = 1;
    private void Awake()
    {
        m_lRewards = new List<Quest_Reward>();
    }

    void Save()
    {
        XmlDocument xmlDocument = new XmlDocument();
    }
    public List<Quest_Data> GetQuestDatas()
    {
        return m_lQuests;
    }
    public void LoadQuests()
    {
        var txtAsset = (TextAsset)Resources.Load("XML/QuestDatas");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(txtAsset.text);
        List<Quest_Data> quests = new List<Quest_Data>();
        string[] announcements = new string[3];
        for (int i =0;i< m_iNpcCount;i++)
        {
            XmlNodeList npcQuestNodes = xmlDocument.SelectNodes($"/Quests/NPC[@id='{i}']/Quest");
            int rewardID;
            foreach (XmlNode questNode in npcQuestNodes)        //퀘스트 기준으로 데이터 가져오기
            {
                Quest_Data quest = new Quest_Data();
                quest.m_iQuestID = int.Parse(questNode.SelectSingleNode("ID").InnerText);
                quest.m_sTitle = questNode.SelectSingleNode("Title").InnerText;
                quest.m_Description = questNode.SelectSingleNode("Description").InnerText;
                quest.m_iNPCID = i;
                rewardID = int.Parse(questNode.SelectSingleNode("Reward").InnerText);

                quest.m_sRequire.requireLV = int.Parse(questNode.SelectSingleNode("RequireLV").InnerText);
                quest.m_sRequire.count= int.Parse(questNode.SelectSingleNode("RequireCount").InnerText);
                quest.m_sRequire.id = int.Parse(questNode.SelectSingleNode("RequireID").InnerText);
                quest.m_eType = (Quest_Type)int.Parse(questNode.SelectSingleNode("QuestType").InnerText);
                announcements[(int)Announcement_Type.Accept] = questNode.SelectSingleNode("QuestAccept").InnerText;
                announcements[(int)Announcement_Type.Refuse] = questNode.SelectSingleNode("QuestCancle").InnerText;
                announcements[(int)Announcement_Type.Non] = questNode.SelectSingleNode("QuestNon").InnerText;
                quest.m_arrAnnouncement = announcements;
                string xpath = $"/Quests/NPC[@id='{i}']/Quest[ID='{quest.m_iQuestID}']/Dialog/Step";
                XmlNodeList dialogNodes = questNode.SelectNodes(xpath);

                // 수정: dialogNode를 기준으로 데이터 가져오도록 수정
                foreach (XmlNode dialogNode in dialogNodes)
                {
                    string text = dialogNode.SelectSingleNode("text").InnerText;

                    // 수정: 대화 스텝을 객체로 저장
                    if (quest.m_lDialog == null)
                    {
                        quest.m_lDialog = new List<string>();
                    }
                    quest.m_lDialog.Add(new string(text));

                }
                quest.m_sReward = m_lRewards[rewardID];

                quests.Add(quest);

            }
        }
        m_lQuests = quests;
       
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
        }


    }

}



