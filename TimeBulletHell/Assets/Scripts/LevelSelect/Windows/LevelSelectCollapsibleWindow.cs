using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LevelSelectCollapsibleWindow : MonoBehaviour, ILevelSelectPanel
{
    [SerializeField]
    private KeyCode key;
    [SerializeField]
    private float expandedHeight;
    [SerializeField]
    private float collapsedHeight;
    [SerializeField]
    private ALevelSelectWindowContents aContents;
    private ILevelSelectWindowContents contents;

    private RectTransform rectTransform;

    private bool hasFocus;

    void Awake()
    {
        this.contents = this.aContents;
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        this.focus();
    }

    void Update()
    {
        if (Input.GetKeyDown(this.key))
        {
            if (this.hasFocus)
            {
                this.defocus();
            } else
            {
                this.focus();
            }
        }
    }

    public void defocus()
    {
        this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, this.collapsedHeight);
        this.contents.hideContents();
        this.hasFocus = false;
    }

    public void focus()
    {
        this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, this.expandedHeight);
        this.contents.showContents();
        this.hasFocus = true;
    }
}
