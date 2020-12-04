using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using globals;
using UnityEngine;

/**
 * Modified Nibble
 *  https://www.cse.iitb.ac.in/~sudarsha/Pubs-dir/comad09-clustering.pdf 
 *  Ziyao He try to implement that
 */
public class NibbleClustering : AnalyticsScript
{
    public override Hashtable Nodes { get; set; }
    public override Hashtable Links { get; set; }
    public override bool HasGuiButton { get; set; } = true;
    public override Hashtable Results { get; set; } 
    public override bool HasResultButton { get; set; } = true;
    
    //p1
    DictionaryEntry randNodeEntry;
    Node randNode;//get random Node as a start node
    int totalSteps;
    double a = 1;
    double d = 5;
    double r = 1;
    double f = 1;
    double maxActiveNodeBound;
    int maxClusterSize=10;//p5 User can define the value this time we set up as 3
    int batch = 10;//Batch i
    double spreadProbability = 0.5;
    List<Node> ActiveNodes= new List<Node>();
    
    List<Link> linkList = new List<Link>();
     
    List<List<Node>> bestClusters = new List<List<Node>>();

    void initialization(Hashtable Nodes,Hashtable Links)
    {

        /*foreach (DictionaryEntry entry in Nodes) {
            Debug.Log("键值为： " + entry.Key + "内容是：" + entry.Value);
        }*/
        totalSteps = 0;//set total steps to 0
        int size = Nodes.Count;
        
        int item = new System.Random().Next(size);
        int i = 0;
        //find a random start node
        foreach (DictionaryEntry node in Nodes)
        {
            if (i == item)
            {
                randNodeEntry = node;
                randNode = (Node)node.Value;
                randNode.p = 1;
                /*Debug.Log(node.Key); check
                Debug.Log(randNode.Id);*/
                
                ActiveNodes.Add(randNode);
            }
            i++;
            //allNodes.Add((Node)node.Value);
        }
        foreach (DictionaryEntry link in Links) {
            linkList.Add((Link)link.Value);
        }
        maxActiveNodeBound = f * maxClusterSize;
        
        //Debug.Log("hellow");
    }

