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

    public void CreateEnemy()
    {
        GameObject obj = GameObject.Find("Enemy");
        Instantiate(obj, transform.position, transform.rotation);
    }
}
