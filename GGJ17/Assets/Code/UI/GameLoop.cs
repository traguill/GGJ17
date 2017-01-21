using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLoop : MonoBehaviour 
{
    public static GameLoop manager;
    public Text life_text;
    int lifes = 3;

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
	
	}

    public void RemoveLife()
    {
        lifes--;
        life_text.text = "Lifes: " + lifes.ToString();
    }
}
