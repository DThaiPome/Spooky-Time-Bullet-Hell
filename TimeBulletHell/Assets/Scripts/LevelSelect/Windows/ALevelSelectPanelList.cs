using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelSelectPanelList : ALevelSelectPanel, ILevelSelectPanelList
{
    public abstract void nextPanel();
    public abstract void prevPanel();
    public abstract void selectDefaultPanel();

    protected virtual List<ILevelSelectPanel> makePanelList(List<ALevelSelectPanel> panelList)
    {
        List<ILevelSelectPanel> panels = new List<ILevelSelectPanel>();
        foreach (ALevelSelectPanel aPanel in panelList)
        {
            panels.Add(aPanel);
        }
        return panels;
    }
}
