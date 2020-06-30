using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelSelectPanelMutableList : ILevelSelectPanelList
{
    /// <summary>
    /// Adds a panel to the list
    /// </summary>
    /// <param name="panel">the panel to add</param>
    void add(ILevelSelectPanel panel);

    /// <summary>
    /// Clears the list of panels
    /// </summary>
    void clear();
}
