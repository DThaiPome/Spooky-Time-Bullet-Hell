using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : LevelSelectPanelListGroup
{
    private ILevelSelectPanelList selectedGroup;

    protected override void update()
    {
        base.update();
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.prevPanel();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            this.nextPanel();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.selectedGroup.nextPanel();
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            this.selectedGroup.prevPanel();
        }
    }

    public override ILevelSelectPanel nextPanel()
    {
        return this.selectPanel(base.nextPanel());
    }

    public override ILevelSelectPanel prevPanel()
    {
        return this.selectPanel(base.prevPanel());
    }

    public override ILevelSelectPanel selectDefaultPanel()
    {
        return this.selectPanel(base.selectDefaultPanel());
    }

    private ILevelSelectPanel selectPanel(ILevelSelectPanel panel)
    {
        ILevelSelectPanelList panelList = (ILevelSelectPanelList)panel;
        this.selectedGroup = panelList;
        return panel;
    }
}
