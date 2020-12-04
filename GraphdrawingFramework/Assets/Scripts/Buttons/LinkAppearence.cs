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
/// The script for the Link Appearence button.
/// </summary>
public class LinkAppearence : MonoBehaviour
{

    /// <summary>
    /// The visual appearence of generated buttons.
    /// </summary>
    public GameObject LookOfButtons;

    /// <summary>
    /// The parent.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// The container of generated content.
    /// </summary>
    public GameObject PanelToShow;

    /// <summary>
    /// Function, that enables the Link Appearence container and makes it visible.
    /// </summary>
    public void ShowLinkStyles()
    {
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(PanelToShow);
        cv.StatusText.text = "Available Link Appearences";
    }

    /// <summary>
    /// Regular starting function. Populates the Link apearence container with visuals loaded from the respective Resources folder.
    /// </summary>
    void Start()
    {
        List<string> linkStyles = GlobalVariables.GetFilesFrom("Linkobjects", "prefab");
        for (int i = 0; i < linkStyles.Count; i++)
        {
            GameObject btn = Instantiate(LookOfButtons);
            btn.transform.SetParent(PanelToShow.transform);
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                GlobalVariables.LinkStyle = btn.transform.GetChild(0).GetComponent<Text>().text;
            });
            btn.transform.GetChild(0).GetComponent<Text>().text = linkStyles[i];
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            btn.transform.localPosition = new Vector3(0, 0, 0);
        }

    }
}