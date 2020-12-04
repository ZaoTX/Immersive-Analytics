using System.Collections;
using globals;
using UnityEngine;

public class ShowRandomMatching : VisualScript {
    public override bool HasGuiButton { get; set; } = true;
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; } = (Hashtable) GlobalVariables.Graphlinks[1];
    public override Hashtable RenderedNodes { get; set; }
    public override Hashtable RenderedLinks { get; set; } = GlobalVariables.GetRenderedGraphLinks (1);

    public override void Execute () {
        RandomMatching RM = new RandomMatching ();
        ArrayList rmLinks = RM.FindMatching (Links);
        foreach (Link link in rmLinks)
        {
            ((Edge)RenderedLinks[link.Id]).Color = Color.green;
        }
    }
}