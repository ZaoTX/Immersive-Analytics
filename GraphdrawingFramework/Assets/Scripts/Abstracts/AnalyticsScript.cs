/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using System.Collections;
using globals;

/// <summary>
/// This is the interface for analytical scripts. These do not interact with the scene.
/// </summary>
public abstract class AnalyticsScript
{

    /// <summary>
    /// Indicate, if the UI generates a button for the script.
    /// </summary>
    public abstract bool HasGuiButton { get; set; }

    /// <summary>
    /// Indicates, if the UI generates a button to generate a button to show results.
    /// </summary>
    /// <remarks>
    /// At the moment, the UI will just display the results if they are string values.
    /// </remarks>
    public abstract bool HasResultButton { get; set; }

    /// <summary>
    /// Hashtable of Node objects to use for scripts.
    /// </summary>
    public abstract Hashtable Nodes { get; set; }

    /// <summary>
    /// Hashtable of Link objects to use for scripts.
    /// </summary>
    public abstract Hashtable Links { get; set; }

    /// <summary>
    /// A Hashtable which can be filled with results (strings only), so that the UI will show these results.
    /// </summary>
    public abstract Hashtable Results { get; set; }

    /// <summary>
    /// Constructor for the scripts that automatically can assign nodes and links, ready to use.
    /// </summary>
    protected AnalyticsScript()
    {
        Nodes = GlobalVariables.GetGraphNodes(1);
        Links = GlobalVariables.GetGraphLinks(1);
    }

    /// <summary>
    /// Function, which gets bound to a generated UI button.
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Function which gets called automatically, if <see cref="HasResultButton"/> is true.
    /// It adds the results for this script to the global analysis results.
    /// </summary>
    /// <remarks>
    /// This is a temporary solution for storing results and will be improved for the final framework.
    /// </remarks>
    /// <param name="classname"></param>
    public virtual void AddResults(string classname)
    {
        if (!GlobalVariables.AnalyticsResults.ContainsKey(classname))
        {
            GlobalVariables.AnalyticsResults.Add(classname, Results);
        }
        else
        {
            GlobalVariables.AnalyticsResults[classname] = Results;
        }
    }
}