using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoOnHover))]
[RequireComponent(typeof(Collider))]
public class LevelSelectOption : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;

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
}
