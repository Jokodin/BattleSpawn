using System.Collections.Generic;
using UnityEngine;
using ConstantData;

public class ManagerObject : MonoBehaviour{

    public List<PlayerObject> team1Entities = new List<PlayerObject>();
    public List<PlayerObject> team2Entities = new List<PlayerObject>();
    public List<GoldNode> goldNodes = new List<GoldNode>();
    public GameObject playerPrefab;
    public int mapSizeX { get; set; }
    public int mapSizeY { get; set; }

    void Start()
    {
        Player player = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation).GetComponent<Player>();
        player.team = Constants.TEAM1;
        mapSizeX = GameObject.Find("Ground").GetComponent<TerrainGenerator>().width;
        mapSizeY = GameObject.Find("Ground").GetComponent<TerrainGenerator>().length;
    }

    public List<PlayerObject> getMyEnemies(string myTeam)
    {
        if (myTeam.Equals(Constants.TEAM1))
        {
            return team2Entities;
        }
        else if (myTeam.Equals(Constants.TEAM2))
        {
            return team1Entities;
        }

        return null;
    }

    public void assignToTeam(PlayerObject playerObject)
    {
        if (playerObject.team.Equals(Constants.TEAM1))
        {
            addTeam1(playerObject);
        }
        else if (playerObject.team.Equals(Constants.TEAM2))
        {
            addTeam2(playerObject);
        }
    }

    public void removeFromTeam(PlayerObject playerObject)
    {
        if (playerObject.team.Equals(Constants.TEAM1))
        {
            removeTeam1(playerObject);
        }
        else if (playerObject.team.Equals(Constants.TEAM2))
        {
            removeTeam2(playerObject);
        }
    }

    private void addTeam1(PlayerObject gameObject)
    {
        team1Entities.Add(gameObject);
    }

    private void removeTeam1(PlayerObject gameObject)
    {
        team1Entities.Remove(gameObject);
    }

    private void addTeam2(PlayerObject gameObject)
    {
        team2Entities.Add(gameObject);
    }

    private void removeTeam2(PlayerObject gameObject)
    {
        team2Entities.Remove(gameObject);
    }

    public void addResourceNode(ResourceNode node)
    {
        if (node.GetComponent<GoldNode>())
        {
            goldNodes.Add((GoldNode) node);
        }
    }

    public List<GoldNode> getGoldNodes()
    {
        return goldNodes;
    }


}
