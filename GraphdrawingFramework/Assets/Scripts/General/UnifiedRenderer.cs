/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using System.Collections.Generic;
using globals;
using TMPro;
using UnityEngine;

namespace RenderSpace
{

    /// <summary>
    /// The unified renderer as the job to handle the rendering of Nodes and Edges in general.
    /// </summary>
    /// <remarks>
    /// The approach will be optimized in the final framework.
    /// </remarks>
    public class UnifiedRenderer : MonoBehaviour
    {

        /// <summary>
        /// An empty gameobject, which is a placeholder iff the linerenderer is used to 
        /// depict the edges.
        /// </summary>
        public GameObject linkPrefab;

        /// <summary>
        /// Shows the amount of nodes.
        /// </summary>
        public TextMeshProUGUI nodeCount;

        /// <summary>
        /// Shows the amount of edges.
        /// </summary>
        public TextMeshProUGUI edgeCount;


        /// <summary>
        /// Renders nodes and links and adds them to the global variables.
        /// </summary>
        public void RenderGraphs()
        {
            Hashtable Graphs = GlobalVariables.Graphnodes;
            Hashtable nodesToAdd = new Hashtable();
            Hashtable linksToAdd = new Hashtable();
            int graphCount = Graphs.Count;

            for (int k = 1; k <= graphCount; k++)
            {

                Hashtable Graph = (Hashtable)Graphs[k];

                foreach (DictionaryEntry node in Graph)
                {
                    Node n = (Node)node.Value;

                    List<Link> adjacentLinks = n.AdjacentLinks;

                    Object nodeObject = InstantiateNode(n);

                    if (!nodesToAdd.ContainsKey(nodeObject.name))
                    {
                        nodesToAdd.Add(nodeObject.name, nodeObject);
                    }

                    foreach (Link adjacentLink in adjacentLinks)
                    {
                        Node sourceNode = (Node)Graph[adjacentLink.Source];
                        if (n.Id == sourceNode.Id)
                        {
                            Node targetNode = (Node)Graph[adjacentLink.Target];

                            if (sourceNode != targetNode)
                            {
                                Object edgeObject = InstantiateLink(adjacentLink, sourceNode, targetNode);
                                if (!linksToAdd.ContainsKey(adjacentLink.Id))
                                {
                                    linksToAdd.Add(adjacentLink.Id, edgeObject);
                                }
                            }
                        }
                    }
                }
                nodeCount.text = "Nodes: " + nodesToAdd.Count;
                edgeCount.text = "Links: " + linksToAdd.Count;
                GlobalVariables.AddRenderedGraphNodes(nodesToAdd);
                GlobalVariables.AddRenderedGraphLinks(linksToAdd);
            }
        }

        /// <summary>
        /// Handles instantiation of links.
        /// </summary>
        /// <param name="adjacentLink"></param>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        /// <returns>Gameobject of the corresponding link.</returns>
        private Object InstantiateLink(Link adjacentLink, Node sourceNode, Node targetNode)
        {
            var edgePrefab = Resources.Load("Linkobjects/" + GlobalVariables.LinkStyle);
            var edgeObject = Instantiate(GlobalVariables.CustomEdge ? edgePrefab : linkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            edgeObject = ((GameObject)edgeObject).AddComponent<Edge>();
            Edge edge = (Edge)edgeObject;
            if (GlobalVariables.CustomEdge)
            {
                edge.IsCustom = true;
            }
            edge.Color = Color.grey;
            edge.SetNodes(sourceNode, targetNode);
            edge.transform.position = edge.Position;
            edge.Id = adjacentLink.Id;
            edgeObject.name = adjacentLink.Id;
            return edgeObject;
        }

        /// <summary>
        /// Handles the instantiation of nodes.
        /// </summary>
        /// <param name="n"></param>
        /// <returns>Gameobject of the corresponding node.</returns>
        private static Object InstantiateNode(Node n)
        {
            var nodePrefab = Resources.Load("Nodeobjects/" + GlobalVariables.NodeStyle);
            var nodeObject = Instantiate(
                nodePrefab,
                new Vector3(n.X, n.Y, n.Z),
                Quaternion.identity
            );
            nodeObject = ((GameObject)nodeObject).AddComponent<Vertice>();
            nodeObject.name = n.Id;
            return nodeObject;
        }
    }
}