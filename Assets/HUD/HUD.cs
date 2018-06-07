using System.Collections.Generic;
using UnityEngine;
using ConstantData;

public class HUD : MonoBehaviour {

    public GUISkin resourceSkin, ordersSkin;
    private const int ORDERS_BAR_WIDTH = 150, RESOURCE_BAR_HEIGHT = 40;
    private Player player;
    private const int SELECTION_NAME_HEIGHT = 30;
    private Dictionary<Enums.ResourceType, int> resourceValues, resourceLimits;
    private const int ICON_WIDTH = 32, ICON_HEIGHT = 32, TEXT_WIDTH = 128, TEXT_HEIGHT = 32;
    public Texture2D[] resourceTextures;
    private Dictionary<Enums.ResourceType, Texture2D> resourceImages;

    // Use this for initialization
    void Start () {
        resourceValues = new Dictionary<Enums.ResourceType, int>();
        resourceLimits = new Dictionary<Enums.ResourceType, int>();
        player = transform.root.GetComponent<Player>();

        resourceImages = new Dictionary<Enums.ResourceType, Texture2D>();
        for (int i = 0; i < resourceTextures.Length; i++)
        {
            switch (resourceTextures[i].name)
            {
                case "Gold":
                    resourceImages.Add(Enums.ResourceType.Gold, resourceTextures[i]);
                    resourceValues.Add(Enums.ResourceType.Gold, 0);
                    resourceLimits.Add(Enums.ResourceType.Gold, 0);
                    break;
                case "Power":
                    resourceImages.Add(Enums.ResourceType.Power, resourceTextures[i]);
                    resourceValues.Add(Enums.ResourceType.Power, 0);
                    resourceLimits.Add(Enums.ResourceType.Power, 0);
                    break;
                default: break;
            }
        }

    }
	
	// Update is called once per frame
	void OnGUI () {
        if (player && player.human)
        {
            DrawOrdersBar();
            DrawResourceBar();
        }
    }

    private void DrawOrdersBar()
    {
        GUI.skin = ordersSkin;
        GUI.BeginGroup(new Rect(Screen.width - ORDERS_BAR_WIDTH, RESOURCE_BAR_HEIGHT, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT), "");

        string selectionName = "";
        if (player.SelectedObject)
        {
            selectionName = player.SelectedObject.objectName;
        }
        if (!selectionName.Equals(""))
        {
            GUI.Label(new Rect(0, 10, ORDERS_BAR_WIDTH, SELECTION_NAME_HEIGHT), selectionName);
        }

        GUI.EndGroup();
    }

    private void DrawResourceBar()
    {
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT), "");

        int topPos = 4, iconLeft = 4, textLeft = 20;
        DrawResourceIcon(Enums.ResourceType.Gold, iconLeft, textLeft, topPos);
        iconLeft += TEXT_WIDTH;
        textLeft += TEXT_WIDTH;
        DrawResourceIcon(Enums.ResourceType.Power, iconLeft, textLeft, topPos);

        GUI.EndGroup();
    }

    private void DrawResourceIcon(Enums.ResourceType type, int iconLeft, int textLeft, int topPos)
    {
        Texture2D icon = resourceImages[type];
        string text = resourceValues[type].ToString() + "/" + resourceLimits[type].ToString();
        GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), icon);
        GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), text);
    }


    public bool MouseInBounds()
    {
        //Screen coordinates start in the lower-left corner of the screen
        //not the top-left of the screen like the drawing coordinates do
        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
        bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;
        return insideWidth && insideHeight;
    }

    public void SetResourceValues(Dictionary<Enums.ResourceType, int> resourceValues, Dictionary<Enums.ResourceType, int> resourceLimits)
    {
        this.resourceValues = resourceValues;
        this.resourceLimits = resourceLimits;
    }
}
