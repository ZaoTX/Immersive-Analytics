/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using InheritingViewPort;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ViewGraphs : MonoBehaviour
{

    /// <summary>
    /// The parent.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// The container of generated content.
    /// </summary>
    public GameObject PanelToShow;

    /// <summary>
    /// Function, that enables the graph container and makes it visible.
    /// </summary>
    public void ShowGraphs()
    {
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(PanelToShow);
        cv.StatusText.text = "Showing Available Graphs";
    }
}