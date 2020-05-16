using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelIcon : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        this.sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        EventManager.instance.onLevelSelectedEvent += this.onLevelSelected;
    }

    void OnDestroy()
    {
        EventManager.instance.onLevelSelectedEvent -= this.onLevelSelected;
    }

    private void onLevelSelected(LevelInfo info)
    {
        this.sr.sprite = info.icon;
    }
}
