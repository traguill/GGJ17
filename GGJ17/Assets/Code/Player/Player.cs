using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    [Header("Balance")]
    [Header("Movement")]
    public float max_speed = 10.0f;
    public float acceleration = 1.0f;
    [Header("Others")]
    [Range(0.0f, 1.0f)]
    public float reactivityPercent = 0.5f;
    [Range(0.0f, 180.0f)]
    public float aim_tolerance = 30.0f;
    public float max_stun_time = 1.5f;

    public float max_dash_dst = 200;
    public LayerMask enemy_layer;
    public float detection_time = 0.5f;

    float horizontal = 0;
    float vertical = 0;

    float aim_horizontal = 0;
    float aim_vertical = 0;

    //Movement
    [HideInInspector]
    public float velocity = 0.0f;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;

    [HideInInspector]
    public bool block_movement = false;

    //Stun
    [HideInInspector]
    public bool stunned = false;
    float stunned_time = 0.0f;

    SpriteRenderer render;

    public static Player pl;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        pl = this;
    }

	
	// Update is called once per frame
	void Update () 
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (!GameLoop.manager.IsGameOver())
        {

            if (!stunned)
            {
                if (!block_movement)
                    Movement();
                Aiming();
            }
            else
            {
                stunned_time += Time.deltaTime;
                if (stunned_time >= max_stun_time)
                {
                    stunned_time = 0.0f;
                    stunned = false;
                    //End of the stun
                }
            }
        }
        
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

    public void Stun()
    {
        //Execute fail animation
        if(stunned == false)
        {
            stunned = true;
            stunned_time = 0.0f;
        }  
    }

   void OnDrawGizmos()
   {
       Gizmos.color = Color.yellow;
       Vector3 dst = new Vector3(aim_horizontal, aim_vertical, 0);

       Gizmos.DrawLine(transform.position, transform.position + (dst.normalized * 3));
   }

    public void KillEnemy(Enemy enemy)
   {
       WaveManager.wave_manager.DestroyEnemy(enemy.gameObject);
       ButtonController.button_ctrl.RemoveEnemy(enemy);
       Destroy(enemy.gameObject);   
   }

    //The enemy has hit the player
    public void PlayerHit()
    {
        StartCoroutine("DamagedPlayer");
    }

    IEnumerator DamagedPlayer()
    {
        int max_pop = 6;
        for(int i = 0; i < max_pop; i++)
        {
            //Paint red
            render.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            //Paint white
            render.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}