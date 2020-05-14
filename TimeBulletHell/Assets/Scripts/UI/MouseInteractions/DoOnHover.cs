using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOnHover : MonoBehaviour
{
    [SerializeField]
    private HoverAction hoverAction;
    [SerializeField]
    private float magnitude;

    private Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onHoverEnterEvent += this.onHoverEnter;
        EventManager.instance.onHoverExitEvent += this.onHoverExit;
        this.defaultScale = this.transform.localScale;
    }

    private void onHoverEnter(Transform t)
    {
        if (t.Equals(this.transform))
        {
            switch(this.hoverAction)
            {
                case HoverAction.Grow:
                    this.transform.localScale *= this.magnitude;
                    break;
            }
        }
    }

    private void onHoverExit(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.transform.localScale = this.defaultScale;
        }
    }

    void OnDestroy()
    {
        EventManager.instance.onHoverEnterEvent -= this.onHoverEnter;
        EventManager.instance.onHoverExitEvent -= this.onHoverExit;
    }

    public enum HoverAction
    {
        Grow
    }
}
