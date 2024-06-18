using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public enum Locations
{
    Default,
    One,
    Two,
    Three
}

[Serializable]
public class LocationData
{
    public Transform StartingPosition;
    public string AreaName;
    

}
public class LocationHandler : MonoBehaviour
{
    public Transform SpawnLocation;
    public OVRPassthroughLayer PassThroughLayer;
    public XROrigin XrOrigin;
    public GameObject SmallCity;
    public GameObject LargeCity;
    [Range(0f, 1f)]
    public float PassthroughVal;
    public Camera Cam;

    public List<Behaviour> Components = new();

    private void Awake()
    {
        PassThroughLayer.textureOpacity = PassthroughVal;
        Cam.clearFlags = CameraClearFlags.SolidColor;
        Cam.backgroundColor = Color.black;
    }
    public void SendToLocation()
    {
        PassThroughLayer.textureOpacity = 0f;
        Cam.clearFlags = CameraClearFlags.Skybox;
        foreach (var component in Components) 
        {
            component.enabled = true;
        }
        SmallCity.SetActive(false);
        LargeCity.SetActive(true);
        XrOrigin.transform.SetPositionAndRotation(SpawnLocation.position, SpawnLocation.rotation);
    }
}
