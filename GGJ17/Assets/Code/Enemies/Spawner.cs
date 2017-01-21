using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        //CreateEnemy();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GameObject CreateEnemy(GameObject enemy, GameObject target)
    {
        float offset_x = Random.Range(0.0f, 2.0f);
        float offset_y = Random.Range(0.0f, 2.0f);
        Vector3 spawn_position = transform.position;
        spawn_position.x += offset_x;
        spawn_position.y += offset_y;

        GameObject new_enemy = Instantiate(enemy, spawn_position, transform.rotation) as GameObject;        
        new_enemy.GetComponent<Seek>().target = target.transform;
        return new_enemy;
    }
}
