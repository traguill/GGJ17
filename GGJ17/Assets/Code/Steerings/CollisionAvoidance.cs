using UnityEngine;
using System.Collections;

public class CollisionAvoidance : SteeringAbstract {

    SteeringBasics basics;
    public float detection_radius = 5.0f;
    public LayerMask detection_mask;

    Vector3 accel;

    // Use this for initialization
    void Start()
    {
        basics = GetComponent<SteeringBasics>();

    }

    void Update()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, detection_radius, detection_mask);
        accel = Steer(col);
    }

    private Vector3 Steer(Collider[] targets)
    {

        Vector3 acceleration = Vector3.zero;

        // Find the target that is the first possibility to collide 

        //First collision time
        float shortest_time = float.PositiveInfinity;

        // First target that we will collide and other data that we will need

        Collider first_target = null;
        float first_min_separation = 0, first_distance = 0, first_radius = 0;
        Vector3 first_relative_pos = Vector3.zero, first_relative_vel = Vector3.zero;

        foreach (Collider target in targets)
        {
            SteeringBasics basic_target = target.GetComponentInParent<SteeringBasics>();

            if (basic_target == null)
            {
                Debug.Log("ColAvoidance: target doesn't have steering basics");
            }

            //Calculate the time to collision

            Vector3 relative_pos = transform.position - target.transform.position;
            Vector3 relative_vel = basics.movement - basic_target.movement;
            float distance = relative_pos.magnitude;
            float relative_speed = relative_vel.magnitude;

            if (relative_speed == 0)
            {
                continue;
            }

            float time_to_collision = -1 * Vector3.Dot(relative_pos, relative_vel) / (relative_speed * relative_speed);

            //Check if we will collide 
            Vector3 separation = relative_pos + relative_vel * time_to_collision;
            float min_separation = separation.magnitude;

            if (min_separation > basics.character_radius + basic_target.character_radius)
            {
                continue;
            }

            // Check if its the shortest

            if (time_to_collision > 0 && time_to_collision < shortest_time)
            {
                shortest_time = time_to_collision;
                first_target = target;
                first_min_separation = min_separation;
                first_distance = distance;
                first_relative_pos = relative_pos;
                first_relative_vel = relative_vel;
                first_radius = basic_target.character_radius;
            }

        }

        // calculate steering

        // if we have no target then return

        if (first_target == null)
        {
            return acceleration;
        }

        //If we are going to collide with no separation or if we are already colliding then steer based on current position, else calculate the future relative position 
        if (first_min_separation <= 0 || first_distance < basics.character_radius + first_radius)
        {

            acceleration = transform.position - first_target.transform.position;

        }
        else
        {
            acceleration = first_relative_pos + first_relative_vel * shortest_time;


        }


        //avoid the target 
        acceleration.Normalize();
        acceleration *= basics.max_acceleration;

        if (acceleration.magnitude < 0.005f)
        {
            return acceleration;
        }
        acceleration.y = 0;
        basics.AccelerationMovement(acceleration, weight);

        return acceleration;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 start_pos = new Vector3(transform.position.x, 4, transform.position.z);
        Gizmos.DrawLine(start_pos, start_pos + accel);
    }

}
