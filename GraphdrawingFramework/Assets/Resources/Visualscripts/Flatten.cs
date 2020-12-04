using System.Collections;
using globals;
using UnityEngine;

public class Flatten : VisualScript {
    public override bool HasGuiButton { get; set; } = true;
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; }
    public override Hashtable RenderedNodes { get; set; }
    public override Hashtable RenderedLinks { get; set; }

    private void Flat () {
        Hashtable g = GlobalVariables.GetRenderedGraphNodes (GlobalVariables.SelectedGraphID);
        foreach (DictionaryEntry d in g) {
            ((Vertice) d.Value).transform.position = new Vector3 (((Vertice) d.Value).transform.position.x, ((Vertice) d.Value).transform.position.y, 0.0f);
        }
    }

    public override void Execute () {
        Flat ();
    }
}