using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectList : ALevelSelectPanel
{
    [SerializeField]
    private List<LevelInfo> levels;
    [SerializeField]
    private GameObject levelOptionPrefab;
    [SerializeField]
    private float growMagnitude;

    private Vector2 initScale;

    void Start()
    {
        foreach(LevelInfo li in this.levels)
        {
            GameObject g = Object.Instantiate(levelOptionPrefab);
            g.transform.SetParent(this.transform);
            LevelSelectOption lso = g.GetComponent<LevelSelectOption>();
            lso.setLevelInfo(li);
        }

        this.initScale = this.transform.localScale;
    }
    public override void defocus()
    {
        this.transform.localScale = this.initScale;
    }

    public override void focus()
    {
        this.transform.localScale = this.initScale * this.growMagnitude;
    }
}
