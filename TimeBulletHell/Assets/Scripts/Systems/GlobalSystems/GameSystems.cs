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
            Debug.Log("DESTROYED");
            Object.Destroy(this.gameObject);
        }
        Object.DontDestroyOnLoad(this);
    }
}
