using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit {

    public Building parentBuilding;
    public GameObject nodeToMine;
    public bool isMining, isReturning;
    public float miningTime, mineTimer;
    public List<Vector3> points = new List<Vector3>();
    public int currentDestinationIndex;

    new void Awake()
    {
        moveSpeed = 5;
        currentDestinationIndex = 0;
    }

    new void Start () {
        base.Start();
        //Debug.Log("Worker unit starting");
        miningTime = 5;
        mineTimer = miningTime;
        isMining = false;
        isReturning = false;
    }
	
	void Update () {
        if (parentBuilding)
        {
            if (parentBuilding.GetComponent<Miner>())
            {
                nodeToMine = parentBuilding.GetComponent<Miner>().nodeToMine;
                if (!points.Contains(nodeToMine.transform.position))
                {
                    points.Add(nodeToMine.transform.position);
                    agent.SetDestination(points[0]);
                }
                if (!points.Contains(parentBuilding.transform.position)) points.Add(parentBuilding.transform.position);
            }
        }


        Debug.Log(agent.destination);

        if (!agent.pathPending && agent.remainingDistance < 3)
        {
            
            GoToNextPoint();
        }

    }

    private void GoToNextPoint()
    {
        if (points.Count == 0) return;

        currentDestinationIndex = (currentDestinationIndex + 1) % points.Count;
        agent.SetDestination(points[currentDestinationIndex]);
    }
}
