using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

    public float min_intensity;
    public float max_intensity;

    public float speed;
    bool grow = true;

    Light light;

    void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = min_intensity;
    }

	// Update is called once per frame
	void Update ()
    {
        if(grow)
        {
            light.intensity += speed * Time.deltaTime;
            if (light.intensity >= max_intensity)
                grow = false;
        }
        else
        {
            light.intensity -= speed * Time.deltaTime;
            if (light.intensity <= min_intensity)
                grow = true;
        }
    }
}
