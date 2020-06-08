using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectList : MonoBehaviour
{
    [SerializeField]
    private List<LevelInfo> levels;
    [SerializeField]
    private GameObject levelOptionPrefab;

    void Start()
    {
        foreach(LevelInfo li in this.levels)
        {
            GameObject g = Object.Instantiate(levelOptionPrefab);
            g.transform.SetParent(this.transform);
            LevelSelectOption lso = g.GetComponent<LevelSelectOption>();
            lso.setLevelInfo(li);
        }
    }
}
