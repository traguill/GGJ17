using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLoop : MonoBehaviour 
{
    public static GameLoop manager;
    public Text life_text;
    int lifes = 3;

    bool game_over = false;

    void Awake()
    {
        manager = this;
    }

    void Start()
    {
        life_text.text = "Lifes: " + lifes.ToString();
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
        life_text.text = "Lifes: " + lifes.ToString();
        if (lifes == 0)
            game_over = true;
    }
}
