using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoPrereq", menuName = "LevelPrereqObjects/NoPrereq", order = 1)]
public class NoPrereq : ALevelPrereq
{
    public override bool isAvailable()
    {
        return true;
    }
}
