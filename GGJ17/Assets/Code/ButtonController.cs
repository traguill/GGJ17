using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour 
{
    List<EnemyItem> enemies = new List<EnemyItem>(); //Enemies you can PUNCH!!!

    public static ButtonController button_ctrl;

    void Awake()
    {
        button_ctrl = this;
    }

    void Update()
    {
        PlayerInput();
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
            Enemy enemy = CheckKeyPressed(0);
            if(enemy)
            {
                //Check enemy is near or far. Near anim kill. Far dash to kill
                RemoveEnemy(enemy);
                Destroy(enemy.gameObject);
            }
            else
            {
                //Player has pressed a key that is not on the screen
                Debug.Log("Miss key");
            }
        }

        //B
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Enemy enemy = CheckKeyPressed(1);
            if (enemy)
            {
                //Check enemy is near or far. Near anim kill. Far dash to kill
                RemoveEnemy(enemy);
                Destroy(enemy.gameObject);
            }
            else
            {
                //Player has pressed a key that is not on the screen
                Debug.Log("Miss key");
            }
        }

        //X
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Enemy enemy = CheckKeyPressed(2);
            if (enemy)
            {
                //Check enemy is near or far. Near anim kill. Far dash to kill
                RemoveEnemy(enemy);
                Destroy(enemy.gameObject);
            }
            else
            {
                //Player has pressed a key that is not on the screen
                Debug.Log("Miss key");
            }
        }

        //Y
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Enemy enemy = CheckKeyPressed(3);
            if (enemy)
            {
                //Check enemy is near or far. Near anim kill. Far dash to kill
                RemoveEnemy(enemy);
                Destroy(enemy.gameObject);
            }
            else
            {
                //Player has pressed a key that is not on the screen
                Debug.Log("Miss key");
            }
        }
    }

    private Enemy CheckKeyPressed(int id)
    {
        foreach (EnemyItem item in enemies)
        {
            if (item.id == id)
                return item.enemy;
        }

        return null;
    }
}
