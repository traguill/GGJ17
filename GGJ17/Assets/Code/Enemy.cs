using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float hit_radius = 1.0f;

    [Header("Configuration")]
    public Button_Enemy button;
    public LayerMask player_layer;

	
	// Update is called once per frame
	void Update () 
    {
        DetectPlayer();
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

    public void ShowButtonAim()
    {
        button.ShowAimButton();
    }

    //Discoment to show the attack radius sphere
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, hit_radius);
    }*/
}
