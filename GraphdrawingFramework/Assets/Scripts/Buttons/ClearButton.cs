/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using globals;
using InheritingViewPort;
using TMPro;
using UnityEngine;

/// <summary>
/// A button, which clears the scene.
/// </summary>
public class ClearButton : MonoBehaviour
{

    /// <summary>
    /// The main viewport, inheriting most of the generated content.
    /// </summary>
    /// <remarks>
    /// Included here, so the status text can be set.
    /// </remarks>
    public ContainerViewPort inheritor;


    /// <summary>
    /// Shows the amount of nodes.
    /// </summary>
    public TextMeshProUGUI nodeCount;

    /// <summary>
    /// Shows the amount of edges.
    /// </summary>
    public TextMeshProUGUI edgeCount;

    /// <summary>
    /// Function which clears the scene.
    /// </summary>
    /// <remarks>It does so, by destroying all gameobjects which are vertices or edges (see <see cref="Vertice"/>, <see cref="Edge"/>),
    /// and clears all related hashtables.
    /// </remarks>
    public void ClearScene()
    {
        ContainerViewPort cv = inheritor;
        cv.StatusText.text = "Clearing Scene";
        if (GlobalVariables.RenderedNodes.Count > 0)
        {
            bool customEdges = GlobalVariables.CustomEdge;
            Hashtable nodes = GlobalVariables.RenderedNodes;
            Hashtable links = GlobalVariables.RenderedLinks;
            int nodeCount = nodes.Count;
            int linkCount = links.Count;
            for (int j = 1; j <= linkCount; j++)
            {
                Hashtable graphLinks = (Hashtable)links[j];
                foreach (DictionaryEntry link in graphLinks)
                {
                    ((Edge)link.Value).SelfDestroy();
                }
            }
            for (int k = 1; k <= nodeCount; k++)
            {
                Hashtable graphNodes = (Hashtable)nodes[k];

                foreach (DictionaryEntry node in graphNodes)
                {
                    ((Vertice)node.Value).SelfDestroy();
                }
            }
        }
        GlobalVariables.Graphnodes.Clear();
        GlobalVariables.Graphlinks.Clear();
        GlobalVariables.RenderedNodes.Clear();
        GlobalVariables.RenderedLinks.Clear();
        nodeCount.text = "";
        edgeCount.text = "";
    }
}