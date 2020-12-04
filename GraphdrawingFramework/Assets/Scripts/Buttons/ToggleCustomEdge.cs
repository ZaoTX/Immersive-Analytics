/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using globals;
using UnityEngine;

/// <summary>
/// Script for the checkbox, if custom edges are to be used or not.
/// </summary>
public class ToggleCustomEdge : MonoBehaviour
{

    /// <summary>
    /// Sets, if custom edges are used or not.
    /// </summary>
    public void UseCustomEdges()
    {
        GlobalVariables.CustomEdge = !GlobalVariables.CustomEdge;
    }
}