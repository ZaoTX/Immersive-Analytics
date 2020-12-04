using System.Collections.Generic;
using UnityEngine;

class CSVPipe : Pipe {
    public override List<Node> Nodes { get; set; }
    public override List<Link> Links { get; set; }
    public override string FileExtension { get; set; } = "csv";
    public override string Description { get; set; } = "csv Parser fuer Adjazenzlisten";

    public override void NodesAndLinksToList (string filePath) {
        string parseMe = "Graphs/" + (filePath.Split ('.')) [0];
        TextAsset csv = (TextAsset) Resources.Load (parseMe);
        string[][] grid = CSV.Parse (csv.text);
        string[] row = new string[0];
        string source = "";
        int index;
        Nodes = new List<Node> ();
        Links = new List<Link> ();
        for (int i = 0; i < grid.Length; i++) {
            row = grid[i];
            source = row[0];
            index = Nodes.FindIndex (node => node.Id == source);
            if (!(index >= 0)) {
                Nodes.Add (new Node (source));
            }
            for (int j = 1; j < row.Length; j++) {
                if (row[j] != "") {
                    string target = row[j];
                    index = Nodes.FindIndex (node => node.Id == target);
                    if (!(index >= 0)) {
                        Nodes.Add (new Node (target));
                    }
                    Links.Add (new Link (new Node (source), new Node (target)));
                }
            }
        }
    }
}