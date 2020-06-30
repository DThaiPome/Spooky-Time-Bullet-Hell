using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField]
    private ALevelSelectPanelList leftPanelGroup;
    [SerializeField]
    private ALevelSelectPanelList rightPanelGroup;

    private ILevelSelectPanelList leftGroup;
    private ILevelSelectPanelList rightGroup;
    private ILevelSelectPanelList selectedGroup;

    void Awake()
    {
        this.leftGroup = this.leftPanelGroup;
        this.rightGroup = this.rightPanelGroup;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.focusRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.focusLeft();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            this.focusRight();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.nextPanel();
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            this.prevPanel();
        }
    }

    private void focusLeft()
    {
        this.leftGroup.focus();
        this.rightGroup.defocus();
        this.selectedGroup = this.leftGroup;
    }

    private void focusRight()
    {
        this.leftGroup.defocus();
        this.rightGroup.focus();
        this.selectedGroup = this.rightGroup;
    }

    private void nextPanel()
    {
        this.selectedGroup.nextPanel();
    }

    private void prevPanel()
    {
        this.selectedGroup.prevPanel();
    }
}
