using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAuraColorChange : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color slowColor;

    private PlayerMovement pm;
    private Image image;

    void Start()
    {
        this.pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        this.image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        this.image.color = Color.Lerp(this.slowColor, this.normalColor, this.pm.getSpeedPercent());
        this.image.color = new Color(this.image.color.r, this.image.color.g, this.image.color.b, 1);
    }
}
