using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCType
{
    Hunter = 0,
    Smith,
}
public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject dialogPanel;
    [SerializeField] List<NPCController> m_NPCs;
    

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
