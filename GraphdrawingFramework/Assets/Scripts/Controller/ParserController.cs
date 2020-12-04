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
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the creation of buttons based on target files and provides these with respective parsing functionality.
/// </summary>
public class ParserController : MonoBehaviour
{

    /// <summary>
    /// Container in the UI inheriting the graph buttons.
    /// </summary>
    public GameObject ContentGraphFiles;

    /// <summary>
    /// The visual appearence of buttons instantiated.
    /// </summary>
    public GameObject LookOfButtons;

    /// <summary>
    /// The button to clear the scene.
    /// </summary>
    public ClearButton clearButton;

    /// <summary>
    /// Container for additional parsers.
    /// </summary>
    /// <remarks>
    /// This happens, if multiple parsers for the same file extension are provided.
    /// </remarks>
    public GameObject parserContainer;

    /// <summary>
    /// The viewport, inheriting most of the generated UI.
    /// </summary>
    public ContainerViewPort inheritor;

    /// <summary>
    /// Regular start function, enabling the usage of parsing buttons.
    /// </summary>
    /// <remarks>
    /// It obtains all graphnames from the respective resources folder and maps the provided parsers 
    /// over them. For each file with a fileextension for which a parser exists, it generates a button.
    /// If there are more then one parser for a fileextension, the graph button opens a selection of available parsers,
    /// showing the description of the parser as button name.
    /// </remarks>
    void Start()
    {
        List<string> graphs = GlobalVariables.GetFullFilesFrom("Graphs", "");
        Hashtable graphExtensions = new Hashtable();
        List<string> extensions = new List<string>();
        Hashtable instances = new Hashtable();
        ContainerViewPort cv = inheritor;

        // Sortiere Graphen nach extensions
        for (int k = 0; k < graphs.Count; k++)
        {
            string extension = graphs[k].Split('.')[graphs[k].Split('.').Length - 1];

            if (graphExtensions.ContainsKey(extension))
            {
                ((List<string>)graphExtensions[extension]).Add(graphs[k]);
            }
            else
            {
                List<string> tmpGraphs = new List<string>
                {
                    graphs[k]
                };
                graphExtensions.Add(extension, tmpGraphs);
            }

        }
        // Ermittle Parser
        var type = typeof(Pipe);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p != typeof(Pipe)).ToList();

        // Generiere Instanzen der Parser mit mapping auf fileextensions
        for (int k = 0; k < types.Count; k++)
        {
            Pipe instance = (Pipe)Activator.CreateInstance(types[k]);
            extensions.Add(instance.FileExtension);
            if (instances.ContainsKey(instance.FileExtension))
            {
                ((List<Pipe>)instances[instance.FileExtension]).Add(instance);
            }
            else
            {
                List<Pipe> tmpPipes = new List<Pipe>
                {
                    instance
                };
                instances.Add(instance.FileExtension, tmpPipes);
            }
        }

        // Hole alle Graphen
        foreach (DictionaryEntry graphList in graphExtensions)
        {

            string currentFileextension = (string)graphList.Key;
            List<string> currentGraphs = (List<string>)graphList.Value;
            // Pro Graph, hole alle parser
            if (instances.ContainsKey(currentFileextension))
            {
                foreach (string currentGraph in currentGraphs)
                {
                    List<Pipe> currentParsers = (List<Pipe>)instances[currentFileextension];
                    GameObject graphButton = NewGameObject(ContentGraphFiles, LookOfButtons);
                    graphButton.transform.GetChild(0).GetComponent<Text>().text = currentGraph;
                    if (currentParsers.Count == 1)
                    {
                        // Wenn #parser == 1
                        Pipe currentParser = currentParsers[0];
                        graphButton.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            if (GlobalVariables.Graphnodes.Count > 0) { clearButton.ClearScene(); }
                            cv.StatusText.text = "Graphfile " + currentGraph + " was chosen";
                            GlobalVariables.NewGraphLoaded = !GlobalVariables.NewGraphLoaded;
                            currentParser.NodesAndLinksToList(currentGraph);
                            currentParser.InsertGraph(currentParser.Nodes, currentParser.Links);
                        });

                    }
                    else if (currentParsers.Count > 1)
                    {
                        // Wenn #parser > 1
                        GameObject thisGraphsContainer = NewGameObject(inheritor, parserContainer);
                        graphButton.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            cv.ShowPanel(thisGraphsContainer);
                            GlobalVariables.ChosenGraph = graphButton.transform.GetChild(0).GetComponent<Text>().text;
                            cv.StatusText.text = "Graphfile " + GlobalVariables.ChosenGraph + " was chosen";
                        });
                        foreach (Pipe currentParser in currentParsers)
                        {
                            GameObject chooseParserButton = NewGameObject(thisGraphsContainer, LookOfButtons);
                            chooseParserButton.transform.GetChild(0).GetComponent<Text>().text = currentParser.Description;
                            chooseParserButton.GetComponent<Button>().onClick.AddListener(() =>
                            {
                                if (GlobalVariables.Graphnodes.Count > 0) { clearButton.ClearScene(); }
                                GlobalVariables.NewGraphLoaded = !GlobalVariables.NewGraphLoaded;
                                currentParser.NodesAndLinksToList(GlobalVariables.ChosenGraph);
                                currentParser.InsertGraph(currentParser.Nodes, currentParser.Links);
                            });
                        }

                    }

                }
            }

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
        GameObject button = Instantiate(appearence);
        button.transform.SetParent(parent.transform);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        button.transform.localPosition = new Vector3(0, 0, 0);

        return button;
    }

    /// <summary>
    /// Creates a new gameobject, located in the parent gameobject (special case for viewport).
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="appearence"></param>
    /// <remarks>
    /// This can be optimized in future.
    /// </remarks>
    /// <returns></returns>
    private GameObject NewGameObject(ContainerViewPort parent, GameObject appearence)
    {
        GameObject button = Instantiate(appearence);
        button.transform.SetParent(parent.transform);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        button.transform.localPosition = new Vector3(0, 0, 0);

        return button;
    }
}