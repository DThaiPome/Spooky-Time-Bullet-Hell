using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelScenePair
{
    [SerializeField]
    public string levelId;
    [SerializeField]
    public string sceneName;

    public LevelScenePair(string levelId, string sceneName)
    {
        this.levelId = levelId;
        this.sceneName = sceneName;
    }
}
