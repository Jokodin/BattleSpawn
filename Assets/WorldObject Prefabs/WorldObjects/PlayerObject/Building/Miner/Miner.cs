using System.Collections.Generic;
using UnityEngine;

public class Miner : Building {

    public float workerMineSpeed;
    public int maxGoldStorage, currentGoldStorage, numWorkers;
    public GameObject nodeToMine, workerPrefab;
    public List<GameObject> workers;
    private Transform spawnLocation;

	new void Start () {
        base.Start();
        workerMineSpeed = 1;
        maxGoldStorage = 100;
        currentGoldStorage = 0;
        numWorkers = 1;
        nodeToMine = null;
        spawnLocation = transform.Find("spawnLocation");
    }
	
	void Update () {
        if (!isGhost)
        {
            if (!nodeToMine)
            {
                nodeToMine = findNodeToMine();
            }

            if (nodeToMine)
            {
                if (workers.Count < numWorkers)
                {
                    GameObject worker = Instantiate(workerPrefab, spawnLocation.position, workerPrefab.transform.rotation);
                    worker.GetComponent<Worker>().parentBuilding = this;
                    worker.GetComponent<Worker>().team = this.team;
                    manager.assignToTeam(worker.GetComponent<Worker>());
                    workers.Add(worker);
                }
            }
        }
	}

    private GameObject findNodeToMine()
    {
        float closestDistance = 9999;
        GameObject closestNode = null;
        List<GoldNode> goldNodes = manager.getGoldNodes();
        foreach(GoldNode node in goldNodes)
        {
            float currentDistance = Vector3.Distance(transform.position, node.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestNode = node.gameObject;
            }
        }
        return closestNode;
    }
}
