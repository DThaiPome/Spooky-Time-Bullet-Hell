using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    [SerializeField]
    private string levelName;
    [SerializeField]
    private string sceneTarget;
    [SerializeField]
    private ALevelPrereq prereq;

    [HideInInspector]
    public bool completed;
    public int highScore { get; private set; }

    public LevelData()
    {
        this.completed = false;
        this.highScore = 0;
    }

    public int setHighScore(int score)
    {
        this.highScore = score > this.highScore ? score : this.highScore;
        return this.highScore;
    }

    public int resetHighScore()
    {
        return this.highScore = 0;
    }

    public string getLevelName()
    {
        return levelName;
    }

    public bool preReqMet()
    {
        return this.prereq.isAvailable();
    }
}
