using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoOnHover))]
[RequireComponent(typeof(Collider))]
public class LevelSelectOption : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;

    void Awake()
    {
        levelInfo.icon = this.GetComponent<SpriteRenderer>().sprite;
    }

    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    void OnDestroy()
    {
        EventManager.instance.onClickEvent -= this.onClick;
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

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            //EventManager.instance.switchToLevel(this.levelInfo.levelTarget);
            EventManager.instance.onLevelSelected(this.levelInfo);
        }
    }
}
