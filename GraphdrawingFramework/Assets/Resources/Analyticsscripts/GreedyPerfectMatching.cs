using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using globals;

public class GreedyPerfectMatching : AnalyticsScript {
    public override bool HasGuiButton { get; set; }
    public override bool HasResultButton { get; set; } = true;
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; }
    public override Hashtable Results { get; set; } = new Hashtable();

    public override void Execute () {
        Nodes = (Hashtable) GlobalVariables.Graphnodes[1];
        Links = (Hashtable) GlobalVariables.Graphlinks[1];
        Results.Add (0, CalcPerfectMatching (Links));
        
    }

    public ArrayList CalcPerfectMatching (Hashtable links) {
        foreach (DictionaryEntry link in links) {
            Link startingEdge = (Link) link.Value;
            List<ArrayList> matching = CalcMatching (startingEdge, links);
            if (IsPerfectMatching (matching[0])) return matching[1];
        }
        return new ArrayList ();
    }

    private List<ArrayList> CalcMatching (Link startingEdge, Hashtable links) {
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
        return new List<ArrayList> { matchingNodes, matchingEdges };
    }

    private bool IsPerfectMatching (ArrayList linklist) {
        foreach (DictionaryEntry nodeEntry in Nodes) {
            Node node = (Node) nodeEntry.Value;
            if (!linklist.Contains (node) || !linklist.Contains (node)) return false;
        }
        return true;
    }
}