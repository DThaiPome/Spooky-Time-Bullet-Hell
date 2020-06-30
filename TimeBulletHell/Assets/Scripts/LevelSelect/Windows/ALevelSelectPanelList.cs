using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectPanelList : ALevelSelectPanel, ILevelSelectPanelList
{
    public abstract void nextPanel();
    public abstract void prevPanel();
    public abstract void selectDefaultPanel();
}
