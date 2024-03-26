using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : BossRockBullet
{
    public override void RemoveStone() 
    {
        GetComponentInParent<BossBulletFallRock>().RemoveRock();
    }
}
