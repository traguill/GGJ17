using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    [Header("Balance")]
    public float speed = 0.2f;
    public float max_dash_dst = 200;
    public LayerMask enemy_layer;

    float horizontal = 0;
    float vertical = 0;

	
	// Update is called once per frame
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Movement();

       
	
	}

    void FixedUpdate()
    {
        Aiming();
    }

    private void Movement()
    {
        Vector3 step = Vector3.zero;

        
        if (horizontal != 0)
        {
            step.x += horizontal * speed * Time.deltaTime;
        }

        if (vertical != 0)
        {
            step.y += vertical * speed * Time.deltaTime;
        }

        transform.position += step;
    }

    private void Aiming()
    {
        if (horizontal == 0 && vertical == 0)
            return;

        Vector3 aim_dir = new Vector3(horizontal, vertical, 0);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, aim_dir.normalized, out hit, max_dash_dst, enemy_layer))
        {
            Enemy enemy = hit.transform.gameObject.GetComponentInParent<Enemy>();
            if(enemy)
            {
                enemy.ShowButtonAim();
            }
            else
            {
                Debug.Log("Enemy doesn't have a button_enemy script");
            }

        }
    
    }
}
