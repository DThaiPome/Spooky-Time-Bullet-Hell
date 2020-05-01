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
    public int rawAxis { get; private set; }

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

    public void updateAxis()
    {
        if (this.rawAxis != 0)
        {
            this.updateAxisHelper();
        } else
        {
            this.axis -= this.gravity * Time.deltaTime * Mathf.Sign(this.axis);
            if (Mathf.Abs(this.axis) <= this.dead)
            {
                this.axis = 0;
            }
        }
    }

    private void updateAxisHelper()
    {
        if (snap)
        {
            if (this.rawAxis == 1 && this.axis < 0)
            {
                this.axis = 0;
            }
            if (this.rawAxis == -1 && this.axis > 0)
            {
                this.axis = 0;
            }
        }
        this.axis += this.sensitivity * this.rawAxis * Time.deltaTime;
        this.axis = Mathf.Clamp(this.axis, -1, 1);
    }

    public void updateRawAxis()
    {
        bool negative = Input.GetKey(this.negativeButton) || Input.GetKey(this.altNegativeButton);
        bool positive = Input.GetKey(this.positiveButton) || Input.GetKey(this.altPositiveButton);
        if (negative)
        {
            this.rawAxis = -1;
        }
        if (positive)
        {
            this.rawAxis = 1;
        }
        if ((negative && positive) || (!negative && !positive))
        {
            this.rawAxis = 0;
        }
        if (invert)
        {
            this.rawAxis *= -1;
        }
    }

    public bool positive()
    {
        return Input.GetKey(this.positiveButton) || Input.GetKey(this.altPositiveButton);
    }

    public bool positiveDown()
    {
        return Input.GetKeyDown(this.positiveButton) || Input.GetKeyDown(this.altPositiveButton);
    }

    public bool positiveUp()
    {
        return Input.GetKeyUp(this.positiveButton) || Input.GetKeyUp(this.altPositiveButton);
    }

    public bool negative()
    {
        return Input.GetKey(this.negativeButton) || Input.GetKey(this.altNegativeButton);
    }

    public bool negativeDown()
    {
        return Input.GetKeyDown(this.negativeButton) || Input.GetKeyDown(this.altNegativeButton);
    }

    public bool negativeUp()
    {
        return Input.GetKeyUp(this.negativeButton) || Input.GetKeyUp(this.altNegativeButton);
    }
}
