using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectWindowContents : MonoBehaviour, ILevelSelectWindowContents
{
    public abstract void hideContents();
    public abstract void showContents();
}
