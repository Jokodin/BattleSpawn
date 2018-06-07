using UnityEngine;
using ConstantData;

public abstract class ResourceNode : MonoBehaviour {

    public int maxResources;
    public int remainingResources;
    public int yieldAmount;
    public Enums.ResourceType resourceType;

    public virtual void Awake()
    {
        maxResources = 100;
        remainingResources = maxResources;
        yieldAmount = 1;
    }

    public virtual void Update()
    {
        if(remainingResources <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual int Mine()
    {
        if (remainingResources >= yieldAmount)
        {
            remainingResources -= yieldAmount;
            return yieldAmount;
        }
        else
        {
            int amount = remainingResources;
            remainingResources = 0;
            return amount;
        }
    }

}
