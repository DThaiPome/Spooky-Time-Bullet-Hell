using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelInfo
{
    [SerializeField]
    public string levelTarget;
    [SerializeField]
    public int roomCount;
    [SerializeField]
    public List<GameObject> mobTypePrefabs;
    [SerializeField]
    public string displayName;

}
