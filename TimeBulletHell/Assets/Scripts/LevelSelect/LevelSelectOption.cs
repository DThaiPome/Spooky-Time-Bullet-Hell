using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DoOnHover))]
[RequireComponent(typeof(Collider))]
public class LevelSelectOption : ALevelSelectPanel
{
    [SerializeField]
    private LevelInfo levelInfo;
    [SerializeField]
    private GameObject checkObject;
    [SerializeField]
    private Text displayText;
    [SerializeField]
    private float growMagnitude;

    private Vector2 initScale;

    void Awake()
    {
        this.initScale = this.transform.localScale;
    }

    void Start()
    {
        this.checkObject.SetActive(LevelDataMap.instance.isComplete(this.levelInfo.levelTarget));
    }

    public void setLevelInfo(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
        this.displayText.text = levelInfo.displayName;
    }

    public LevelInfo getLevelInfo()
    {
        return this.levelInfo;
    }

    public void enableAsSelectable()
    {
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<DoOnHover>().enabled = true;
    }

    public void disableAsSelectable()
    {
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<DoOnHover>().enabled = false;
    }

    public override void defocus()
    {
        this.transform.localScale = this.initScale;
    }

    public override void focus()
    {
        this.transform.localScale = this.initScale * this.growMagnitude;
        EventManager.instance.onLevelSelected(this.levelInfo);
    }
}
