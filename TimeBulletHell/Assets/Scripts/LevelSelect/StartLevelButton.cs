using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StartLevelButton : ALevelSelectPanelList
{
    [SerializeField]
    KeyCode selectButton;

    private string target;

    private bool selected;

    void Start()
    {
        EventManager.instance.onLevelSelectedEvent += this.onLevelSelected;
        this.selected = false;
    }

    void OnDestroy()
    {
        EventManager.instance.onLevelSelectedEvent -= this.onLevelSelected;
    }

    void Update()
    {
        if (this.selected && Input.GetKeyDown(this.selectButton))
        {
            this.loadLevel();
        }
    }
    
    private void onLevelSelected(LevelInfo info)
    {
        this.target = info.levelTarget;
    }

    private void loadLevel()
    {
        if (this.target != null && LevelDataMap.instance.getData(this.target).preReqMet())
        {
            EventManager.instance.switchToLevel(this.target);
        }
    }

    public override void defocus()
    {
        EventManager.instance.onHoverExit(this.transform);
        this.selected = false;
    }

    public override void focus()
    {
        EventManager.instance.onHoverEnter(this.transform);
        this.selected = true;
    }

    public override ILevelSelectPanel nextPanel()
    {
        return this;
    }

    public override ILevelSelectPanel prevPanel()
    {
        return this;
    }

    public override ILevelSelectPanel selectDefaultPanel()
    {
        return this;
    }
}
