using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystems : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameSystems").Length > 1)
        {
            Object.Destroy(this.gameObject);
        }
        Object.DontDestroyOnLoad(this);
    }
}
