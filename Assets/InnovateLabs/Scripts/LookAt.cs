using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform LookAtObject;
    private Coroutine _lookAtCoroutine;

    void OnEnable()
    {
        if (LookAtObject == null)
        {
            Debug.LogError("look at object not found in the scene.");
            return;
        }
        _lookAtCoroutine = StartCoroutine(LookAtCameraCoroutine());
    }

    IEnumerator LookAtCameraCoroutine()
    {
        while (true)
        {
            transform.LookAt(LookAtObject.transform);
            yield return null;
        }
    }

    void OnDisable()
    {
        if (_lookAtCoroutine != null)
        {
            StopCoroutine(_lookAtCoroutine);
        }
    }
}
