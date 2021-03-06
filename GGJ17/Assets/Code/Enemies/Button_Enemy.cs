﻿using UnityEngine;
using System.Collections;

public class Button_Enemy : MonoBehaviour 
{
    [Header("Balance")]
    public float aim_fade_time = 0.2f;

    [Header("Configuration")]
    public Sprite[] buttons;
    public Sprite selected_icon;
    SpriteRenderer sprite;
    public Enemy parent;

    bool is_visible = false;
    int id = -1;
    int hide_id = -1;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hide_id = Random.Range(0, 4);
    }

    public void ShowButton()
    {
        if(!is_visible)
        {
            id = hide_id;
            is_visible = true;
            sprite.sprite = buttons[id];
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

    public void PreSelectedShow()
    {
        if(!is_visible)
        {
            sprite.sprite = selected_icon;
        }
    }

    public void PreSelectedHide()
    {
        if (!is_visible)
            sprite.sprite = null;
    }

    public void HideButtonAnim()
    {
        StartCoroutine("HideButtonAnimCo");
    }

    public void HideButtonAnimNow()
    {
        sprite.sprite = null;
        is_visible = false;
        id = -1;
        ButtonController.button_ctrl.RemoveEnemy(parent);
    }

    IEnumerator HideButtonAnimCo()
    {
        yield return new WaitForSeconds(aim_fade_time);
        sprite.sprite = null;
        is_visible = false;
        id = -1;
        ButtonController.button_ctrl.RemoveEnemy(parent);
    }
}
