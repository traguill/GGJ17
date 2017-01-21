using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    public List<GameObject> spawners;
    float timer;
    public float time_to_spawn;
    public int max_enemies;
    public int enemies_number;
    bool stop_spawning;

    // Use this for initialization
    void Start ()
    {
        timer = 0.0f;
        stop_spawning = false;
        enemies_number = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!stop_spawning)
        {
            if (timer > time_to_spawn)
            {
                Spawn();
                timer = 0.0f;
            }
            else
                timer += Time.deltaTime;
        }
    }

    void Spawn()
    {
        int chosen_spawner = Random.Range(0, spawners.Count);
        spawners[chosen_spawner].GetComponent<Spawner>().CreateEnemy();

        ++enemies_number;
        if (enemies_number == max_enemies)
            stop_spawning = true;     
    }
}
