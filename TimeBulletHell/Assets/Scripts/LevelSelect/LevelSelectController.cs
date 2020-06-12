using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField]
    private ALevelSelectPanel leftGroup;
    [SerializeField]
    private ALevelSelectPanel rightGroup;

    // Start is called before the first frame update
    void Start()
    {
        this.leftGroup.focus();
        this.rightGroup.defocus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.focusLeft();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            this.focusRight();
        }
    }

    private void focusLeft()
    {
        this.leftGroup.focus();
        this.rightGroup.defocus();
    }

    private void focusRight()
    {
        this.leftGroup.defocus();
        this.rightGroup.focus();
    }
}
