using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager wave_manager;
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
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!spawner_manager.IsSpawning() && enemies_alive.Count == 0)
        {
            actual_wave++;
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
