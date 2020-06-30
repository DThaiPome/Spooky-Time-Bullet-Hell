using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelSelectLinearPanelList : ALevelSelectPanelList
{
    [SerializeField]
    private int selectedPanel;
    private List<ILevelSelectPanel> panels;

    private int previousSelectedPanel;

    void Awake()
    {
        this.awake();
    }

    protected virtual void awake()
    {
        this.panels = this.makePanelList(this.getPanelList());
    }

    protected virtual List<ILevelSelectPanel> makePanelList(List<ALevelSelectPanel> panelList)
    {
        List<ILevelSelectPanel> panels = new List<ILevelSelectPanel>();
        foreach (ALevelSelectPanel aPanel in panelList)
        {
            panels.Add(aPanel);
        }
        return panels;
    }

    protected abstract List<ALevelSelectPanel> getPanelList();

    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        if (this.indexInRange(this.selectedPanel))
        {
            this.selectedPanel = 0;
        }
        this.previousSelectedPanel = this.selectedPanel;
        this.selectDefaultPanel();
        this.defocus();
    }

    private bool indexInRange(int i)
    {
        return i >= 0 && i < this.panels.Count;
    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.focusIfSelectedPanelChanges();
    }

    private void focusIfSelectedPanelChanges()
    {
        if (this.previousSelectedPanel != this.selectedPanel)
        {
            this.previousSelectedPanel = this.selectedPanel;
            this._focus();
        }
    }

    public override void defocus()
    {
        this._defocus();
    }

    private void _defocus()
    {
        foreach (ILevelSelectPanel panel in this.panels)
        {
            panel.defocus();
        }
    }

    public override void focus()
    {
        this._focus();
    }

    private void _focus()
    {
        ILevelSelectPanel selectedPanel = null;
        if (this.indexInRange(this.selectedPanel))
        {
            selectedPanel = this.panels[this.selectedPanel];
        }
        foreach (ILevelSelectPanel panel in this.panels)
        {
            if (selectedPanel == panel)
            {
                panel.focus();
            }
            else
            {
                panel.defocus();
            }
        }
    }

    public override ILevelSelectPanel nextPanel()
    {
        if (this.selectedPanel < this.panels.Count - 1)
        {
            this.selectedPanel++;
        }
        return this.getPanelAt(this.selectedPanel);
    }

    public override ILevelSelectPanel prevPanel()
    {
        if (this.selectedPanel > 0)
        {
            this.selectedPanel--;
        }
        return this.getPanelAt(this.selectedPanel);
    }

    public override ILevelSelectPanel selectDefaultPanel()
    {
        this.selectedPanel = 0;
        return this.getPanelAt(this.selectedPanel);
    }

    private ILevelSelectPanel getPanelAt(int index)
    {
        if (this.indexInRange(index))
        {
            return this.panels[index];
        } else
        {
            return null;
        }
    }
}
