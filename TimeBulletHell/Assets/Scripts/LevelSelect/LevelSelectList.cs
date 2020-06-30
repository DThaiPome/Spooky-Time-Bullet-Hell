using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectList : MonoBehaviour
{
    [SerializeField]
    private LevelSelectMutableWindowGroup controlPanel;
    private ILevelSelectPanelMutableList iControlPanel;
    [SerializeField]
    private List<LevelInfo> levels;
    [SerializeField]
    private GameObject levelOptionPrefab;

    void Awake()
    {
        this.iControlPanel = this.controlPanel;
    }

    void Start()
    {
        foreach(LevelInfo li in this.levels)
        {
            GameObject g = Object.Instantiate(levelOptionPrefab);
            g.transform.SetParent(this.transform);
            LevelSelectOption lso = g.GetComponent<LevelSelectOption>();
            lso.setLevelInfo(li);
            this.iControlPanel.add(lso);
        }
    }
}
