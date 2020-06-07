using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoOnHover))]
[RequireComponent(typeof(Collider))]
public class LevelSelectOption : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;
    [SerializeField]
    private GameObject checkObject;

    void Awake()
    {
        levelInfo.icon = null;
    }

    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
        this.checkObject.SetActive(LevelDataMap.instance.isComplete(this.levelInfo.levelTarget));
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
