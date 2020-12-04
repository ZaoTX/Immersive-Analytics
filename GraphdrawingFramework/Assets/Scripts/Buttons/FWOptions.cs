/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle
/// 
using InheritingViewPort;
using UnityEngine;

/// <summary>
/// The script for the Options button.
/// </summary>
public class FWOptions : MonoBehaviour
{

    /// <summary>
    /// Container for options inside the main viewport (see <see cref="ContainerViewPort"/>
    /// </summary>
    public GameObject optionsContainer;

    /// <summary>
    /// The parent.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// Function, that enables the options container and makes it visible.
    /// </summary>
    public void OpenOptions()
    {
        ContainerViewPort cv = inheritor;
        cv.StatusText.text = "Choose Options";
        cv.ShowPanel(optionsContainer);
    }
}