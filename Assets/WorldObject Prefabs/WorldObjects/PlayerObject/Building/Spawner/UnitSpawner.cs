using UnityEngine;
using ConstantData;
using System.Collections.Generic;

public class UnitSpawner : Building, ISpawnBehavior {

    public GameObject unitToSpawn;
    public List<Transform> spawnLocations;
    private float spawnCooldown;
    private float spawnTimer;

    new void Start()
    {
        base.Start();

        spawnLocations = new List<Transform>();
        foreach (Transform t in transform)
        {
            if (t.gameObject.name.Equals("SpawnLocation"))
            {
                spawnLocations.Add(t);
            }
        }

        spawnCooldown = 5;
        spawnTimer = spawnCooldown;
    }

    void Update()
    {
        if (!isGhost)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                foreach (Transform spawnLocation in spawnLocations)
                {
                    Spawn(unitToSpawn, spawnLocation.position);
                }
                spawnTimer = spawnCooldown;
            }
        }

    }

	public void Spawn(GameObject unit, Vector3 location)
    {
        if (unitToSpawn)
        {
            GameObject spawnedUnit = Instantiate(unit, location, unitToSpawn.transform.rotation);
            spawnedUnit.GetComponent<AttackingUnit>().team = this.team;

            //Color the unit for its team
            Material mat = spawnedUnit.transform.GetChild(1).GetComponent<Renderer>().material;
            if (team.Equals(Constants.TEAM1)) mat.color = Color.red;
            else mat.color = Color.blue;

            //Hand unit to manager
            manager.GetComponent<ManagerObject>().assignToTeam(spawnedUnit.GetComponent<PlayerObject>());
        }
    }

    
}
