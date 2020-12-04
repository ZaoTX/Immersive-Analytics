/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using UnityEngine;

/// <summary>
/// A graph edge.
/// </summary>
/// <remarks>
/// Links and edges are different up to now, due to design decisions. 
/// Edges get attached to the gameobject itself to provide operations towards these.
/// Links are the programmatic objects representing such. See <seealso cref="Link"/>
/// Here the color, adjacent nodes, a linerenderer, an identifier, and the positioning can be accessed and changed as properties.
/// Further improvements are planned.
/// </remarks>
public class Edge : MonoBehaviour
{
    /// <summary>
    /// Identifier of the edge.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// A linerenderer of the edgeobject.
    /// </summary>
    /// <remarks>
    /// As linerenderer have no complex geometry to them, the prototype of the framework
    /// provides the usage of such. The benefit lies in rendering dense graphs. When alot of
    /// edges get rendered and the costum variant, implemented by the user for example, of edges
    /// has more complex structures, this might result in alot of triangles and vertices that get rendered.
    /// Such behaviour causes framerate drops. 
    /// </remarks>
    public LineRenderer LineRenderer { get; set; } = new LineRenderer();

    /// <summary>
    /// Use custom edges(true) or linerenderer(false).
    /// </summary>
    public bool IsCustom { get; set; }

    /// <summary>
    /// Sourcenode of the edge.
    /// </summary>
    private Node Source { get; set; }

    /// <summary>
    /// Targetnode of the edge.
    /// </summary>
    private Node Target { get; set; }


    /// <summary>
    /// Private color value, so there will be no Stackoverflow by getting a value by itself with public
    /// </summary>
    /// <remarks>
    /// Grey per default.
    /// </remarks>
    private Color color = Color.grey;

    /// <summary>
    /// The startingposition of the edgeobject that gets rendered.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Adds source and targetnode.
    /// </summary>
    /// <remarks>
    /// It automatically sets the positioning and/or the orientation
    /// of the edge. 
    /// </remarks>
    /// <param name="sourceNode"></param>
    /// <param name="targetNode"></param>
    public void SetNodes(Node sourceNode, Node targetNode)
    {
        Source = sourceNode;
        Target = targetNode;
        Vector3 spawn = new Vector3(Source.X, Source.Y, Source.Z);
        Vector3 target = new Vector3(Target.X, Target.Y, Target.Z);
        if (IsCustom)
        {
            var offset = target - spawn;
            Position = spawn + (offset / 2.0f);
            var scale = new Vector3(0.1f, offset.magnitude / 2.0f, 0.1f);
            gameObject.transform.up = offset;
            gameObject.transform.localScale = scale;
        }
        else
        {
            Position = spawn;
            gameObject.AddComponent<LineRenderer>();
            gameObject.GetComponent<LineRenderer>().startWidth = 0.1f;
            gameObject.GetComponent<LineRenderer>().endWidth = 0.1f;
            gameObject.GetComponent<LineRenderer>().material.color = Color;
            gameObject.GetComponent<LineRenderer>().SetPosition(0, spawn);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(Target.X, Target.Y, Target.Z));
        }
    }

    /// <summary>
    /// Color of the edge.
    /// </summary>
    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            if (IsCustom)
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", value);
            }
            else
            {
                gameObject.GetComponent<LineRenderer>().material.color = value;
            }
            color = value;
        }
    }

    /// <summary>
    /// Removes the gameobject from the scene.
    /// </summary>
    public void SelfDestroy()
    {
        if (IsCustom)
        {
            Destroy(gameObject.GetComponent<LineRenderer>());
        }
        Destroy(gameObject);
    }

}