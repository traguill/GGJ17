using UnityEngine;
using System.Collections;

public class Seek : SteeringAbstract 
{
    SteeringBasics move;
    public Transform target;
    public float min_distance = 0.5f;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<SteeringBasics>();
    }

    void Update()
    {
        SeekPoint(target.position, weight);
    }

    public void SeekPoint(Vector3 pos, float weight_ext)
    {
        Vector3 position = new Vector3(pos.x, transform.position.y, pos.z);
        Vector3 distance = pos - transform.position;


        distance.Normalize();

        Vector3 acceleration = distance * move.max_acceleration;

        move.AccelerationMovement(acceleration, weight_ext);
    }
}
