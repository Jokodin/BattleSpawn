using System.Collections.Generic;
using UnityEngine;
using ConstantData;

public class Player : MonoBehaviour {

    public WorldObject SelectedObject { get; set; }
    public string username;
    public bool human;
    public HUD hud;
    public string team;
    public List<GameObject> buildings;
    private int startingGoldLimit, startingPowerLimit, startingGold, startingPower;
    private Dictionary<Enums.ResourceType, int> resources, resourceLimits;
    public float mineRange;
    
    void Awake()
    {
        resources = InitResourceList();
        resourceLimits = InitResourceList();
        startingGold = 100;
        startingPower = 0;
        startingGoldLimit = 1000;
        startingPowerLimit = 0;
        mineRange = 5;
    }

    void Start () {
        hud = GetComponentInChildren<HUD>();
        AddStartResourceLimits();
        AddStartResources();
    }
	
	void Update () {
        if (human)
        {
            hud.SetResourceValues(resources, resourceLimits);
        }
    }

    private Dictionary<Enums.ResourceType, int> InitResourceList()
    {
        Dictionary<Enums.ResourceType, int> list = new Dictionary<Enums.ResourceType, int>();
        list.Add(Enums.ResourceType.Gold, 0);
        list.Add(Enums.ResourceType.Power, 0);
        return list;
    }

    private void AddStartResourceLimits()
    {
        IncrementResourceLimit(Enums.ResourceType.Gold, startingGoldLimit);
        IncrementResourceLimit(Enums.ResourceType.Power, startingPowerLimit);
    }

    private void AddStartResources()
    {
        AddResource(Enums.ResourceType.Gold, startingGold);
        AddResource(Enums.ResourceType.Power, startingPower);
    }

    public void AddResource(Enums.ResourceType type, int amount)
    {
        resources[type] += amount;
    }

    public void IncrementResourceLimit(Enums.ResourceType type, int amount)
    {
        resourceLimits[type] += amount;
    }
}
