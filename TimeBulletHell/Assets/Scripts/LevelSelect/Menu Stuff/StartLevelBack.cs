using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelBack : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    void OnDestroy()
    {
        EventManager.instance.onClickEvent -= this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.onStartViewBackButtonClicked();
        }
    }
}
