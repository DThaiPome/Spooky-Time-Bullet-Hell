using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelButton : MonoBehaviour
{
    private string target;

    void Start()
    {
        EventManager.instance.onLevelSelectedEvent += this.onLevelSelected;
        EventManager.instance.onClickEvent += this.onClick;
    }

    void OnDestroy()
    {
        EventManager.instance.onLevelSelectedEvent -= this.onLevelSelected;
        EventManager.instance.onClickEvent -= this.onClick;
    }
    
    private void onLevelSelected(LevelInfo info)
    {
        this.target = info.levelTarget;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.loadLevel();
        }
    }

    private void loadLevel()
    {
        EventManager.instance.switchToLevel(this.target);
    }
}
