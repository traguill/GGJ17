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
    }

    // Update is called once per frame
    void Update () 
    {
	    if(game_over)
        {
            SceneManager.LoadScene("MenuScene");
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
