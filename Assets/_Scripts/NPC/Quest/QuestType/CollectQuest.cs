using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : Quest
{
    public override string GetRequireText()
    {
        return ItemDataManager.Instance.FindItem(m_sData.m_sRequire.id).itemName;
    }
    public override void ClearQuest()
    {
        base.ClearQuest();
        Inventory.Instance.FindAndRemoveItem(m_sData.m_sRequire.id, m_sData.m_sRequire.count);
    }
}
