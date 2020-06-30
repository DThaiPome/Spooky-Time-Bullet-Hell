﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectWindowGroup : ALevelSelectPanelList
{
    [SerializeField]
    private List<ALevelSelectPanel> aPanels;
    private List<ILevelSelectPanel> panels;

    [SerializeField]
    private int selectedPanel;

    private int previousSelectedPanel;

    void Awake()
    {
        this.panels = this.makePanelList(this.aPanels);
    }

    void Start()
    {
        if (this.indexInRange(this.selectedPanel))
        {
            this.selectedPanel = 0;
        }
        this.previousSelectedPanel = this.selectedPanel;
        this._defocus();
    }

    private bool indexInRange(int i)
    {
        return i >= 0 && i < this.panels.Count;
    }

    void Update()
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

    public override void nextPanel()
    {
        if (this.selectedPanel < this.panels.Count - 1)
        {
            this.selectedPanel++;
        }
    }

    public override void prevPanel()
    {
        if (this.selectedPanel > 0)
        {
            this.selectedPanel--;
        }
    }

    public override void selectDefaultPanel()
    {
        this.selectedPanel = 0;
    }
}