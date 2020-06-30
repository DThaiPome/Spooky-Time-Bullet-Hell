using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMutableWindowGroup : LevelSelectWindowGroup, ILevelSelectPanelMutableList
{

    private List<ILevelSelectPanel> panelsDelegate;

    protected override List<ILevelSelectPanel> makePanelList(List<ALevelSelectPanel> panelList)
    {
        this.panelsDelegate = base.makePanelList(panelList);
        return this.panelsDelegate;
    }

    public void add(ILevelSelectPanel panel)
    {
        this.panelsDelegate.Add(panel);
    }

    public void clear()
    {
        this.selectDefaultPanel();
        this.destroyAllPanels();
        this.panelsDelegate.Clear();
    }

    private void destroyAllPanels()
    {
        foreach (ILevelSelectPanel panel in this.panelsDelegate)
        {
            Object.Destroy(((ALevelSelectPanel)panel).gameObject);
        }
    }
}
