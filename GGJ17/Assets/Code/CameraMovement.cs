﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    public Transform target;

    [Header("Balance")]
    public float limit_x = 3;
    public float limit_y = 2.5f;

    float half_size_x;
    float half_size_y;

    void Awake()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

        half_size_x = bounds.extents.x;
        half_size_y = bounds.extents.y;
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
}