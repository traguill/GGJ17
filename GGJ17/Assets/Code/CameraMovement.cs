using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour 
{
    public Transform target;

    [Header("Balance")]
    public float limit_x = 3;
    public float limit_y = 2.5f;

    float half_size_x;
    float half_size_y;

    float current_transition;
    public float total_transition;

    void Awake()
    {
        Camera.main.orthographicSize = Screen.height / 32.0f / 2.0f;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

        half_size_x = bounds.extents.x;
        half_size_y = bounds.extents.y;

        current_transition = 0.0f;
    }
	
	void LateUpdate()
    {
        float pos_x = 0.0f;
        float pos_y = 0.0f;
        //Margin Left
        if(target.position.x < transform.position.x - half_size_x + limit_x)
            pos_x = target.position.x + half_size_x - limit_x;
        //MarginRight
        if (target.position.x > transform.position.x + half_size_x - limit_x)
            pos_x = target.position.x - half_size_x + limit_x;
        //MarginTop
        if (target.position.y > transform.position.y + half_size_y - limit_y)
            pos_y = target.position.y - half_size_y + limit_y;
        //MarginBottom
        if (target.position.y < transform.position.y - half_size_y + limit_y)
            pos_y = target.position.y + half_size_y - limit_y;

        pos_x = RoundToNearestPixel(pos_x);
        pos_y = RoundToNearestPixel(pos_y);

        if (pos_x != 0 && pos_y != 0)
            transform.position = new Vector3(pos_x, pos_y, transform.position.z);
        else
        {
            if (pos_x != 0)
                transform.position = new Vector3(pos_x, transform.position.y, transform.position.z);

            if (pos_y != 0)
                transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
        }
    }

    public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = (Screen.height / (Camera.main.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (Camera.main.orthographicSize * 2));
        return adjustedUnityUnits;
    }

    public void CenterCamera()
    {
        if (current_transition < total_transition)
        {
            current_transition += Time.deltaTime;

            float pos_x = target.position.x;
            float pos_y = target.position.y;

            Vector3 final_pos = Vector3.Lerp(transform.position, new Vector3(pos_x, pos_y, transform.position.z), (current_transition / total_transition));
            final_pos.x = RoundToNearestPixel(final_pos.x);
            final_pos.y = RoundToNearestPixel(final_pos.y);
            transform.position = final_pos;
        }
    }

    /*currentStep += Time.deltatime;  
    Vector3 oldposition = camera.transform.position;
    camera.transform.position = Vector3.Lerp(oldposition, cameratarget.transform.position, currentStep / panSteps);*/
}
