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
        GameObject new_enemy = Instantiate(enemy, transform.position, transform.rotation) as GameObject;        
        new_enemy.GetComponent<Seek>().target = target.transform;
        return new_enemy;
    }
}
