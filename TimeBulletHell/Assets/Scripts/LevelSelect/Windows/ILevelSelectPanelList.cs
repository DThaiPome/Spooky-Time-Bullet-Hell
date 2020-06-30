using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelSelectPanelList : ILevelSelectPanel
{
    /// <summary>
    /// Select the next panel in the list
    /// </summary>
    void nextPanel();

    /// <summary>
    /// Select the previous panel in the list
    /// </summary>
    void prevPanel();

    /// <summary>
    /// Select the defualt panel in the list
    /// </summary>
    void selectDefaultPanel();
}
