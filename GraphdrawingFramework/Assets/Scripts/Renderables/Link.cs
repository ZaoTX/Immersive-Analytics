/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

/// <summary>
/// A graph link.
/// </summary>
/// <remarks>
/// Links and edges are different up to now, due to design decisions. 
/// See <seealso cref="Edge"/>.
/// Links are the programmatic objects representing such.
/// Here the id, sourcenode id, targetnode id, sourcenode (Node object), targetnode (Node object), and weight
/// can be accessed and changed via properties.
/// Further improvements are planned.
/// </remarks>
public class Link
{

    /// <summary>
    /// Identifier for the link.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Identifier of the sourcenode.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Identifier of the targetnode.
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// Weight of the edge.
    /// </summary>
    public float Weight { get; set; }

    /// <summary>
    /// Nodeobject of the sourcenode.
    /// </summary>
    public Node SourceNode { get; set; }

    /// <summary>
    /// Nodeobject of the targetnode.
    /// </summary>
    public Node TargetNode { get; set; }

    /// <summary>
    /// Initializes a link with its identifier, sourcenode identifier, targetnode identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public Link(string id, string source, string target)
    {
        Id = id;
        Source = source;
        Target = target;
    }

    /// <summary>
    /// Initializes a link with its sourcenode identifier, targetnode identifier.
    /// </summary>
    /// <remarks>
    /// Some inputformats may not include identifier for edges. Therefore, links can be initialized
    /// without them. Identifier will be generated in that case.
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public Link(string source, string target)
    {
        Source = source;
        Target = target;
    }

    /// <summary>
    /// Initializes a link with its sourcenode and targetnode.
    /// </summary>
    /// <remarks>
    /// When generating links in run time it might be useful to just pass the node objects instead of extracting the respective
    /// identifiers.
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public Link(Node source, Node target)
    {
        SourceNode = source;
        TargetNode = target;
    }

    /// <summary>
    /// Initializes a link with its sourcenode identifier and targetnode.
    /// </summary>
    /// <remarks>
    /// For robustness, the passing of identifiers and nodes is included in all four variations.
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public Link(string source, Node target)
    {
        Source = source;
        TargetNode = target;
    }

    /// <summary>
    /// Initializes a link with its sourcenode and targetnode identifier.
    /// </summary>
    /// <remarks>
    /// For robustness, the passing of identifiers and nodes is included in all four variations.
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public Link(Node source, string target)
    {
        SourceNode = source;
        Target = target;
    }

    /// <summary>
    /// Initializes a link with its identifier, only.
    /// </summary>
    /// <remarks>
    /// For the purpose of batch initialization and assigning the source and target afterwards, this
    /// feature is included.
    /// </remarks>
    /// <param name="id"></param>
    public Link(string id)
    {
        Id = id;
    }

    /// <summary>
    /// Takes another link object as input and returns a boolean to indicate adjacency.
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public bool IsAdjacentTo(Link link)
    {
        return Source == link.Source || Source == link.Target || Target == link.Source || Target == link.Target;
    }
}