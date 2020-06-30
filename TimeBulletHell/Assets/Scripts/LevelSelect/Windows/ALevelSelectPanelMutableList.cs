using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectPanelMutableList : ALevelSelectPanelList, ILevelSelectPanelMutableList
{
    public abstract void add(ILevelSelectPanel panel);
    public abstract void clear();
}
