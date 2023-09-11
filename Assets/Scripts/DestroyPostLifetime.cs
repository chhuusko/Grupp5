using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPostLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.0f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
