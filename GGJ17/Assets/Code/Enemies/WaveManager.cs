using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wave_manager;
    public Text actual_wave_ui;
    public SpawnerManager spawner_manager;
    public List<Vector3> waves;
    public int actual_wave;
    List<GameObject> enemies_alive = new List<GameObject>();    

    void Awake()
    {
        wave_manager = this;
    }
    
    // Use this for initialization
    void Start ()
    {
        actual_wave = 0;
        actual_wave_ui.text = "Wave: " + (actual_wave + 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!spawner_manager.IsSpawning() && enemies_alive.Count == 0)
        {
            actual_wave++;
            actual_wave_ui.text = "Wave: " + (actual_wave + 1);
            spawner_manager.WaveChanged();
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
}
