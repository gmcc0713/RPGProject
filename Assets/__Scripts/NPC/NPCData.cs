using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]

public class NPCData : ScriptableObject
{
    [SerializeField] private string NPCName;
    [SerializeField] private string NPCTalk;
    [SerializeField] private Sprite NPCImage;
    [SerializeField] private NPCType type;

    public string _npcName => NPCName;
    public string _npcTalk => NPCTalk;
    public Sprite _npcImage => NPCImage;
    public NPCType _type => type;
}
/*
 * npc 종류
 * 대장 장이 (장비상점, 무기 제작)
 * 잡화 상인 (아이템 상점, 물약 제작(보류))
 * 일반 - 퀘스트만 줌
 * 
 * 
 */