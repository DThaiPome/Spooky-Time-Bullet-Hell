using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectWindowGroup : ALevelSelectPanel
{
    [SerializeField]
    private List<ALevelSelectPanel> aPanels;
    private List<ILevelSelectPanel> panels;

    void Awake()
    {
        this.panels = new List<ILevelSelectPanel>();
        foreach(ALevelSelectPanel aPanel in this.aPanels)
        {
            this.panels.Add(aPanel);
        }
    }

    void Start()
    {
        foreach(ILevelSelectPanel panel in this.panels)
        {
            this.defocus();
        }
    }

    void Update()
    {
        
    }

    public override void defocus()
    {
        foreach(ILevelSelectPanel panel in this.panels) {
            panel.defocus();
        }
    }

    public override void focus()
    {
        foreach (ILevelSelectPanel panel in this.panels)
        {
            panel.focus();
        }
    }
}
