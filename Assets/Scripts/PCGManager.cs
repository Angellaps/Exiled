using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGManager : MonoBehaviour
{ 
    [Serializable]
    public struct Node {
        public GameObject obj; //What node we're referring to. Copper/Silver/Oak Tree/Pine Tree etc
        [Range(0, 300)]
        public int amountToHave; //How many of these objects do we want spawned in our scene. Fixed amount.
        public int currentlyInScene; //How many are there in the scene already.
    }   

    public List<SpawnPoints> TreeSpawnPoints;
    public List<SpawnPoints> OreSpawnPoints;
    public List<Node> TreeNodes;
    public List<Node> OreNodes;

    private void Start() {
        CommenceScenePopulation();
    }

    //Called at world creation and at the start of each day.
    public bool CommenceScenePopulation() {
        //Add Tree Nodes to the Tree Spawn Points.
        Populate( TreeSpawnPoints,  TreeNodes);
        //Same for Ore Nodes.
        Populate( OreSpawnPoints,  OreNodes);
        return true;
    }

    public void Populate(List<SpawnPoints> spawns, List<Node> nodes) {     

        int[] spawnCounterPerNode = new int[nodes.Count];
        for (int i = 0; i < spawnCounterPerNode.Length; i++) {
            spawnCounterPerNode[i] = 0;
        }
        int nodeCounter = 0;
        System.Random rnd = new System.Random();
        
        foreach (Node node in nodes) {
            int spawnCounter = 0;            
            int amountToSpawn = node.amountToHave - node.currentlyInScene;
            for (int i = 0; i < amountToSpawn; i++) {
                bool spawnedNode = false;
                //Get random nodes till we find an empty one to spawn our node.
                while (spawnedNode == false) {
                    int r = rnd.Next(spawns.Count);

                    //         :::TO DO:::
                    //CHECK FOR SPIDER HIVE IN RANGE HERE
                    //IF TRUE: DO NOT INSTANTIATE NEW OBJ
                    //DO NOT currentlyInScene++
                    //BUT SET spawnedObject TRUE TO SKIP AND LOSE THIS NODE.
                    
                    //If spawn point is empty, instansiate the node and set the spawn point's occupied bool to true.
                    if (spawns[r].occupied == false) {
                        //Create new node at spawn point's position
                        Instantiate(node.obj, spawns[r].spawnPoint.transform.position, spawns[r].spawnPoint.transform.rotation);
                        spawnCounterPerNode[nodeCounter]++;
                        // Save how many new nodes were added in scene to update
                        // CurrentlyInScene info per node. Need to do it in a temp Array
                        // cause Structs are scuffed af
                        SpawnPoints v = spawns[r];
                        v.occupied = true;
                        spawns[r] = v;                        
                        spawnedNode = true;
                        spawnCounter++;
                    }
                }
            }
            Debug.Log("Spawned " + spawnCounter + " " + node.obj.ToString());            
            nodeCounter++;
        }
        
        //Update CurrentlyInScene info per Node
        for (int i = 0; i < spawnCounterPerNode.Length; i++) {
            Node temp = nodes[i];
            temp.currentlyInScene += spawnCounterPerNode[i];            
            nodes[i] = temp;            
        }
        
    }
}