using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {
    public enum SpawnType {
        Tree,
        Ore,
        EnemySpawner
    };
    public SpawnType type;
    public bool occupied;
    public GameObject spawnPoint;
    public GameObject PCGManager;

    void Awake() {
        occupied = false;
        spawnPoint = this.gameObject;
        switch (type) {
            case SpawnType.Tree:
                PCGManager.GetComponent<PCGManager>().TreeSpawnPoints.Add(this);
                break;
            case SpawnType.Ore:
                PCGManager.GetComponent<PCGManager>().OreSpawnPoints.Add(this);
                break;
            //case SpawnType.EnemySpawner:
            //    PCGManager.GetComponent<PCGManager>().TreeSpawnPoints.Add(this);
            //    break;
        }
        
    }
}
