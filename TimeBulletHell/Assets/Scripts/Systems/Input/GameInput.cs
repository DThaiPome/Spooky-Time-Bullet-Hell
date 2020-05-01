using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField]
    private List<InputAxis> axes;

    public static GameInput input;

    void Awake()
    {
        input = this;
    }

    void Update()
    {
        foreach(InputAxis axis in this.axes)
        {
            axis.updateRawAxis();
            axis.updateAxis();
        }
    }

    public InputAxis getAxis(string name)
    {
        foreach(InputAxis axis in this.axes)
        {
            if (axis.name == name)
            {
                return axis;
            }
        }
        Debug.Log(name + " is not a valid axis name");
        return null;
    }
}
