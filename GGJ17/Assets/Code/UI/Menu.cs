using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            Play();
    }

    public void Play()
    {
        SceneManager.LoadScene("FinalComboAllTogether");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
