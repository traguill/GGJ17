using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public float aim_tolerance = 20.0f;
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
    [HideInInspector]
    public bool invulnerable = false;
    public static Player pl;

    [Header("Bounds")]
    public float min_x;
    public float max_x;
    public float min_y;
    public float max_y;

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
        CheckBounds();
    }

    private void CheckBounds()
    {
        float new_x = 0;
        float new_y = 0;
        if (transform.position.x < min_x)
        {
            new_x = min_x;
        }
        if (transform.position.x > max_x)
        {
            new_x = max_x;
        }
        if (transform.position.y < min_y)
        {
            new_y = min_y;
        }
        if (transform.position.y > max_y)
        {
            new_y = max_y;
        }

        Vector3 new_position = transform.position;
        if (new_x != 0)
            new_position.x = new_x;
        if (new_y != 0)
            new_position.y = new_y;

        transform.position = new_position;
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
                Enemy enemy = AimingEnemies(aim_dir);
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
        else
        {
            Enemy enemy = AimingEnemies(aim_dir);
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

    private Enemy AimingEnemies(Vector3 aim_dir)
    {
        List<GameObject> enemies = WaveManager.wave_manager.enemies_alive;        
        Vector3 enemy_dir;
        float min_angle = 0.0f;
        int target_enemy = 0;

        for(int i = 0; i < enemies.Count; i++)
        {
            if (i == 0)
            {
                enemy_dir = enemies[i].transform.position - transform.position;
                min_angle = Vector3.Angle(aim_dir, enemy_dir);
                target_enemy = i;
            }
            else
            {
                enemy_dir = enemies[i].transform.position - transform.position;
                float tmp_angle = Vector3.Angle(aim_dir, enemy_dir);

                if(tmp_angle < min_angle)
                {
                    min_angle = tmp_angle;
                    target_enemy = i;
                }
            }
        }

        if (enemies.Count > 0 && min_angle <= aim_tolerance)
            return enemies[target_enemy].GetComponent<Enemy>();
        else
            return null;
    }

    public void Stun()
    {
        //Execute fail animation
        if(stunned == false)
        {
            stunned = true;
            stunned_time = 0.0f;
            GetComponent<PlayerAnim>().PlayFailAnim();
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
       StartCoroutine("EnemyDead", enemy);  
   }

    IEnumerator EnemyDead(Enemy enemy)
    {
        enemy.GetComponent<EnemyAnim>().DieAnim();
        enemy.SetDead();
        yield return new WaitForSeconds(2);
        WaveManager.wave_manager.DestroyEnemy(enemy.gameObject);
        ButtonController.button_ctrl.RemoveEnemy(enemy);
        Destroy(enemy.gameObject); 
    }

    //The enemy has hit the player
    public void PlayerHit()
    {
        if(invulnerable == false)
        {
            if (!GameLoop.manager.IsGameOver())
                StartCoroutine("DamagedPlayer");
            else
                GetComponent<PlayerAnim>().GameOverAnim();

            invulnerable = true;
        }
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

        invulnerable = false;
    }
}