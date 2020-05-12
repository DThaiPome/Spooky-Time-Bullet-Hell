using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.endLevelEvent += this.onLevelEnd;
        this.gameObject.SetActive(false);
    }

    private void onLevelEnd()
    {
        this.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        EventManager.instance.endLevelEvent -= this.onLevelEnd;
    }
}
