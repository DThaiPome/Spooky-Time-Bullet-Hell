using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScrollArrow : MonoBehaviour
{
    [SerializeField]
    private LevelSelectArrowDirection direction;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.onLevelSelectArrowClicked(this.direction);
        }
    }

    void OnDestroy()
    {
        EventManager.instance.onClickEvent -= this.onClick;
    }
}

public enum LevelSelectArrowDirection
{
    Right, Left
}
