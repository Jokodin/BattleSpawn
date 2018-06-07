using ConstantData;

public class GoldNode : ResourceNode {

    new private void Awake()
    {
        base.Awake();
        resourceType = Enums.ResourceType.Gold;
    }

}
