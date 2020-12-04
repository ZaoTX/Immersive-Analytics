/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using globals;

/// <summary>
/// This is the interface for visual scripts. These can interact with the scene.
/// </summary>
public abstract class VisualScript
{

    /// <summary>
    /// Indicate, if the UI generates a button for the script.
    /// </summary>
    public abstract bool HasGuiButton { get; set; }

    /// <summary>
    /// Hashtable of Node objects to use for scripts.
    /// </summary>
    public abstract Hashtable Nodes { get; set; }

    /// <summary>
    /// Hashtable of Link objects to use for scripts.
    /// </summary>
    public abstract Hashtable Links { get; set; }

    /// <summary>
    /// Hashtable of rendered nodes to use for scripts.
    /// </summary>
    public abstract Hashtable RenderedNodes { get; set; }

    /// <summary>
    /// Hashtable of rendered links to use for scripts.
    /// </summary>
    public abstract Hashtable RenderedLinks { get; set; }

    /// <summary>
    /// Constructor for the scripts that automatically can assign nodes and links, ready to use.
    /// </summary>
    protected VisualScript()
    {
        Nodes = GlobalVariables.GetGraphNodes(1);
        Links = GlobalVariables.GetGraphLinks(1);
        RenderedNodes = GlobalVariables.GetRenderedGraphNodes(1);
        RenderedLinks = GlobalVariables.GetRenderedGraphLinks(1);
    }

    /// <summary>
    /// Function, which gets bound to a generated UI button.
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Function which will automatically be called, updating the positions of nodes and links after execution of the script.
    /// </summary>
    public virtual void UpdateGraph()
    {
        GlobalVariables.Update();
    }
}