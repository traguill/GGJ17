using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public List<Spawner> spawners;    

    float timer;
    float time_to_spawn;
    int wave_enemies;
    public int enemies_spawned;
    bool stop_spawning;
    int enemies_for_spawn;

    // Use this for initialization
    void Start ()
    {
        WaveChanged();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!stop_spawning)
        {
            if (timer > time_to_spawn)
            {
                if (WaveManager.wave_manager.actual_wave == 1)
                {
                    int random = Random.Range(0, spawners.Count);
                    Spawn(random);
                    Spawn(random);
                }
                else
                {
                    for (int i = 0; i < enemies_for_spawn; i++)
                        Spawn();
                }
                timer = 0.0f;
            }
            else
                timer += Time.deltaTime;
        }
    }

    void Spawn()
    {
        int chosen_spawner = Random.Range(0, spawners.Count);
        GameObject new_enemy = spawners[chosen_spawner].CreateEnemy(enemy, player);
        WaveManager.wave_manager.AddEnemy(new_enemy);

        ++enemies_spawned;
        if (enemies_spawned == wave_enemies)
            stop_spawning = true;     
    }

    void Spawn(int chosen_spawner)
    {
        GameObject new_enemy = spawners[chosen_spawner].CreateEnemy(enemy, player);
        WaveManager.wave_manager.AddEnemy(new_enemy);

        ++enemies_spawned;
        if (enemies_spawned == wave_enemies)
            stop_spawning = true;
    }

    public void WaveChanged()
    {
        timer = 0.0f;
        time_to_spawn = WaveManager.wave_manager.GetActualWave().y;
        stop_spawning = false;
        enemies_spawned = 0;
        wave_enemies = (int)WaveManager.wave_manager.GetActualWave().x;
        enemies_for_spawn = (int)WaveManager.wave_manager.GetActualWave().z;

        StartWave();
    }

    void StartWave()
    {
        Vector3 initial_wave = WaveManager.wave_manager.GetActualInitialWave();
        for (int i  = 0; i < initial_wave.x; i++)
        {
            Spawn(0);
        }
        for (int i = 0; i < initial_wave.y; i++)
        {
            Spawn(1);
        }
        for (int i = 0; i < initial_wave.z; i++)
        {
            Spawn(2);
        }
    }
        
    public bool IsSpawning()
    {
        return !stop_spawning;
    }    
}
