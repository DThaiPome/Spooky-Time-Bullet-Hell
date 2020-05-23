using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
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
}
