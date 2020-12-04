/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace globals
{

    /// <summary>
    /// This class inherits variables and operations that are globally accessible.
    /// </summary>
    /// <remarks>
    /// Due to my learning process (and progress) I will in future have to reduce
    /// this class down to a minimum.
    /// Nodes and links will wander off into Graph classes and a lot of the functionality
    /// with them. Until now, only one graph at a time is supposed to be shown.
    /// </remarks>
    static class GlobalVariables
    {

        /// <summary>
        /// A hashtable that inherits all Graphs with their nodes.
        /// </summary>
        /// <remarks>
        /// Its only the nodes, to quickly access information about these.
        /// In future, this wont be necessary anymore.
        /// </remarks>
        public static Hashtable Graphnodes { get; set; } = new Hashtable();

        /// <summary>
        /// A hashtable that inherits all Graphs with their links.
        /// </summary>
        /// <remarks>
        /// Its only the links, to quickly access information about these.
        /// In future, this wont be necessary anymore.
        /// </remarks>
        public static Hashtable Graphlinks { get; set; } = new Hashtable();

        /// <summary>
        /// A hashtable that inherits all Graphs with their nodes (rendered in the scene).
        /// </summary>
        /// <remarks>
        /// A hashtable with the actually rendered gameobjects to alter those quickly.
        /// In future, this wont be necessary anymore.
        /// </remarks>
        public static Hashtable RenderedNodes { get; set; } = new Hashtable();

        /// <summary>
        /// A hashtable that inherits all Graphs with their links (rendered in the scene).
        /// </summary>
        /// <remarks>
        /// A hashtable with the actually rendered gameobjects to alter those quickly.
        /// In future, this wont be necessary anymore.
        /// </remarks>
        public static Hashtable RenderedLinks { get; set; } = new Hashtable();

        /// <summary>
        /// A hashtable, which can inherit the results of analytical scripts.
        /// </summary>
        /// <remarks>
        /// This feature enables the framework to automatically access results and display them
        /// in the results field. A more convenient method will be provided in the future.
        /// </remarks>
        public static Hashtable AnalyticsResults { get; set; } = new Hashtable();

        /// <summary>
        /// A boolean, indicating if edges get custom designs or will be drawn with a linerenderer.
        /// </summary>
        public static bool CustomEdge { get; set; } = true;

        /// <summary>
        /// A boolean, indicating if a new graph has been loaded.
        /// </summary>
        /// <remarks>
        /// This is for the prototype. That way, the prototype can control, if a graph has to be removed first or not.
        /// </remarks>
        public static bool NewGraphLoaded { get; set; }

        /// <summary>
        /// Indicator, which node style to load from the resources.
        /// </summary>
        public static string NodeStyle { get; set; }

        /// <summary>
        /// Indicator, which link style to load from the resources.
        /// </summary>
        public static string LinkStyle { get; set; }

        /// <summary>
        /// Identifier that determines, which graph to load from the node graphs.
        /// </summary>
        /// <remarks>
        /// This will be discontinued or reworked in the final framework, as graphobjects themselves will be selectable.
        /// </remarks>
        public static int SelectedGraphID { get; set; } = 1;

        /// <summary>
        /// Identifier which tells the canvas, which camera to detect.
        /// </summary>
        /// <remarks>
        /// This will be discontinued with another raycast approach.
        /// </remarks>
        public static Camera SelectedHand { get; set; }

        /// <summary>
        /// Indicates the name of the graph file clicked on for the status.
        /// </summary>
        public static string ChosenGraph { get; set; } = "";

        /// <summary>
        /// Lowerbound for the x-position for graph nodes.
        /// </summary>
        public static float minX = -50.0f;

        /// <summary>
        /// Lowerbound for the y-position for graph nodes.
        /// </summary>
        public static float minY = 0.0f;

        /// <summary>
        /// Lowerbound for the z-position for graph nodes.
        /// </summary>
        public static float minZ = -50.0f;

        /// <summary>
        /// Upperbound for the x-position for graph nodes.
        /// </summary>
        public static float maxX = 50.0f;

        /// <summary>
        /// Upperbound for the y-position for graph nodes.
        /// </summary>
        public static float maxY = 100.0f;

        /// <summary>
        /// Upperbound for the z-position for graph nodes.
        /// </summary>
        public static float maxZ = 50.0f;

        /// <summary>
        /// Getter for the nodes hashtable at a given position.
        /// </summary>
        /// <param name="num">
        /// The position for the graph.
        /// </param>
        /// <returns>Hashtable with node objects</returns>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static Hashtable GetGraphNodes(int num)
        {
            return (Hashtable)Graphnodes[num];
        }

        /// <summary>
        /// Getter for the nodes (rendered) hashtable at a given position.
        /// </summary>
        /// <param name="num">
        /// The position for the graph.</param>
        /// <returns>Hashtable with rendered node objects.</returns>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static Hashtable GetRenderedGraphNodes(int num)
        {
            return (Hashtable)RenderedNodes[num];
        }

        /// <summary>
        /// Getter for the links hashtable at a given position.
        /// </summary>
        /// <param name="num"> The position for the graph.</param>
        /// <returns>Hashtable with link objects.</returns>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static Hashtable GetGraphLinks(int num)
        {
            return (Hashtable)Graphlinks[num];
        }

        /// <summary>
        /// Getter for the links (rendered) hashtable at a given position.
        /// </summary>
        /// <param name="num"> The position for the graph.</param>
        /// <returns>Hashtable with rendered Link objects.</returns>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static Hashtable GetRenderedGraphLinks(int num)
        {
            return (Hashtable)RenderedLinks[num];
        }

        /// <summary>
        /// Adds a new Hashtable with Node objects to the Graph hashtables.
        /// </summary>
        /// <param name="nodes">Hashtable(string nodeID, Node node)</param>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static void AddGraphNodes(Hashtable nodes)
        {
            int idx = Graphnodes.Count + 1;
            Graphnodes.Add(idx, nodes);
        }

        /// <summary>
        /// Adds a new Hashtable with gameobjects to the Graph hashtables.
        /// </summary>
        /// <param name="nodes">Hashtable(string nodeID, GameObject node)</param>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static void AddRenderedGraphNodes(Hashtable nodes)
        {
            int idx = RenderedNodes.Count + 1;
            RenderedNodes.Add(idx, nodes);
        }

        /// <summary>
        /// Adds a new Hashtable with Link objects to the Graph hashtables.
        /// </summary>
        /// <param name="links">Hashtable(string linkID, Link link)</param>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static void AddGraphLinks(Hashtable links)
        {
            int idx = Graphlinks.Count + 1;
            Graphlinks.Add(idx, links);
        }

        /// <summary>
        /// Adds a new Hashtable with gameobjects to the Graph hashtables.
        /// </summary>
        /// <param name="links">Hashtable(string linkID, GameObject link)</param>
        /// <remarks>Will be discontinued once the graph objects are included.</remarks>
        public static void AddRenderedGraphLinks(Hashtable links)
        {
            int idx = RenderedLinks.Count + 1;
            RenderedLinks.Add(idx, links);
        }

        /// <summary>
        /// Scales a vector between the minimum and maximum values. These are defined in <see cref="GlobalVariables"/>. 
        /// </summary>
        /// <param name="position">Position vector of a node.</param>
        /// <returns>A scaled vector.</returns>
        public static Vector3 ScaleVector(Vector3 position)
        {
            return new Vector3(MapMinMax(position.x, minX, maxX), MapMinMax(position.y, minY, maxY), MapMinMax(position.z, minZ, maxZ));
        }

        /// <summary>
        /// Linear mapping between a lowerbound and an upperbound.
        /// </summary>
        /// <param name="value">Float value to scale.</param>
        /// <param name="minVal">Lowerbound for scaling.</param>
        /// <param name="maxVal">Upperbound for scaling.</param>
        /// <returns></returns>
        public static float MapMinMax(float value, float minVal, float maxVal)
        {
            return ((value - minVal) / (maxVal - minVal)) * (maxVal - minVal) + minVal;
        }

        /// <summary>
        /// Returns a list containing the names of all files from a given directory, depending on the given extension (empty string: all files).
        /// </summary>
        /// <param name="directoryInResources"></param>
        /// <param name="extension"></param>
        /// <returns>List<string> of names in directory, having given extension.</string></returns>
        public static List<string> GetFilesFrom(string directoryInResources, string extension)
        {
            List<string> result = new List<string>();
            string myPath = "Assets/Resources/" + directoryInResources;
            DirectoryInfo dir = new DirectoryInfo(myPath);
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
            {
                if (f.Extension == ("." + extension) || (extension == "" && f.Extension != ".meta"))
                {
                    string tempName = f.Name;
                    string ext = f.Extension;
                    string strippedName = tempName.Replace(ext, "");
                    result.Add(strippedName);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a list containing the names of all files from a given directory (with extension), depending on the given extension (empty string: all files).
        /// </summary>
        /// <param name="directoryInResources"></param>
        /// <param name="extension"></param>
        /// <returns>List<string> of names in directory, having given extension.</string></returns>
        public static List<string> GetFullFilesFrom(string directoryInResources, string extension)
        {
            List<string> result = new List<string>();
            string myPath = "Assets/Resources/" + directoryInResources;
            DirectoryInfo dir = new DirectoryInfo(myPath);
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
            {
                if (f.Extension == ("." + extension) || (extension == "" && f.Extension != ".meta"))
                {
                    result.Add(f.Name);
                }
            }
            return result;
        }

        /// <summary>
        /// Updates the coordinates of the nodes in the Hashtable of not rendered nodes to the 
        /// positions of the rendered ones.
        /// </summary>
        /// <remarks>
        /// This approach will be discontinued in the final framework.
        /// </remarks>
        private static void UpdateNodes()
        {
            Hashtable graph = GetGraphNodes(SelectedGraphID);
            Hashtable renderedg = GetRenderedGraphNodes(SelectedGraphID);
            foreach (DictionaryEntry d in renderedg)
            {
                string id = (string)d.Key;
                ((Node)graph[id]).X = ((Vertice)renderedg[id]).transform.position.x;
                ((Node)graph[id]).Y = ((Vertice)renderedg[id]).transform.position.y;
                ((Node)graph[id]).Z = ((Vertice)renderedg[id]).transform.position.z;
            }

        }

        /// <summary>
        /// Updates the source and target links of links and adjusts their orientation.
        /// </summary>
        /// <remarks>
        /// This approach will be discontinued in the final framework.
        /// </remarks>
        private static void UpdateLinks()
        {
            Hashtable graph = GetGraphNodes(SelectedGraphID);
            Hashtable links = GetGraphLinks(SelectedGraphID);
            Hashtable renderedl = GetRenderedGraphLinks(SelectedGraphID);
            foreach (DictionaryEntry d in links)
            {
                Node srcNode = (Node)graph[((Link)d.Value).Source];
                Node tgtNode = (Node)graph[((Link)d.Value).Target];
                if (srcNode != tgtNode)
                {
                    Edge edge = (Edge)renderedl[(string)d.Key];
                    edge.SetNodes(srcNode, tgtNode);
                    edge.transform.position = edge.Position;
                }
            }
        }

        /// <summary>
        /// Calls both update functions to simplify external calls.
        /// </summary>
        /// <remarks>
        /// This approach will be discontinued in the final framework.
        /// </remarks>
        public static void Update()
        {
            UpdateNodes();
            UpdateLinks();
        }
    }

}