using System.Collections;
using globals;

public class Nodedegree : AnalyticsScript {
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; }
    public override bool HasGuiButton { get; set; } = true;
    public override Hashtable Results { get; set; }
    public override bool HasResultButton { get; set; } = true;

    public override void Execute () {
       
        Results = new Hashtable ();
        Results.Clear();
        Nodes = (Hashtable) GlobalVariables.Graphnodes[1];
        Links = (Hashtable) GlobalVariables.Graphlinks[1];
        foreach (DictionaryEntry node in Nodes) {
            Results.Add (node.Key, ((Node) node.Value).Degree);
        }
    }
}