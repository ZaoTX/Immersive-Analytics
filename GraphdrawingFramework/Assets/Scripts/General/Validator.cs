/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using System.Collections.Generic;
using globals;
using UnityEngine;

namespace Validation
{

    /// <summary>
    /// The validator ensures consistency of graph input for the framework.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Boolean indicator, if nodes get assigned randomized coordinates.
        /// </summary>
        public static bool randomizedCoordinates;

        /// <summary>
        /// Boolean indicator, if the inputted graph is a directed graph.
        /// </summary>
        /// <remarks>
        /// Not relevant for the prototype.
        /// </remarks>
        public static bool directedGraph;

        /// <summary>
        /// Hashtable, which inherits the nodes that will be forwarded to the framework after validation.
        /// </summary>
        private static Hashtable validatedNodes;

        /// <summary>
        /// Hashtable, which inherits the links that will be forwarded to the framework after validation.
        /// </summary>
        private static Hashtable validatedLinks;

        /// <summary>
        /// Validates the nodes and links coming from a pipe function.
        /// </summary>
        /// <param name="nodes">List of Node objects.<see cref="Node"/></param>
        /// <param name="links">List of Link objects.<see cref="Link"/></param>
        public static void Validate(List<Node> nodes, List<Link> links)
        {

            validatedLinks = new Hashtable();
            validatedNodes = new Hashtable();
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];

                ValidateIdentifier(i, node);

                ValidateCoordinates(node);

                if (!validatedNodes.ContainsKey(node.Id))
                {
                    validatedNodes.Add(node.Id, node);
                }
            }

            foreach (Link link in links)
            {
                string linkIDRev = "";


                if (link.Id == null)
                {
                    List<string> generated = GenerateID(link, "Link_");
                    link.Id = generated[0];
                    linkIDRev = generated[1];

                }
                if (!validatedLinks.ContainsKey(link.Id) && (!directedGraph && !validatedLinks.ContainsKey(linkIDRev) || directedGraph))
                {
                    ValidateLinkValues(link);
                    validatedLinks.Add(link.Id, link);
                }

            }
            GlobalVariables.AddGraphNodes(validatedNodes);
            GlobalVariables.AddGraphLinks(validatedLinks);
        }

        /// <summary>
        /// Adds identifier if none is given.
        /// </summary>
        /// <param name="i">Number of iteration.</param>
        /// <param name="node"></param>
        private static void ValidateIdentifier(int i, Node node)
        {
            if (node.Id == null)
            {

                node.Id = GenerateID(i, "Node_");
            }
        }

        /// <summary>
        /// Adds coordinates to nodes if none are given or if random coordinates have to be generated.
        /// </summary>
        /// <param name="node"></param>
        private static void ValidateCoordinates(Node node)
        {
            if (node.X == 0.0f || randomizedCoordinates)
            {
                node.X = GenerateCoordinate(GlobalVariables.minX, GlobalVariables.maxX);
            }
            else
            {
                GlobalVariables.MapMinMax(node.X, GlobalVariables.minX, GlobalVariables.maxX);
            }
            if (node.Y == 0.0f || randomizedCoordinates)
            {
                node.Y = GenerateCoordinate(GlobalVariables.minY, GlobalVariables.maxY);
            }
            else
            {
                GlobalVariables.MapMinMax(node.Y, GlobalVariables.minY, GlobalVariables.maxY);
            }
            if (node.Z == 0.0f || randomizedCoordinates)
            {
                node.Z = GenerateCoordinate(GlobalVariables.minZ, GlobalVariables.maxZ);
            }
            else
            {
                GlobalVariables.MapMinMax(node.Z, GlobalVariables.minZ, GlobalVariables.maxZ);
            }
        }

        /// <summary>
        /// Updates parameters of a link.
        /// </summary>
        /// <param name="link"></param>
        private static void ValidateLinkValues(Link link)
        {
            if (link.Weight == 0.0f)
            {
                link.Weight = 1;
            }

            ((Node)validatedNodes[link.Source]).Degree++;
            ((Node)validatedNodes[link.Target]).Degree++;
            ((Node)validatedNodes[link.Source]).AdjacentLinks.Add(link);
            ((Node)validatedNodes[link.Target]).AdjacentLinks.Add(link);
            ((Node)validatedNodes[link.Source]).Neighbours.Add(link.TargetNode);
            ((Node)validatedNodes[link.Target]).Neighbours.Add(link.SourceNode);
        }


        /// <summary>
        /// Generates new coordinates between lowerbound and upperbound.
        /// </summary>
        /// <param name="min">Lowerbound</param>
        /// <param name="max">Upperbound</param>
        /// <returns></returns>
        private static float GenerateCoordinate(float min, float max)
        {
            return Random.Range(min, max);
        }

        /// <summary>
        /// Generates a new identifier, using a counter.
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GenerateID(int counter, string prefix)
        {
            string result = prefix + "_";
            foreach (char c in counter.ToString())
            {
                result += ((int)c).ToString();
            }
            return result;
        }

        /// <summary>
        /// Generates a new identifier, based on the input object.
        /// </summary>
        /// <param name="obj">Object to name</param>
        /// <param name="prefix">Name prefix for the identifier</param>
        /// <returns>A list of identifiers</returns>
        private static List<string> GenerateID(object obj, string prefix)
        {
            if (obj.GetType() == typeof(Link))
            {
                Link link = (Link)obj;
                string src, tgt;
                src = link.Source ?? (link.SourceNode).Id;
                tgt = link.Target ?? (link.TargetNode).Id;
                link.Source = src;
                link.Target = tgt;
                link.SourceNode = (Node)validatedNodes[src];
                link.TargetNode = (Node)validatedNodes[tgt];
                return new List<string> { prefix + src + tgt, prefix + tgt + src };
            }
            return new List<string>();
        }
    }

}