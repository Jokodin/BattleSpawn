using System.Collections.Generic;
using UnityEngine;
using ConstantData;

public class GenerateLevel : MonoBehaviour
{

    public int mapSizeX;
    public int mapSizeY;
    public int offsetX, offsetY;
    private int numGoldNodes;
    private int distanceFromEdge;
    public List<GameObject> goldNodes;

    void Start()
    {
        mapSizeX = GameObject.Find("Ground").GetComponent<TerrainGenerator>().width;
        mapSizeY = GameObject.Find("Ground").GetComponent<TerrainGenerator>().length;
        offsetX = mapSizeX / 2;
        offsetY = mapSizeY / 2;
        distanceFromEdge = 300;
        numGoldNodes = 100;
        PlaceResourceNodes(Enums.ResourceType.Gold, numGoldNodes);
    }

    private void PlaceResourceNodes(Enums.ResourceType type, int numNodes)
    {
        int numNodesPlaced = 0;
        while(numNodesPlaced < numNodes)
        {
            GameObject nodeToInstantiate;
            switch (type.ToString())
            {
                case "Gold":
                    //Get random variation of gold node prefab
                    nodeToInstantiate = goldNodes[Random.Range(0, goldNodes.Count)];
                    break;
                default:
                    nodeToInstantiate = new GameObject();
                    break;
            }

            //Spawn node in random spot
            int randomX = Random.Range((-mapSizeX + distanceFromEdge) / 2, (mapSizeX - distanceFromEdge) / 2);
            int randomY = Random.Range((-mapSizeY + distanceFromEdge) / 2, (mapSizeY - distanceFromEdge) / 2);
            GameObject node = Instantiate(nodeToInstantiate, new Vector3(randomX, nodeToInstantiate.transform.position.y, randomY), nodeToInstantiate.transform.rotation);
            GetComponent<ManagerObject>().addResourceNode(node.GetComponent<ResourceNode>());

            //Calculate value based on location
            int locationValue = ((mapSizeX / 2 - Mathf.Abs(randomX) + Mathf.Abs(randomY)));

            //Alter resource node based on location value
            node.GetComponent<ResourceNode>().maxResources = locationValue;
            Vector3 newScale = node.transform.localScale;
            newScale *= locationValue/200;
            node.transform.localScale = newScale;
            numNodesPlaced++;
        }
    }
}
