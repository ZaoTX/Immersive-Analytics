/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using globals;
using InheritingViewPort;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the creation of buttons based on analytical algorithm interface implementations and provides these with respective functionality.
/// </summary>
public class AnalyticsController : MonoBehaviour
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
    /// A container inheriting the textmeshes with the results.
    /// </summary>
    public GameObject resultPanel;

    /// <summary>
    /// A textmesh which then inherits the results.
    /// </summary>
    public GameObject textMesh;

    /// <summary>
    /// Triggers the activation of the analytical algorithms container.
    /// </summary>
    public void ShowAnalyticalAlgorithms()
    {
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(PanelToShow);
        cv.StatusText.text = "Showing Analytical Algorithms";
    }

    /// <summary>
    /// Standard starting function. It generates an interactable button for each
    /// script implementing the AnalyticsScript abstract class.
    /// </summary>
    void Start()
    {
        List<string> scripts = GlobalVariables.GetFilesFrom("Analyticsscripts", "cs");
        var type = typeof(AnalyticsScript);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p != typeof(AnalyticsScript)).ToList();
        Debug.Log(types.Count);
        for (int i = 0; i < types.Count; i++)
        {
            //Debug.Log(scripts[i]);
            AnalyticsScript instance = (AnalyticsScript)Activator.CreateInstance(types[i]);
            if (instance.HasGuiButton)
            {
                GameObject btn = NewGameObject(PanelToShow, lookOfButtons);
                //original
                //btn.transform.GetChild(0).GetComponent<Text>().text = scripts[i]; 
                //modified
                btn.transform.GetChild(0).GetComponent<Text>().text = types[i].ToString();
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    instance.Nodes = GlobalVariables.GetGraphNodes(1);
                    instance.Links = GlobalVariables.GetGraphLinks(1);
                    instance.Execute();

                });
                if (instance.HasResultButton)
                {
                    btn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ChangeButtonsForResults(instance);
                    });
                }
            }
        }
    }

    /// <summary>
    /// Hides the buttons of analytical algorithms and shows the outcome of the executed one.
    /// </summary>
    /// <param name="instance"></param>
    private void ChangeButtonsForResults(AnalyticsScript instance)
    {
        instance.AddResults(instance.ToString());
        ContainerViewPort cv = inheritor;
        cv.ShowPanel(resultPanel);
        cv.StatusText.text = "Showing Results of " + instance.ToString();
        foreach (DictionaryEntry result in instance.Results)
        {
            GameObject textmeshKey = NewGameObject(resultPanel, textMesh);
            GameObject textmeshValue = NewGameObject(resultPanel, textMesh);
            textmeshKey.GetComponent<TextMeshProUGUI>().SetText((string)result.Key);
            textmeshValue.GetComponent<TextMeshProUGUI>().SetText(result.Value.ToString());

        }
    }

    /// <summary>
    /// Creates a new gameobject, located in the parent gameobject.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="appearence"></param>
    /// <returns></returns>
    private GameObject NewGameObject(GameObject parent, GameObject appearence)
    {
        GameObject obj = Instantiate(appearence);
        obj.transform.SetParent(parent.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        obj.transform.localPosition = new Vector3(0, 0, 0);

        return obj;
    }
}