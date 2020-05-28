using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompleteLevelsFirstPrereq", menuName = "LevelPrereqObjects/CompleteLevelsFirstPrereq", order = 1)]
public class CompleteLevelsFirstPrereq : ALevelPrereq
{
    [SerializeField]
    private List<string> levelsToComplete;

    public override bool isAvailable()
    {
        foreach(string level in this.levelsToComplete)
        {
            if (!LevelDataMap.instance.isComplete(level))
            {
                return false;
            }
        }
        return true;
    }
}
