using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelPrereq : ScriptableObject
{
    //Is the level available?
    public abstract bool isAvailable();
}
