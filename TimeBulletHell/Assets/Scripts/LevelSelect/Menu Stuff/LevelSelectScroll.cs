using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScroll : MonoBehaviour
{
    [SerializeField]
    private float distanceBetweenIcons;

    private List<LevelSelectOption> icons;
    private int selectedIndex;

    void Start()
    {
        EventManager.instance.onLevelSelectArrowClickedEvent += this.arrowClicked;

        this.icons = new List<LevelSelectOption>();
        foreach(Transform t in transform)
        {
            LevelSelectOption option = t.gameObject.GetComponent<LevelSelectOption>();
            option.disableAsSelectable();
            this.icons.Add(option);
        }
        this.selectedIndex = 0;
        this.icons[this.selectedIndex].enableAsSelectable();
        this.initIcons();
    }

    private void initIcons()
    {
        for(int i = 0; i < this.icons.Count; i++)
        {
            LevelSelectOption icon = this.icons[i];
            icon.transform.localPosition = new Vector3(0, 0, icon.transform.localPosition.z);
            icon.transform.localPosition += (Vector3)new Vector2(this.distanceBetweenIcons * i, 0);
        }
    }

    private void arrowClicked(LevelSelectArrowDirection direction)
    {
        this.icons[this.selectedIndex].disableAsSelectable();
        switch(direction)
        {
            case LevelSelectArrowDirection.Right:
                this.moveRight();
                break;
            case LevelSelectArrowDirection.Left:
                this.moveLeft();
                break;
        }
        this.icons[this.selectedIndex].enableAsSelectable();
    }

    private void moveRight()
    {
        if (this.selectedIndex < this.icons.Count - 1)
        {
            this.transform.position -= (Vector3)new Vector2(this.distanceBetweenIcons, 0);
            this.selectedIndex++;
        }
    }

    private void moveLeft()
    {
        if (this.selectedIndex > 0)
        {
            this.transform.position += (Vector3)new Vector2(this.distanceBetweenIcons, 0);
            this.selectedIndex--;
        }
    }
}
