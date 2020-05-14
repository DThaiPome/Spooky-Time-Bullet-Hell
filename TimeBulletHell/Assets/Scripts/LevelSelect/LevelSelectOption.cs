using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectOption : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;
    
    public LevelInfo getLevelInfo()
    {
        return this.levelInfo;
    }
}
