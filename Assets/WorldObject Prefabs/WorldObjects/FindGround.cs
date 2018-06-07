using UnityEngine;

public class FindGround : MonoBehaviour {
	
	void FixedUpdate () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(transform.up), out hit))
        {
            if (hit.transform.gameObject.name.Equals("Ground"))
            {
                if (hit.distance > 0)
                {
                    
                    transform.position += transform.TransformDirection(-transform.up);
                }
            }
        }
    }
}
