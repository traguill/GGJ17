using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioClip music;
    public AudioSource source;

    void Start()
    {
        source.clip = music;
        source.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            Play();
    }

    public void Play()
    {
        source.Stop();
        SceneManager.LoadScene("FinalComboAllTogether");
    }

    public void Credits()
    {
        source.Stop();
        SceneManager.LoadScene("Credits");
    }
}
