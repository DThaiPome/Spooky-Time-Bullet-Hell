using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimelessBullet : PlayerBullet
{
    protected override float fixedDeltaTime()
    {
        return Time.deltaTime;
    }
}
