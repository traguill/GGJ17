using UnityEngine;
using System.Collections;

public class Button_Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float visible_time = 2.0f;

    [Header("Configuration")]
    public Sprite[] buttons;
    SpriteRenderer sprite;

    bool is_visible = false;
    int id = -1;
    bool is_aim_visible = false;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void PreUpdate()
    {
        is_aim_visible = false;
    }

    void PostUpdate()
    {
        if(is_aim_visible == false && id == -1 && is_visible == false)
        {
            sprite.sprite = null;
        }
    }
    
    public void ShowButton()
    {
        if(!is_visible)
        {
            id = Random.Range(0, 3);
            is_visible = true;
            sprite.sprite = buttons[id];
            StartCoroutine("HideButton");
        } 
    }

    public void ShowAimButton()
    {
       if(is_visible == false)
       {
           //First time seen
           if(id == -1)
           {
               id = Random.Range(0, 3);
               sprite.sprite = buttons[id];
           }
           is_aim_visible = true;
       }
    }

    IEnumerator HideButton()
    {
        yield return new WaitForSeconds(visible_time);
        sprite.sprite = null;
        is_visible = false;
        id = -1;
    }
}
