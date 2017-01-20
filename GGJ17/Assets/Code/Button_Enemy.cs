using UnityEngine;
using System.Collections;

public class Button_Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float visible_time = 2.0f;
    public float aim_fade_time = 0.2f;

    [Header("Configuration")]
    public Sprite[] buttons;
    SpriteRenderer sprite;
    public Enemy parent;

    bool is_visible = false;
    int id = -1;
    int hide_id = -1;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hide_id = Random.Range(0, 3);
    }

    public void ShowButton()
    {
        if(!is_visible)
        {
            id = hide_id;
            is_visible = true;
            sprite.sprite = buttons[id];
            StartCoroutine("HideButton");
            ButtonController.button_ctrl.AddEnemy(parent, id);
        } 
    }

    public void ShowAimButton()
    {
        if(!is_visible)
        {
            id = hide_id;
            is_visible = true;
            sprite.sprite = buttons[id];
            ButtonController.button_ctrl.AddEnemy(parent, id);
        }
    }

    public void HideButtonAnim()
    {
        StartCoroutine("HideButtonAnimCo");
    }

    IEnumerator HideButtonAnimCo()
    {
        yield return new WaitForSeconds(aim_fade_time);
        sprite.sprite = null;
        is_visible = false;
        id = -1;
        ButtonController.button_ctrl.RemoveEnemy(parent);
    }

    IEnumerator HideButton()
    {
        yield return new WaitForSeconds(visible_time);
        sprite.sprite = null;
        is_visible = false;
        id = -1;
        ButtonController.button_ctrl.RemoveEnemy(parent);
    }
}
