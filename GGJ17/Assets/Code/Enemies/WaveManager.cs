using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wave_manager;
    public SpawnerManager spawner_manager;

    public Text actual_wave_ui;
    public Text countdown;

    int count;
    bool countdown_activated;
    float countdown_timer;
    bool wave_changed;

    public List<Vector3> waves;
    public List<Vector3> initial_waves;
    public int actual_wave;
    public List<GameObject> enemies_alive = new List<GameObject>();    

    void Awake()
    {
        wave_manager = this;
    }
    
    // Use this for initialization
    void Start ()
    {
        actual_wave = 0;
        actual_wave_ui.text = (actual_wave + 1).ToString();
        countdown_timer = 0.0f;
        countdown_activated = false;
        wave_changed = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!spawner_manager.IsSpawning() && enemies_alive.Count == 0)
        {
            if (!wave_changed)
                ChangeWave();

            if (actual_wave == waves.Count)
                GameLoop.manager.player_wins = true;

            else if (!countdown_activated)
                ActivateCountdown();   
        }

        if(countdown_activated)
        {
            if (countdown_timer > 1.0f)
            {
                if (count > 0)
                {
                    count--;
                    countdown.text = count.ToString();
                    countdown_timer = 0.0f;
                }
                else
                {
                    DesctivateCountdown();
                    actual_wave_ui.text = (actual_wave + 1).ToString();
                    spawner_manager.WaveChanged();
                    wave_changed = false;
                }
            }
            else
                countdown_timer += Time.deltaTime;
        }
    }

    public void DestroyEnemy(GameObject enemy_to_destroy)
    {
        enemies_alive.Remove(enemy_to_destroy);
    }

    public void AddEnemy(GameObject new_enemy)
    {
        enemies_alive.Add(new_enemy);
    }

    public Vector3 GetActualWave()
    {
        return waves[actual_wave];
    }

    public Vector3 GetActualInitialWave()
    {
        return initial_waves[actual_wave];
    }

    public int EnemiesAlive()
    {
        return enemies_alive.Count;
    }

    void ActivateCountdown()
    {
        count = 5;
        countdown_timer = 0.0f;
        countdown.text = count.ToString();
        countdown.gameObject.SetActive(true);
        countdown_activated = true;        
    }

    void DesctivateCountdown()
    {
        countdown.gameObject.SetActive(false);
        countdown_activated = false;
    }

    void ChangeWave()
    {
        actual_wave++;
        wave_changed = true;
    }
}
