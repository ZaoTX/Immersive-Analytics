using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using globals;

public class RandomMatching : AnalyticsScript {
    public override bool HasGuiButton { get; set; }
    public override bool HasResultButton { get; set; } = true;
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; }
    public override Hashtable Results { get; set; } = new Hashtable ();

    public override void Execute () {
        Nodes = (Hashtable) GlobalVariables.Graphnodes[1];
        Links = (Hashtable) GlobalVariables.Graphlinks[1];
        Results.Add (0, FindMatching (Links));
    }

    public ArrayList FindMatching (Hashtable links) {
        List<string> keys = links.Keys.Cast<string> ().ToList ();;
        int size = links.Count;
        Random rand = new Random ();
        string randomKey = keys[rand.Next (size)];
        Link startingEdge = (Link) links[randomKey];
        ArrayList matchingNodes = new ArrayList ();
        ArrayList matchingEdges = new ArrayList ();
        matchingNodes.Add (startingEdge.SourceNode);
        matchingNodes.Add (startingEdge.TargetNode);
        matchingEdges.Add (startingEdge);
        foreach (DictionaryEntry link in links) {
            Link lnk = (Link) link.Value;
            if (!matchingNodes.Contains (lnk.SourceNode) && !matchingNodes.Contains (lnk.TargetNode)) {
                matchingNodes.Add (lnk.SourceNode);
                matchingNodes.Add (lnk.TargetNode);
                matchingEdges.Add (lnk);
            }
        }
        return matchingEdges;
    }
}