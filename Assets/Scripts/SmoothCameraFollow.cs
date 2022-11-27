using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void LateUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 4, -15));




        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        //TESTAR O LERP!!!!!!!!!!!


        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.7f);


        //transform.rotation = target.rotation;


    }

 
}
