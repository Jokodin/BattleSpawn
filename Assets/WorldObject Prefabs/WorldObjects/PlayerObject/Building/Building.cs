using UnityEngine;

public class Building : PlayerObject
{
    private int cost;
    public bool isGhost;
    public bool isColliding;

    new protected virtual void Start()
    {
        //Debug.Log("Building start");
        base.Start();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGhost && !collision.gameObject.name.Equals("Path") && !collision.gameObject.name.Equals("Ground"))
        {
            isColliding = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (isGhost && !collision.gameObject.name.Equals("Path") && !collision.gameObject.name.Equals("Ground"))
        {
            isColliding = false;
        }
    }
}
