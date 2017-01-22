using UnityEngine;
using System.Collections;

public class EntityReposition : MonoBehaviour 
{

	void LateUpdate()
    {
        float z = transform.position.y;
        z = z / 10000.0f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
