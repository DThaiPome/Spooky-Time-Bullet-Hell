using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGetTextForUI : MonoBehaviour, IGetTextForUI
{
    public abstract string getText();
}
