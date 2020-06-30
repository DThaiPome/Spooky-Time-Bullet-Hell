using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectWindowGroup : LevelSelectLinearPanelList
{
    [SerializeField]
    private List<ALevelSelectPanel> aPanels;

    protected override List<ALevelSelectPanel> getPanelList()
    {
        return this.aPanels;
    }
}
