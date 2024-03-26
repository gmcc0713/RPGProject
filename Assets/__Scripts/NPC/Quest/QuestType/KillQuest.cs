using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    public override string GetRequireText()
    {
        return EnemyManager.Instance._enemyData[m_sData.m_sRequire.id]._enemyName;
    }

}
