using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private Vector3 _initPos;
    private Quaternion _initRot;

    private void Awake()
    {
        _initPos = transform.position;
        _initRot = transform.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boom!");
            transform.SetPositionAndRotation(_initPos, _initRot);
        }
    }
}
