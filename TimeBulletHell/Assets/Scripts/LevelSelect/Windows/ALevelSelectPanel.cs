using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectPanel : MonoBehaviour, ILevelSelectPanel
{
    public abstract void defocus();
    public abstract void focus();
}
