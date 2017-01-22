using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour 
{
    SteeringBasics movement;
    Animator anim;
    bool facing_right = false;

    bool attacking = false;
    bool dead = false;

	void Awake()
    {
        movement = GetComponent<SteeringBasics>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(!attacking && !dead)
        {
            //Movement
            if (movement.movement.x >= 0 && facing_right == false)
            {
                anim.Play("RunR");
                facing_right = true;
            }

            if (movement.movement.x < 0 && facing_right == true)
            {
                anim.Play("RunL");
                facing_right = false;
            }
        }
	}

    public void ChargeAttackAnim()
    {
        attacking = true;
        if (facing_right)
            anim.Play("ChargeR");
        else
            anim.Play("ChargeL");
    }

    public void AttackAnim()
    {
        if (facing_right)
            anim.Play("AttackR");
        else
            anim.Play("AttackL");
    }

    public void DieAnim()
    {
        dead = true;
        if (facing_right)
            anim.Play("DieR");
        else
            anim.Play("DieL");
    }
}
