using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour 
{
    [Header("Balance")]
    public float hit_radius = 1.0f; ///Configure with the enemy hit radius.Must have the same value
    List<EnemyItem> enemies = new List<EnemyItem>(); //Enemies you can PUNCH!!!

    public static ButtonController button_ctrl;

    public Player player;

    Enemy target = null;
    [HideInInspector]
    public Enemy pre_selected_target = null;

    void Awake()
    {
        button_ctrl = this;
    }

    void Update()
    {
        PlayerInput();

        //Select target
        if(Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if(pre_selected_target)
            {
                if (target)
                    target.HidePermanentButton();
                target = pre_selected_target;
                target.ShowPermanentButton();
                pre_selected_target = null;
            }
        }
    }
	
    public void AddEnemy(Enemy enemy, int button)
    {
        enemies.Add(new EnemyItem(enemy, button));
    }

    public void RemoveEnemy(Enemy enemy)
    {
        foreach(EnemyItem item in enemies)
            if(item.enemy == enemy)
            {
                enemies.Remove(item);
                break;
            }
    }

    private void PlayerInput()
    {
        //A
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            CheckKeyPressed(0);
        }

        //B
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            CheckKeyPressed(1);
        }

        //X
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            CheckKeyPressed(2);
        }

        //Y
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            CheckKeyPressed(3);
        }
    }

    private void CheckKeyPressed(int id)
    {
        Enemy enemy = null;
        foreach (EnemyItem item in enemies)
        {
            if (item.id == id)
            {
                enemy = item.enemy;
                break;
            }
        }

        if (enemy)
        {
            //Check enemy is near or far. Near anim kill. Far dash to kill
            Vector3 distance = enemy.transform.position - player.transform.position;
            if(distance.magnitude >= hit_radius)
            {
                //Dash
                player.transform.position = enemy.transform.position;
            }
            else
            {
                //No dash
            }

            WaveManager.wave_manager.DestroyEnemy(enemy.gameObject);
            RemoveEnemy(enemy);
            Destroy(enemy.gameObject);            
        }
        else
        {
            //Player has pressed a key that is not on the screen
            Debug.Log("Miss key");
        }
    }

    public void SetPreSelectedEnemy(Enemy enemy)
    {
        /*if(pre_selected_target)
        {
            pre_selected_target.HidePreSelectedButton();
        }*/
        pre_selected_target = enemy;
    }
}
