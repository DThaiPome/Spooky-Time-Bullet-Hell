using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataMap : MonoBehaviour
{
    [SerializeField]
    private List<string> levelIds;

    private Dictionary<string, LevelData> levelMap;

    public static LevelDataMap instance;

    void Awake()
    {
        instance = this;

        this.levelMap = new Dictionary<string, LevelData>();
    }

    void Start()
    {
        foreach(string s in levelIds)
        {
            LevelData ld = new LevelData();
            this.levelMap.Add(s, ld);
        }
    }

    public LevelData getData(string id)
    {
        if (this.levelMap.TryGetValue(id, out LevelData ld))
        {
            return ld;
        }
        return null;
    }

    public bool isComplete(string id)
    {
        if (this.levelMap.TryGetValue(id, out LevelData ld))
        {
            return ld.completed;
        }
        return false;
    }
}
