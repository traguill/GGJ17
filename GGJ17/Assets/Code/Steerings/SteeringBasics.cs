using UnityEngine;
using System.Collections;

public class SteeringBasics : MonoBehaviour {

    public float max_velocity = 5.0f;
    public float max_acceleration = 1.0f;

    public Vector3 movement = Vector3.zero;

    public float character_radius = 0.8f;

    [HideInInspector]
    public bool stop = false;

    public void SetMovementVelocity(Vector3 vel)
    {
        movement = vel;
    }

    public void AccelerationMovement(Vector3 accel, float weight)
    {
        movement += accel * (weight / 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
            return;

        //Cap the velocity
        if (movement.magnitude > max_velocity)
        {
            movement.Normalize();
            movement *= max_velocity;
        }


        //Make the final move
        transform.position += movement * Time.deltaTime;
    }
}
