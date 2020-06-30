using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanelListGroup : LevelSelectLinearPanelList
{
    [SerializeField]
    private List<ALevelSelectPanelList> aPanels;

    protected override List<ALevelSelectPanel> getPanelList()
    {
        List<ALevelSelectPanel> panels = new List<ALevelSelectPanel>();
        foreach(ALevelSelectPanelList list in this.aPanels)
        {
            panels.Add(list);
        }
        return panels;
    }
}
