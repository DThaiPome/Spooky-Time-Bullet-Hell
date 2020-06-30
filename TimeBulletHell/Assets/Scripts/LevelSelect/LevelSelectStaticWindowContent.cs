using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectStaticWindowContent : ALevelSelectWindowContents
{
    public override void hideContents()
    {
        this.gameObject.SetActive(false);
    }

    public override void showContents()
    {
        this.gameObject.SetActive(true);
    }
}
