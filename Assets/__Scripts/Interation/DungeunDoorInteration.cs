using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeunDoorInteration : InteractionObject
{
    
    public override void PressInteractionKey()
    {
        SaveLoadManager.Instance.Save();

        SceneManager.LoadScene("Dungeon");
        Debug.Log("¿­¼è »ç¿ë");
    }
}
