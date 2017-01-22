using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour 
{
    public float dash_x_displacement = 0.8f;

    enum PAnimation
    {
        idleL = 1,
        idleR = 2,
        runL = 3,
        runR = 4,
        punchL = 5,
        punchR = 6,
        dieL = 7,
        dieR = 8,
        dissapearL = 9,
        dissapearR = 10,
        appearL = 11,
        appearR = 12,
        failL = 13,
        failR = 14
    };

    PAnimation p_anim = PAnimation.idleR;

    Animator anim;
    Player player;
    Enemy enemy = null;

    bool playing_game_over = false; //This animation is above all the others
    bool playing_punch = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        //Nothing to do if it's game over
        if (playing_game_over)
            return;

        bool face_right = (player.direction.x >= 0) ? true : false;

        //No punch-dissapear or appear
        if(!playing_punch && player.stunned == false)
        {
            if (player.velocity != 0)
            {
                //Run
                RunCheck(face_right);
            }
            else
            {
                //Idle
                IdleCheck(face_right);
            }
        }

        player.block_movement = false;

        //Punch
        if(playing_punch)
        {
            player.block_movement = true;
            if (CurrentAnimFinish())
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("punchR") || anim.GetCurrentAnimatorStateInfo(0).IsName("punchL"))
                {
                    playing_punch = false;
                    enemy = null;
                }

                if(enemy != null && (anim.GetCurrentAnimatorStateInfo(0).IsName("dissapearL") || anim.GetCurrentAnimatorStateInfo(0).IsName("dissapearR")))
                {
                    //Kill the enemy HERE!
                    player.KillEnemy(enemy);
                    //tp
                    if(enemy.transform.position.x >= transform.position.x)
                        transform.position = enemy.transform.position - new Vector3(dash_x_displacement, 0, 0);
                    else
                        transform.position = enemy.transform.position + new Vector3(dash_x_displacement, 0, 0);
                    enemy = null;
                }
            }
        }
    }

    public bool CurrentAnimFinish()
    {
       return  (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime) ? false : true;
    }

    //Deprecated as fuck
    public string GetRealAnimName()
    {
        int id_punchL = Animator.StringToHash("appearL");
        int id_punchR = Animator.StringToHash("appearR");

        int hash = anim.GetCurrentAnimatorStateInfo(0).GetHashCode();

        if (id_punchL == hash)
            return "punchL";
        if (id_punchR == hash)
            return "punchR";

        return "";
    }


    //This one uses the variable p_anim that in the dash is wrong.
    public string GetCurrentAnimName()
    {
        string ret = "";
        switch(p_anim)
        {
            case PAnimation.idleL:
                ret = "IdleL";
                break;
            case PAnimation.idleR:
                ret = "IdleR";
                break;
            case PAnimation.runL:
                ret = "runL";
                break;
            case PAnimation.runR:
                ret = "runR";
                break;
            case PAnimation.punchL:
                ret = "punchL";
                break;
            case PAnimation.punchR:
                ret = "punchR";
                break;
            case PAnimation.dieL:
                ret = "dieL";
                break;
            case PAnimation.dieR:
                ret = "dieR";
                break;
            case PAnimation.dissapearL:
                ret = "dissapearL";
                break;
            case PAnimation.dissapearR:
                ret = "dissapearR";
                break;
            case PAnimation.appearL:
                ret = "appearL";
                break;
            case PAnimation.appearR:
                ret = "appearR";
                break;
        }

        return ret;
    }

    public void PlayPunch(Transform target)
    {
        if (playing_game_over)
            return;

        if(target.transform.position.x > transform.position.x)
        {
            //Right
            anim.Play("punchR");
            p_anim = PAnimation.punchR;
        }
        else
        {
            //Left
            anim.Play("punchL");
            p_anim = PAnimation.punchL;
        }
        playing_punch = true;
        enemy = null;
    }

    public void PlayDash(Enemy enemy)
    {
        if (playing_game_over)
            return;

        if (enemy.transform.position.x > transform.position.x)
        {
            //Right
            anim.Play("dissapearR");
            p_anim = PAnimation.dissapearR;
        }
        else
        {
            //Left
            anim.Play("dissapearL");
            p_anim = PAnimation.dissapearL;
        }
        playing_punch = true;
        this.enemy = enemy;
    }

    private void IdleCheck(bool face_right)
    {
        if (p_anim != PAnimation.idleR || p_anim != PAnimation.idleL)
        {
            if ((int)p_anim % 2 == 0)
            {
                anim.Play("IdleR");
                p_anim = PAnimation.idleR;
            }
            else
            {
                anim.Play("IdleL");
                p_anim = PAnimation.idleL;
            }
        }
    }

    private void RunCheck(bool face_right)
    {
        if (p_anim != PAnimation.runR || p_anim != PAnimation.runL)
        {
            if (face_right)
            {
                anim.Play("runR");
                p_anim = PAnimation.runR;
            }
            else
            {
                anim.Play("runL");
                p_anim = PAnimation.runL;
            }
        }
    }


    public void GameOverAnim()
    {
        playing_game_over = true;
        if ((int)p_anim % 2 == 0)
            anim.Play("dieR");
        else
            anim.Play("dieL");
    }

   public void PlayFailAnim()
    {
       if((int) p_anim % 2 == 0)
       {
           p_anim = PAnimation.failR;
           anim.Play("failR");
       }
       else
       {
           p_anim = PAnimation.failL;
           anim.Play("failL");
       }
    }
    
}
