using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputAxis
{
    public string name;
    public string positiveName;
    public string negativeName;

    public KeyCode negativeButton;
    public KeyCode positiveButton;
    public KeyCode altNegativeButton;
    public KeyCode altPositiveButton;

    public float gravity;
    public float dead;
    public float sensitivity;

    public bool snap;
    public bool invert;

    public float axis { get; private set; }

    public InputAxis() { }

    public InputAxis(string name, string positiveName, string negativeName, KeyCode negativeButton, KeyCode positiveButton,
        KeyCode altNegativeButton, KeyCode altPositiveButton, float gravity, float dead, float sensitivity, bool snap, bool invert)
    {
        this.name = name;
        this.positiveName = positiveName;
        this.negativeName = negativeName;
        this.negativeButton = negativeButton;
        this.positiveButton = positiveButton;
        this.altNegativeButton = altNegativeButton;
        this.altPositiveButton = altPositiveButton;
        this.gravity = gravity;
        this.dead = dead;
        this.sensitivity = sensitivity;
        this.snap = snap;
        this.invert = invert;
    }

    public void update()
    {
        if (Input.GetKey(this.negativeButton) || Input.GetKey(this.positiveButton) || Input.GetKey(altNegativeButton) || Input.GetKey(this.altPositiveButton))
        {
            //this.onInput();
        } else
        {
            this.axis -= this.gravity;
        }
    }
}
