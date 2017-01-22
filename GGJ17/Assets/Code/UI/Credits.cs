using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {

    public float max_y;
    public float speed;

    bool paused = false;

    RectTransform trans;

    public AudioClip music;
    public AudioSource source;

    void Awake()
    {
        trans = GetComponent<RectTransform>();
    }

    void Start()
    {
        source.clip = music;
        source.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!paused)
        {
            trans.localPosition += new Vector3(0, Time.deltaTime * speed, 0);
            if (trans.localPosition.y >= max_y)
            {
                paused = true;
                StartCoroutine("GoToMenu");
            }
        }     
	}

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MenuScene");
    }
}
