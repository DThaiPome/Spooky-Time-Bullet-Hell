using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectPanelList : ALevelSelectPanel, ILevelSelectPanelList
{
    public abstract ILevelSelectPanel nextPanel();
    public abstract ILevelSelectPanel prevPanel();
    public abstract ILevelSelectPanel selectDefaultPanel();
}
