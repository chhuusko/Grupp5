using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -15f);
    [SerializeField] private float smoothing = 1.0f;

    void LateUpdate()
    {
        //Moving the camera to the players position using a Lerp to make it smoother/add small delay
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        transform.position = newPosition;
    }
}
