/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System;
using System.Collections.Generic;
using System.Linq;
using globals;
using InheritingViewPort;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the creation of buttons based on visual algorithm interface implementations and provides these with respective functionality.
/// </summary>
public class VisualAlgorithmsController : MonoBehaviour
{

    /// <summary>
    /// The visual appearence of buttons instantiated.
    /// </summary>
    public GameObject lookOfButtons;

    /// <summary>
    /// The viewport, inheriting most of the generated UI.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// The container, inheriting the actual content generated.
    /// </summary>
    public GameObject PanelToShow;

    /// <summary>
    /// Triggers the activation of the visual algorithms container.
    /// </summary>
    public void ShowVisualAlgorithms()
    {
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(PanelToShow);
        cv.StatusText.text = "Showing Visual Algorithms";
    }

    /// <summary>
    /// Standard starting function. It generates an interactable button for each
    /// script implementing the VisualScript abstract class.
    /// </summary>
    void Start()
    {
        List<string> scrobjects = GlobalVariables.GetFilesFrom("Visualscripts", "cs");
        var type = typeof(VisualScript);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p != typeof(VisualScript)).ToList();
        for (int i = 0; i < types.Count; i++)
        {
            VisualScript instance = (VisualScript)Activator.CreateInstance(types[i]);
            if (instance.HasGuiButton)
            {
                GameObject btn = Instantiate(lookOfButtons);
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    instance.Nodes = GlobalVariables.GetGraphNodes(1);
                    instance.Links = GlobalVariables.GetGraphLinks(1);
                    instance.RenderedLinks = GlobalVariables.GetRenderedGraphLinks(1);
                    instance.RenderedNodes = GlobalVariables.GetRenderedGraphNodes(1);
                    instance.Execute();
                    instance.UpdateGraph();

                });
                btn.transform.GetChild(0).GetComponent<Text>().text = scrobjects[i];
                btn.transform.SetParent(PanelToShow.transform);
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                btn.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}