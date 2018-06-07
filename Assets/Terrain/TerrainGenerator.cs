using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    private Terrain terrain;
    public int width, length, mapRes;
    public float scale;

    private void Awake()
    {
        width = 5000;
        length = 8000;
        terrain = GetComponent<Terrain>();
        scale = 1;
        mapRes = 513;
        terrain.transform.position = new Vector3(-width / 2, 0, -length / 2);
    }

    void Start () {
        
    }

}
