using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameLoop : MonoBehaviour 
{
    public static GameLoop manager;
    public CameraMovement camera;

    public Image life1;
    public Image life2;
    public Image life3;

    public Text you_lose;
    Color you_lose_alpha;

    public Text you_win;
    Color you_win_alpha;

    public Text press_button;
    Color press_button_alpha;

    public int lifes = 3;
    bool game_over = false;
    public bool player_wins = false;

    public AudioClip clip_win;
    public AudioClip clip_lose;
    public AudioClip music;
    public AudioSource sound_source;
    public AudioSource music_source;

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
        you_lose.gameObject.SetActive(true);

        you_win_alpha = you_win.color;
        you_win_alpha.a = 0;
        you_win.color = you_win_alpha;
        you_win.gameObject.SetActive(true);

        press_button_alpha = press_button.color;
        press_button_alpha.a = 0;
        press_button.color = press_button_alpha;
        press_button.gameObject.SetActive(true);

        music_source.clip = music;
        music_source.Play();
    }

    // Update is called once per frame
    void Update () 
    {
	    if(game_over)
        {
            music_source.Stop();
            camera.CenterCamera();

            if (you_lose_alpha.a <= 1.0f)
            {
                you_lose_alpha.a += 0.01f;
                you_lose.color = you_lose_alpha;
            }

            else if(press_button_alpha.a <= 1.0f)
            {
                sound_source.PlayOneShot(clip_lose);
                press_button_alpha.a += 0.01f;
                press_button.color = press_button_alpha;
            }

            else if(Input.anyKey)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
        else if(player_wins)
        {
            music_source.Stop();
            camera.CenterCamera();

            if (you_win_alpha.a <= 1.0f)
            {       
                you_win_alpha.a += 0.01f;
                you_win.color = you_win_alpha;
            }

            else if (press_button_alpha.a <= 1.0f)
            {
                sound_source.PlayOneShot(clip_win);
                press_button_alpha.a += 0.01f;
                press_button.color = press_button_alpha;
            }

            else if (Input.anyKey)
            {
                SceneManager.LoadScene("Credits");
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

    public bool IsGameOver()
    {
        return game_over;
    }
}
