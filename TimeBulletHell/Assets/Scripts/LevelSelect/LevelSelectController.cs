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
            this.selectedGroup = (ILevelSelectPanelList)this.prevPanel();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            this.selectedGroup = (ILevelSelectPanelList)this.nextPanel();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.selectedGroup.nextPanel();
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            this.selectedGroup.prevPanel();
        }
    }
}
