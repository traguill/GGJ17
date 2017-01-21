using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    [Header("Balance")]
    [Header("Movement")]
    public float max_speed = 10.0f;
    public float acceleration = 1.0f;
    [Range(0.0f, 1.0f)]
    public float reactivityPercent = 0.5f;
    [Range(0.0f, 180.0f)]
    public float aim_tolerance = 30.0f;

    public float max_dash_dst = 200;
    public LayerMask enemy_layer;
    public float detection_time = 0.5f;

    float horizontal = 0;
    float vertical = 0;

    float aim_horizontal = 0;
    float aim_vertical = 0;

    //Movement
    float velocity = 0.0f;
    Vector3 direction = Vector3.zero;

	
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
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 new_direction = new Vector3(horizontal, vertical, 0);
            float opposite_percent = Vector3.Angle(new_direction, direction) / 180.0f;
            velocity = velocity + acceleration + acceleration * reactivityPercent * opposite_percent;
            if (velocity >= max_speed)
                velocity = max_speed;
            direction = new_direction;
            transform.position += direction.normalized * (velocity * Time.deltaTime);
        }
        else
        {
            direction = Vector3.zero;
            velocity = 0;
        }
    }

    private void Aiming()
    {
        aim_horizontal = Input.GetAxis("HorizontalAim");
        aim_vertical = Input.GetAxis("VerticalAim");

        if (aim_horizontal == 0 && aim_vertical == 0)
            return;

        Vector3 aim_dir = new Vector3(aim_horizontal, aim_vertical, 0);

        if(ButtonController.button_ctrl.pre_selected_target)
        {
            Vector3 player_enemy_dir = ButtonController.button_ctrl.pre_selected_target.transform.position - transform.position;
            float angle = Vector3.Angle(player_enemy_dir.normalized, aim_dir.normalized);

            if(angle <= aim_tolerance)
            {
                ButtonController.button_ctrl.pre_selected_target.Seen();
            }
            else
            {
                //Find a new target
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
        }
        else
        {
            //Find a new target
            RaycastHit hit;
            if (Physics.Raycast(transform.position, aim_dir.normalized, out hit, max_dash_dst, enemy_layer))
            {
                Enemy enemy = hit.transform.gameObject.GetComponentInParent<Enemy>();
                if (enemy)
                {
                    enemy.Seen();
                }
                else
                {
                    Debug.Log("Enemy doesn't have a button_enemy script");
                }
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
       Vector3 dst = new Vector3(aim_horizontal, aim_vertical, 0);

       Gizmos.DrawLine(transform.position, transform.position + (dst.normalized * 3));
   }
}