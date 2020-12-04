using System.Collections.Generic;
using globals;
using UnityEngine;

public class Controller : MonoBehaviour {

    void Start ()
    {
        List<string> nodeStyles = GlobalVariables.GetFilesFrom("Nodeobjects", "prefab");
        GlobalVariables.NodeStyle = nodeStyles[0];
        List<string> linkStyles = GlobalVariables.GetFilesFrom("Linkobjects", "prefab");
        GlobalVariables.LinkStyle = linkStyles[0];
    }

}