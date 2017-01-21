using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float hit_radius = 1.0f;
    public int detect_frames = 2;

    [Header("Configuration")]
    public Button_Enemy button;
    public LayerMask player_layer;

    //Player aims to the enemy
    int frames_d = 0; //frames detected
    int frames_b = 0; //frames begin
    bool aim_button_visible = false;
	
	// Update is called once per frame
	void Update () 
    {
        frames_b = frames_d;

        DetectPlayer();

        if(frames_d >= detect_frames)
        {
            aim_button_visible = true;
            button.ShowAimButton();
        }

        if(aim_button_visible == true && frames_d == 0)
        {
            aim_button_visible = false;
            button.HideButtonAnim();
        }
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
        }
    }

    public void ShowButton()
    {
        button.ShowButton();
    }

    //Seen by the player
    public void Seen()
    {
        if(frames_d < detect_frames)
            frames_d++;
    }

    //Discoment to show the attack radius sphere
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, hit_radius);
    }*/
}
