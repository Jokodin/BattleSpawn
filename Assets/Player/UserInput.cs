using UnityEngine;
using ConstantData;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {

    public Player player;
    private GameObject currentGhost;
    private Vector3 currentGhostPos;
    private bool placingBuilding;
    private Quaternion ghostRot;
    public TerrainCollider terrainCollider;

	void Start () {
        player = gameObject.GetComponent<Player>();
        terrainCollider = GameObject.Find("Ground").GetComponent<TerrainCollider>();
    }
	
	void Update () {

        if (player.human)
        {
            MoveCamera();
            RotateCamera();
        }

        if (placingBuilding)
        {
            MoveGhostWithMouse();
            CheckGhostValidPlacement();

            if (Input.GetMouseButtonDown(0) && !currentGhost.GetComponent<Building>().isColliding)
            {
                PlaceBuilding(currentGhost);
            }
        }
        else if (!placingBuilding)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                SpawnGhost(player.buildings[0]);
            }
        }

    }

    private void SpawnGhost(GameObject building)
    {
        //Vector3 ghostPosition = Camera.main.transform.position + Camera.main.transform.forward;
        //ghostPosition.y = building.transform.position.y;
        GameObject spawnedGhost= Instantiate(building, building.transform.position, Camera.main.transform.rotation);
        spawnedGhost.GetComponent<Building>().isGhost = true;
        currentGhost = spawnedGhost;
        placingBuilding = true;
    }

    private void PlaceBuilding(GameObject building)
    {
        ChangeBuildingColor(building, Color.white);
        building.GetComponent<Building>().isGhost = false;
        building.GetComponent<Building>().team = player.team;
        placingBuilding = false;
        currentGhost = null;
    }

    private void ChangeBuildingColor(GameObject building, Color color)
    {
        if(building.GetComponent<Renderer>()) building.GetComponent<Renderer>().material.color = color;
        foreach (Transform child in building.transform)
        {
            if(child.GetComponent<Renderer>()) child.GetComponent<Renderer>().material.color = color;
        }
    }

    private void MoveGhostWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (terrainCollider.Raycast(ray, out hitInfo, 1000))
        {
            currentGhostPos.x = hitInfo.point.x;
            currentGhostPos.y = currentGhost.transform.position.y;
            currentGhostPos.z = hitInfo.point.z;
            ghostRot = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        currentGhost.transform.position = currentGhostPos;
        currentGhost.transform.rotation = ghostRot;
    }

    private void CheckGhostValidPlacement()
    {
        if (currentGhost.GetComponent<Building>().isColliding)
        {
            ChangeBuildingColor(currentGhost, Color.red);
        }
        else
        {
            ChangeBuildingColor(currentGhost, Color.green);
        }
    }

    private void MoveCamera()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (Input.GetKey(KeyCode.A) || (xpos >= 0 && xpos < Constants.ScrollWidth))
        {
            movement.x -= Constants.ScrollSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || (xpos <= Screen.width && xpos > Screen.width - Constants.ScrollWidth))
        {
            movement.x += Constants.ScrollSpeed;
        }

        //vertical camera movement
        if (Input.GetKey(KeyCode.S) || (ypos >= 0 && ypos < Constants.ScrollWidth))
        {
            movement.z -= Constants.ScrollSpeed;
        }
        else if (Input.GetKey(KeyCode.W) || (ypos <= Screen.height && ypos > Screen.height - Constants.ScrollWidth))
        {
            movement.z += Constants.ScrollSpeed;
        }

        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement += Constants.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel") * Camera.main.transform.forward;

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > Constants.MaxCameraHeight)
        {
            destination.y = Constants.MaxCameraHeight;
        }
        else if (destination.y < Constants.MinCameraHeight)
        {
            destination.y = Constants.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (!Input.GetMouseButton(1) && destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * Constants.ScrollSpeed);
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if (Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * Constants.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * Constants.RotateAmount;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * Constants.RotateSpeed);
        }
    }
}
