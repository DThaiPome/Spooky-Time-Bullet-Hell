using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : LevelSelectPanelListGroup
{
    private ILevelSelectPanelList selectedGroup;

    private InputAxis vertical;
    private InputAxis horizontal;

    protected override void awake()
    {
        base.awake();
        this.vertical = GameInput.input.getAxis("Vertical");
        this.horizontal = GameInput.input.getAxis("Horizontal");
    }

    protected override void update()
    {
        base.update();
        if (this.horizontal.negativeDown())
        {
            this.prevPanel();
        } else if (this.horizontal.positiveDown())
        {
            this.nextPanel();
        }

        if (this.vertical.negativeDown())
        {
            this.selectedGroup.nextPanel();
        } else if (this.vertical.positiveUp())
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
