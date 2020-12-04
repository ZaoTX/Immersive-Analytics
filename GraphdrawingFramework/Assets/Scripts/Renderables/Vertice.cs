/// Graph Drawing framework prototype
///
/// This prototype is supposed to be an outlook to a future version of a framework
/// to draw, interact with, compare, and analyze graphs and its components in VR.
///
/// Author: Wilhelm Kerle

using UnityEngine;

/// <summary>
/// A graph Vertice. 
/// </summary>
/// <remarks>
/// Vertices and nodes are different up to now, due to design decisions. 
/// Vertices get attached to the gameobject itself to provide operations towards these.
/// Nodes are the programmatic objects representing such. See <seealso cref="Node"/>
/// Here the color can be accessed and changed as properties.
/// Further improvements are planned.
/// </remarks>
public class Vertice : MonoBehaviour
{
    /// <summary>
    /// Private color value, so there will be no Stackoverflow by getting a value by itself with public
    /// accessor.
    /// </summary>
    private Color color;

    /// <summary>
    /// Color of Vertice.
    /// </summary>
    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", value);
            color = value;
        }
    }

    /// <summary>
    /// Removes the gameobject from the scene.
    /// </summary>
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}