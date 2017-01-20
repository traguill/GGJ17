using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    [Header("Balance")]
    public float speed = 0.2f;
    public float max_dash_dst = 200;
    public LayerMask enemy_layer;
    public float detection_time = 0.5f;

    float horizontal = 0;
    float vertical = 0;
	
	// Update is called once per frame
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Movement();
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
                enemy.Seen();
            }
            else
            {
                Debug.Log("Enemy doesn't have a button_enemy script");
            }
        }
    
    }

    /*private void CheckEnemy(Enemy enemy)
    {
        if (selected_enemy == null)
        {
            //No enemy is selected now
            if (candidate_enemy == null)
            {
                //enemy is null and candidate too. add the enemy to a new candidate
                candidate_enemy = enemy;
                candidate_vision_time = 0.0f;
            }
            else
            {
                //there is no enemy selected and a candidate. Check if the candidate is the same as the enemy
                if (candidate_enemy == enemy)
                {
                    //The candidate is the same again. Add time
                    candidate_vision_time += Time.deltaTime;
                    if (candidate_vision_time >= detection_time)
                    {
                        selected_enemy = candidate_enemy;
                        candidate_enemy = null;
                        candidate_vision_time = 0.0f;
                    }
                }
                else
                {
                    //The candidate is a new candidate. Reset the time and add a new target
                    candidate_enemy = enemy;
                    candidate_vision_time = 0.0f;
                }
            }
        }
        else
        {
            //The player has pointed to a new target. Deselect the selected and add a new candidate
            if (selected_enemy != enemy)
            {
                selected_enemy.HideButtonAim();
                selected_enemy = null;
                candidate_enemy = enemy;
                candidate_vision_time = 0.0f;
            }

        }
    }*/

   void OnDrawGizmos()
   {
       Gizmos.color = Color.yellow;
       Vector3 dst = new Vector3(horizontal, vertical, 0);

       Gizmos.DrawLine(transform.position, transform.position + (dst.normalized * 3));
   }
}
