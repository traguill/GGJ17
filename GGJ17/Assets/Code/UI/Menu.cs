using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

    public void Play()
    {
        SceneManager.LoadScene("FinalComboAllTogether");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
