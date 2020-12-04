/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections.Generic;
using Validation;

/// <summary>
/// This is the interface for pipes which use parsers to feed the framework with nodes and links.
/// </summary>
/// <remarks> This will in future take more arguments like whole graph structures and hirarchies.</remarks>
public abstract class Pipe
{

    /// <summary>
    /// List of nodes which has to be filled by the parsing pipe.
    /// </summary>
    public abstract List<Node> Nodes { get; set; }

    /// <summary>
    /// List of links which has to be filled by the parsing pipe.
    /// </summary>
    public abstract List<Link> Links { get; set; }

    /// <summary>
    /// The file type the parser is supporting.
    /// </summary>
    public abstract string FileExtension { get; set; }

    /// <summary>
    /// A description which will be used, if more then one parser are supporting the same file type.
    /// </summary>
    public abstract string Description { get; set; }

    /// <summary>
    /// Function, which has to be implemented and does fill the lists of nodes and links.
    /// </summary>
    /// <param name="filePath">The filepath which will be provided by the framework.</param>
    public abstract void NodesAndLinksToList(string filePath);

    /// <summary>
    /// A function which gets automatically called. It ensures consistent data of nodes and links for the framework.
    /// </summary>
    /// <param name="nodes">List of nodes.</param>
    /// <param name="links">List of links.</param>
    public virtual void InsertGraph(List<Node> nodes, List<Link> links)
    {
        Validator.Validate(nodes, links);
    }
}