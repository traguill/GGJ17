using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLoop : MonoBehaviour 
{
    public static GameLoop manager;

    public Image life1;
    public Image life2;
    public Image life3;

    public Text you_lose;
    Color you_lose_alpha;

    public Text press_button;
    Color press_button_alpha;

    public int lifes = 3;
    bool game_over = false;

    void Awake()
    {
        manager = this;
    }

    void Start()
    {
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);

        you_lose_alpha = you_lose.color;
        you_lose_alpha.a = 0;
        you_lose.color = you_lose_alpha;

        press_button_alpha = press_button.color;
        press_button_alpha.a = 0;
        press_button.color = press_button_alpha;
    }

    // Update is called once per frame
    void Update () 
    {
	    if(game_over)
        {
            if (you_lose_alpha.a <= 1.0f)
            {
                you_lose_alpha.a += 0.01f;
                you_lose.color = you_lose_alpha;
            }

            else if(press_button_alpha.a <= 1.0f)
            {
                press_button_alpha.a += 0.01f;
                press_button.color = press_button_alpha;
            }

            else if(Input.anyKey)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }

    public void RemoveLife()
    {
        lifes--;
        if (lifes == 2)
            life3.gameObject.SetActive(false);
        else if (lifes == 1)
            life2.gameObject.SetActive(false);
        else if (lifes == 0)
        {
            life1.gameObject.SetActive(false);
            game_over = true;
        }
    }
}
