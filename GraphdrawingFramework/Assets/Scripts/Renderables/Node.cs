/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System;
using System.Collections.Generic;

/// <summary>
/// A graph node.
/// </summary>
/// <remarks>
/// Vertices and nodes are different up to now, due to design decisions. 
/// See <seealso cref="Vertice"/>.
/// Nodes are the programmatic objects representing such.
/// Here the id, coordinates, nodedegree and adjacent links and direct neighbours 
/// can be accessed and changed via properties.
/// Further improvements are planned.
/// </remarks>
public class Node
{


    /// <summary>
    /// Identifier of the node.
    /// </summary>
    public string Id { get; set; }
    public double p; // Probability for nibble algorithm
    /// <summary>
    /// X coordinate of location.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Y coordinate of location.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Z coordinate of location.
    /// </summary>
    public float Z { get; set; }

    /// <summary>
    /// Node degree.
    /// </summary>
    public int Degree { get; set; }

    /// <summary>
    /// List of links coming from or pointing to the node.
    /// </summary>
    public List<Link> AdjacentLinks { get; set; } = new List<Link>();

    /// <summary>
    /// List of neighbouring nodes.
    /// </summary>
    public List<Node> Neighbours { get; set; } = new List<Node>();

    /// <summary>
    /// Initializes a node with respective information.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public Node(string id, float x, float y, float z)
    {
        Id = id;
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Initializes a node with only the id.
    /// </summary>
    /// <remarks>
    /// In some graph fileformats nodes have only identifiers, but no
    /// particular positioning. In this case, they can be initialized just with their identifiers.
    /// </remarks>
    /// <param name="id"></param>
    public Node(string id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a blank node.
    /// </summary>
    /// <remarks>
    /// It is possible to face graphfiles without identifiers for nodes
    /// or inconsistent identifiers. In this case nodes have to be initialized
    /// without such. They will be generated later on.
    /// </remarks>
    public Node() { }

    public static explicit operator Node(List<Node> v)
    {
        throw new NotImplementedException();
    }
}