    public List<List<Node>> getClustering(Hashtable Nodes,Hashtable Links) {
        List<List<Node>> nibbleBestClusters = new List<List<Node>>();
        Hashtable NodesCopy = (Hashtable)Nodes.Clone();
        Hashtable LinksCopy = (Hashtable)Links.Clone(); 
        while (NodesCopy.Count>0) {
            Debug.Log("New number of Nodes are: " + NodesCopy.Values.Count);
            List<Node> bestCluster=new List<Node>();
            List<Link> clusterLinks = new List<Link>();
            
            if (NodesCopy.Count <= maxClusterSize)
            {
                foreach (DictionaryEntry entry in NodesCopy)
                {
                    bestCluster.Add((Node)entry.Value);
                }
                NodesCopy.Clear();
            }
            else {
                bestCluster = modifiedNibble(NodesCopy, LinksCopy);

                nibbleBestClusters.Add(bestCluster);
                //delete the Nodes in the bestcluster and their edges from the whole graph
                foreach (Node node in bestCluster)
                {
                    if (NodesCopy.Contains(node.Id))
                    {
                        NodesCopy.Remove((String)node.Id);
                        Debug.Log("The size of the allnode is: " + NodesCopy.Keys.Count);
                        Debug.Log("Node is removed from Hashtable NodeCopy");
                    }

                }
                //get the link of the bestcluster
                foreach (Link link in LinksCopy.Values)
                {
                    if (bestCluster.Contains(link.SourceNode)&&bestCluster.Contains(link.TargetNode))
                    {
                        clusterLinks.Add(link);
                        
                    }

                }
                //delete the link in hashtable
                foreach (Link link in clusterLinks) {
                    LinksCopy.Remove((String)link.Id);
                }
            }
           


        }
        return nibbleBestClusters;
    
    }
    public override void Execute()
    {
        Results = new Hashtable();
        Nodes = (Hashtable)GlobalVariables.Graphnodes[1];
        Links = (Hashtable)GlobalVariables.Graphlinks[1];
        Hashtable NodesCopy = (Hashtable)Nodes.Clone();
        Hashtable LinksCopy = (Hashtable)Links.Clone();

        bestClusters = getClustering(NodesCopy, LinksCopy);
        Results.Clear();
        
        int Clustercount = 1;
        foreach (List<Node> cluster in bestClusters) {
            
            foreach (Node node in cluster) {
                if (!Results.Contains(node.Id)) {
                    Results.Add(node.Id, "Cluster " + Clustercount);
                }
               
            }
            Clustercount++;
        }

    }
    public List<Node> modifiedNibble(Hashtable Nodes,Hashtable Links) {
        List<Node> allNodes = new List<Node>();
        allNodes = Nodes.Values.Cast<Node>().ToList();
        int batchSteps;
        int ti;
        List<Node> bestCluster = new List<Node>();
        initialization(Nodes, Links);
        List<List<Node>> C_best_Candidates = new List<List<Node>>();
        for (int i = 1; i <= batch; i++)
        {
            //Debug.Log("was there(for batch)");
            //Batch i initialization
            ti = (int)System.Math.Floor(a * System.Math.Pow(r, batch) + a + batch * d);
            if (ti > maxClusterSize)
            {
                batchSteps = (int)maxClusterSize - totalSteps;
            }
            else
            {
                batchSteps = ti - totalSteps;
            }
            //Do for batchstep number of times:
            for (int j = 0; j < batchSteps; j++)
            {
                ActiveNodes = spreading(ActiveNodes);

            }
            /*foreach (Node node in ActiveNodes)
            {
                Debug.Log("ActiveNode " + i + " " + node.Id.ToString());
            }*/
            //(3) Obtain Cluster Ci= Modified FindBestCLuster
            //Debug.Log("hellow");
            List<Node> Ci = MoifiedFindBestCluster(ActiveNodes, maxClusterSize,allNodes);
            C_best_Candidates.Add(Ci);
            //Debug.Log("was there Ci");
            //(4)
            if (i >= 1)
            {
                List<Node> Ci_1 = C_best_Candidates[i - 1];
                double ci_conductance = CalculateConductance(Ci,allNodes);
                double ci_1_conductance = CalculateConductance(Ci_1,allNodes);
                if (ci_1_conductance <= ci_conductance)
                {
                    bestCluster = Ci;
                    //Debug.Log("Found");
                    //to Step(6)
                    break;
                }
            }
            //(5)
            if (ti >= maxClusterSize)
            {
                //Debug.Log("exceeded");
                break;
            }
            else
            {
                totalSteps = (int)Math.Floor((double)ti);
            }

        }
        //(6) add Abandoned Nodes
        List<Node> abandoned = new List<Node>();
        List<Node> S_hat = allNodes.Where(p => bestCluster.All(p2 => p2.Id != p.Id)).ToList<Node>();
        foreach (Node node in S_hat)
        {
            bool needAdd = true;
            foreach (Node neighbour in node.Neighbours)
            {
                if (!bestCluster.Contains(neighbour))
                {
                    needAdd = false;
                    break;
                }
            }
            if (needAdd)
            {
                abandoned.Add(node);
            }
        }
        foreach (Node node in abandoned)
        {
            bestCluster.Add(node);
        }

        bestCluster = bestCluster.Distinct().ToList<Node>();
        if (bestCluster.Count == 1) {
            Node onlyNode = bestCluster[0];
            Hashtable NodesCopy = (Hashtable)Nodes.Clone();
            NodesCopy.Remove((String)onlyNode.Id);
            bestCluster = modifiedNibble(NodesCopy, Links);
        }
        foreach (Node node in bestCluster)
        {
            
            Debug.Log(node.Id);
        }
        return bestCluster;
    }
    public List<Node> MoifiedFindBestCluster(List<Node> activeNodes, int maxClusterSize,List<Node> allNodes)
    {
        List<Node> Ci = new List<Node>();
        //Normalization
        foreach (Node activeNode in activeNodes) {
            activeNode.p = activeNode.p / activeNode.Neighbours.Count;
        }
        //Sort the ActiveNodes 
        List<Node> SortedActiveNodes = activeNodes.OrderByDescending(o => o.p).ToList();
        int j = Math.Min(maxClusterSize,Math.Abs(activeNodes.Count));
        //define Candidates
        List<List<Node>> Candidates = new List<List<Node>>();
        for (int i = 1; i <= j; i++) {
            
            List<Node> Cj = new List<Node>();
            for (int k = 0; k < i;k++) {
                Node curNode = SortedActiveNodes[k];
                Cj.Add(curNode);
            }
            Candidates.Add(Cj);
            
        }
        double minConductance =100;
        int CandidateIndex = 0;
        int indexcounter = 0;
        foreach (List<Node> Cj in Candidates) {
            double curConductance = CalculateConductance(Cj,allNodes);
            if (minConductance>curConductance) {

                minConductance = curConductance;
                CandidateIndex = indexcounter;
            }
            indexcounter++;
        }
        Ci = Candidates[CandidateIndex];
        return Ci;
    }
    //Calculate conductance of a cluster
    public double CalculateConductance(List<Node> cluster,List<Node> allNodes) {
        List<Node> S_hat = allNodes.Where(p => cluster.All(p2 => p2.Id != p.Id)).ToList<Node>();
        int vol_s_hat = 0;
        foreach (Node node in S_hat)
        {
            int degree = 0;
            //recalculate node Degree:
            foreach (Node neighborNode in node.Neighbours) {
                if (allNodes.Contains(neighborNode)) {
                    degree++;
                }
            }
            vol_s_hat += degree;
        }
        int vol_s = 0;
        foreach (Node node in cluster)
        {
            int degree = 0;
            //recalculate node Degree:
            foreach (Node neighborNode in node.Neighbours)
            {
                if (allNodes.Contains(neighborNode))
                {
                    degree++;
                }
            }
            vol_s += degree;
        }
        int deltaS = 0;
        foreach (Link link in linkList)
        {
            if (cluster.Contains(link.SourceNode) && S_hat.Contains(link.TargetNode) || cluster.Contains(link.TargetNode) && S_hat.Contains(link.SourceNode))
            {
                deltaS++;

            }
        }
        double Conductance;
        if (Math.Min(vol_s, vol_s_hat) == 0)
        {
             Conductance= deltaS/2;
        }
        else
        {
             Conductance = deltaS / Math.Min(vol_s, vol_s_hat);
        }

        return Conductance;
    }
    /**
     * Step 2 a-e
     */
    public List<Node> spreading(List<Node> ActiveNodes) {
        bool p7NeedChange = false;
        // Here we do probability spreading
        foreach (Node node in ActiveNodes.ToList<Node>())
        {
            List<Node> Neighbours = node.Neighbours;
            int count = Neighbours.Count;
            double spreadp = node.p * spreadProbability / (count);
            //new possibility of current node
            node.p -= count * spreadp;
            foreach (Node neighbour in Neighbours)
            {
                if (!p7NeedChange)
                {
                    //spread
                    neighbour.p += spreadp;
                    if (!ActiveNodes.Contains(neighbour)) {
                        //Update activeNodes
                        ActiveNodes.Add(neighbour);
                    }
                   
                }
                else
                {
                    return ActiveNodes;
                }

            }
        }
        if (maxActiveNodeBound <= ActiveNodes.Count)
        {
            p7NeedChange = true;
        }
        return ActiveNodes;
    }
     
}
