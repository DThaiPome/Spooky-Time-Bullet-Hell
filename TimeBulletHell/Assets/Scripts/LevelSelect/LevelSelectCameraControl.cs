using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCameraControl : MonoBehaviour
{
    private Vector2 defaultPos;
    [SerializeField]
    private Vector2 startLevelPos;

    void Awake()
    {
        this.defaultPos = this.transform.position;
    }

    void Start()
    {
        EventManager.instance.onLevelSelectedEvent += this.onLevelSelected;
    }

    void OnDestroy()
    {
        EventManager.instance.onLevelSelectedEvent -= this.onLevelSelected;
    }

    void onLevelSelected(LevelInfo info)
    {
        this.transform.position = new Vector3(this.startLevelPos.x, this.startLevelPos.y, this.transform.position.z);
    }

    void onStartLevelCancelled()
    {
        this.transform.position = new Vector3(this.defaultPos.x, this.defaultPos.y, this.transform.position.z);
    }
}
