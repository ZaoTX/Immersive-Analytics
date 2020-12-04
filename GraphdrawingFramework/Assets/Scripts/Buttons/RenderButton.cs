/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using globals;
using TMPro;
using UnityEngine;

namespace RenderSpace
{

    /// <summary>
    /// The script for the render button.
    /// </summary>
    public class RenderButton : MonoBehaviour
    {

        /// <summary>
        /// The status text at the top of the UI.
        /// </summary>
        public TextMeshProUGUI statusText;

        /// <summary>
        /// The function that either initiates the rendering of a graph, or only updates the status.
        /// </summary>
        public void RenderWrapper()
        {
            if (GlobalVariables.NewGraphLoaded)
            {
                statusText.text = "Rendering graph";
                GameObject.Find("Controller").GetComponent<UnifiedRenderer>().RenderGraphs();
                GlobalVariables.NewGraphLoaded = !GlobalVariables.NewGraphLoaded;
            }
            else
            {
                statusText.text = "No Graph selected";
            }
        }

    }
}