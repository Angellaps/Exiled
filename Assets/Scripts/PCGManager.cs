using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGManager : MonoBehaviour
{
    [Serializable]
    public struct Node {
        public GameObject obj; //What node we're referring to. Copper/Silver/Oak Tree/Pine Tree etc
        [Range(0, 255)]
        public int amountToHave; //How many of these objects do we want spawned in our scene. Fixed amount.
        public int currentlyInScene; //How many are there in the scene already.
    }

    [Serializable]
    public struct SpawnPoint {
        public GameObject spawnPoint; //Empty game objects in scene where stuff will spawn
        public bool occupied;        //Bool for if something is already spawned there.
    }    

    public List<SpawnPoint> TreeSpawnPoints;
    public List<SpawnPoint> OreSpawnPoints;
    public List<Node> TreeNodes;
    public List<Node> OreNodes;

    //Called at world creation and at the start of each day.
    public bool CommenceScenePopulation() {
        //Add Tree Nodes to the Tree Spawn Points.
        Populate(ref TreeSpawnPoints, ref TreeNodes);
        //Same for Ore Nodes.
        Populate(ref OreSpawnPoints, ref OreNodes);
        return true;
    }

    public void Populate(ref List<SpawnPoint> spawns, ref List<Node> nodes) {
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
                        Instantiate(node.obj, spawns[r].spawnPoint.transform);
                        spawnCounter++;

                        //WHY IS THIS A THING??????
                        //EXPLAIN WHY spawns[r].occupied = true DOESN'T WORK
                        SpawnPoint v = spawns[r];
                        v.occupied = true;
                        spawns[r] = v;
                        spawnCounter++;
                        spawnedNode = true;
                    }
                }
            }

            //?????????????????????????????????????????
            //TO MIALO MOU EXEI GINEI POURES. SIGOURA IPARXEI KALITEROS TROPOS.
            //Mou petouse error giati to node einai 'for each variable'
            Node temp = node;
            temp.currentlyInScene += spawnCounter;
            int index = nodes.IndexOf(node);
            nodes[index] = temp;        
        }
        //return;
    }
}