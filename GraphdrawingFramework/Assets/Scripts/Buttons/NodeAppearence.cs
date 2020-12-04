/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections.Generic;
using globals;
using InheritingViewPort;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The script for the Node Appearence button.
/// </summary>
public class NodeAppearence : MonoBehaviour
{

    /// <summary>
    /// The visual appearence of generated buttons.
    /// </summary>
    public GameObject nodeobjButton;

    /// <summary>
    /// The parent.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// The container of generated content.
    /// </summary>
    public GameObject PanelToShow;

    /// <summary>
    /// Function, that enables the Node Appearence container and makes it visible.
    /// </summary>
    public void ShowButtonStyles()
    {
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(PanelToShow);
        cv.StatusText.text = "Available Node Appearences";
    }

    /// Regular starting function. Populates the Node apearence container with visuals loaded from the respective Resources folder.
    void Start()
    {
        List<string> nodeStyles = GlobalVariables.GetFilesFrom("Nodeobjects", "prefab");
        for (int i = 0; i < nodeStyles.Count; i++)
        {
            GameObject btn = Instantiate(nodeobjButton);
            btn.transform.SetParent(PanelToShow.transform);
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                GlobalVariables.NodeStyle = btn.transform.GetChild(0).GetComponent<Text>().text;
            });
            btn.transform.GetChild(0).GetComponent<Text>().text = nodeStyles[i];
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            btn.transform.localPosition = new Vector3(0, 0, 0);
        }

    }
}