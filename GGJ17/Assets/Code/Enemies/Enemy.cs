﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float hit_radius = 1.0f;
    public int detect_frames = 2;
    public float charge_attack_time = 1.0f;

    [Header("Configuration")]
    public Button_Enemy button;
    public LayerMask player_layer;

    //Player aims to the enemy
    int frames_d = 0; //frames detected
    int frames_b = 0; //frames begin
    bool aim_button_visible = false;

    //Player detected
    bool player_detected = false;
    float charge_time = 0.0f;

    bool dead = false;
	
	// Update is called once per frame
	void Update () 
    {
        if (dead)
            return;

        frames_b = frames_d;
        
        if(!player_detected)
            DetectPlayer();
        else
        {
            charge_time += Time.deltaTime;
            if(charge_time >= charge_attack_time)
            {
                //Attack
                Attack();
               
            }
        }

        if(frames_d >= detect_frames)
        {
            aim_button_visible = true;
            button.PreSelectedShow();
            ButtonController.button_ctrl.SetPreSelectedEnemy(this);
        }

        if(aim_button_visible == true && frames_d == 0)
        {
            HidePreSelectedButton();
        }

        if (GameLoop.manager.IsGameOver())
            HidePermanentButton();
	}

    void LateUpdate()
    {
        if(frames_d != 0)
        {
            if (frames_b == frames_d)
                frames_d--;
        }
    }

    private void DetectPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, hit_radius, player_layer);
        
        if(player)
        {
            button.ShowButton();
            player_detected = true;
            //Stop all movement
            SteeringBasics basics = GetComponent<SteeringBasics>();
            basics.stop = true;
            //Start to charge the attack
            charge_time = 0.0f;
            GetComponent<EnemyAnim>().ChargeAttackAnim();
        }
    }

    public void ShowButton()
    {
        if (GameLoop.manager.IsGameOver())
            button.ShowButton();
    }

    public void ShowPermanentButton()
    {
        button.ShowAimButton();
    }

    public void HidePermanentButton()
    {
        button.HideButtonAnim();
    }

    //Seen by the player
    public void Seen()
    {
        if(frames_d < detect_frames)
            frames_d++;
    }

    public void HidePreSelectedButton()
    {
        aim_button_visible = false;
        button.PreSelectedHide();
    }

    public void SetDead()
    {
        dead = true;
        button.HideButtonAnim();

        //Stop all movement
        SteeringBasics basics = GetComponent<SteeringBasics>();
        basics.stop = true;

    }

    private void Attack()
    {
        if(!Player.pl.invulnerable)
            GameLoop.manager.RemoveLife();
        //Play attack animation.
        Vector3 new_pos = Player.pl.transform.position;
        new_pos.y -= 0.2f;
        transform.position = new_pos;
        GetComponent<EnemyAnim>().AttackAnim();

        //Hit player
        Player.pl.PlayerHit();
        //Reset the attack loop.
        charge_time = 0.0f;
    }
    //Discoment to show the attack radius sphere
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, hit_radius);
    }*/
}
