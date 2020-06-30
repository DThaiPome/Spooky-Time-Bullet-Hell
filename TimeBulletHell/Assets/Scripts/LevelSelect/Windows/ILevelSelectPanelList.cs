using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelSelectPanelList : ILevelSelectPanel
{
    /// <summary>
    /// Select the next panel in the list
    /// </summary>
    /// <returns>the newly selected panel</returns>
    ILevelSelectPanel nextPanel();

    /// <summary>
    /// Select the previous panel in the list
    /// </summary>
    /// <returns>the newly selected panel</returns>
    ILevelSelectPanel prevPanel();

    /// <summary>
    /// Select the defualt panel in the list
    /// </summary>
    /// <returns>the newly selected panel</returns>
    ILevelSelectPanel selectDefaultPanel();
}
