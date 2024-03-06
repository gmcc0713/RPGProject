using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteration : InteractionObject
{
    public override void PressInteractionKey()
    {
        gameObject.GetComponent<NPCController>().UseInterationKey();
    }
}
