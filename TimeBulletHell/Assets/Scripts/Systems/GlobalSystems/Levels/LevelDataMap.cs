using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataMap : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> levelData;

    private Dictionary<string, LevelData> levelMap;

    public static LevelDataMap instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this.levelMap = new Dictionary<string, LevelData>();
    }

    void Start()
    {
        EventManager.instance.onLevelCompleteEvent += this.levelComplete;
        foreach(LevelData ld in this.levelData)
        {
            this.levelMap.Add(ld.getLevelName(), ld);
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

    private void levelComplete(string id)
    {
        if (this.levelMap.TryGetValue(id, out LevelData ld))
        {
            ld.completed = true;
        }
    }
}
