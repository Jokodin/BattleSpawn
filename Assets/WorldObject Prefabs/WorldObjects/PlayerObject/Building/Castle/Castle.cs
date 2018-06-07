using System.Collections.Generic;
using UnityEngine;
using ConstantData;

public class Castle : Building, IKillable{

    public GameObject unitToSpawn;
    private float spawnCooldown;
    private float spawnTimer;
    private List<Transform> spawnLocations;
    private Transform spawnLocation;
    public int numUnitsToSpawn;

    // Use this for initialization
    new void Start()
    {
        //Debug.Log("Castle start");
        base.Start();

        objectName = "Castle";
        //PlaceCastle();
        spawnLocations = new List<Transform>();
        foreach(Transform spawnLocation in transform)
        {
            spawnLocations.Add(spawnLocation);
        }

        numUnitsToSpawn = 5;

        maxHealth = 1000;
        currentHealth = maxHealth;

        spawnCooldown = 5F;
        spawnTimer = 1;

        manager.assignToTeam(GetComponent<PlayerObject>());
    }

    void Update () {

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            for(int i = 0; i < numUnitsToSpawn; i++)
            {
                spawnLocation = spawnLocations[i];
                Vector3 spawnVector = spawnLocation.position;
                spawnVector.y = 0;
                GameObject spawnedUnit = Instantiate(unitToSpawn, spawnVector, spawnLocation.rotation);
                spawnedUnit.GetComponent<AttackingUnit>().team = this.team;
                
                Material mat = spawnedUnit.transform.GetChild(1).GetComponent<Renderer>().material;
                if (team.Equals(Constants.TEAM1)) mat.color = Color.red;
                else mat.color = Color.blue;

                manager.assignToTeam(spawnedUnit.GetComponent<PlayerObject>());
            }
            spawnTimer = spawnCooldown;
        }
    }

    private void PlaceCastle()
    {
        float mapSizeX = manager.GetComponent<ManagerObject>().mapSizeX;
        float mapSizeZ = manager.GetComponent<ManagerObject>().mapSizeY;

        if (team.Equals(Constants.TEAM1))
        {
            transform.position = new Vector3(mapSizeX/2, 5, mapSizeZ);
        }
        else if (team.Equals(Constants.TEAM2))
        {
            transform.position = new Vector3(-mapSizeX/2, 5, mapSizeZ);
        }
    }
}
