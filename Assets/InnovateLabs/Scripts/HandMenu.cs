using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;
using System;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


#region Helper Classes
[System.Serializable]
public struct GameObjectInfo
{
    public Vector3 Position;
    public Quaternion Rotation;
}
#endregion
public class HandMenu : MonoBehaviour
{
    #region Input Manager
    private InputManager _inputManager;
    #endregion

    #region References
    [Header("Refereneces")]
    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameObject _leftController;
    [SerializeField] private GameObject _rightController;
    [SerializeField] private GameObject _xROrigin;
    #endregion


    [Header("User Inputs")]
    [SerializeField] private Vector3 _positionOffsets;
    [SerializeField] private GameObject _mainCamera;
    private GameObjectInfo _playerInitData;

    public void Awake()
    {
        Debug.Log($"xr : {_xROrigin}, pos: {_xROrigin.transform.localPosition}, rot: {_xROrigin.transform.rotation}");
        _playerInitData.Position = _xROrigin.transform.localPosition;
        _playerInitData.Rotation = _xROrigin.transform.rotation;

        _inputManager = FindObjectOfType<InputManager>();

        _mainMenu.SetActive(true);

        _inputManager.OnButtonPressed += OnButtonPressed;
        _inputManager.OnButtonReleased += OnButtonReleased;
    }

    private void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_mainCamera.transform.position - _mainMenu.transform.position, Vector3.up);
        if (_inputManager.IsLeftButtonHeld)
        {
            Vector3 targetPosition = _leftController.transform.position + _positionOffsets;
            _mainMenu.transform.SetPositionAndRotation(targetPosition, targetRotation);
        }
        else if (_inputManager.IsRightButtonHeld)
        {
            Vector3 targetPosition = _rightController.transform.position + _positionOffsets;
            _mainMenu.transform.SetPositionAndRotation(targetPosition, targetRotation);

        }
        
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (_inputManager.IsLeftButtonHeld)
        {
            _leftController.GetComponentInChildren<XRRayInteractor>().enabled = false;
        }
        if (_inputManager.IsRightButtonHeld)
        {
            _rightController.GetComponentInChildren<XRRayInteractor>().enabled = false;
        }
        _mainMenu.SetActive(true);

    }

    private async void OnButtonReleased(InputAction.CallbackContext context)
    {

        if (!_inputManager.IsLeftButtonHeld && _inputManager.IsRightButtonHeld)
        {
            _leftController.GetComponentInChildren<XRRayInteractor>().enabled = true;
            _rightController.GetComponentInChildren<XRRayInteractor>().enabled = false;
        }
        else if (!_inputManager.IsRightButtonHeld && _inputManager.IsLeftButtonHeld)
        {
            _rightController.GetComponentInChildren<XRRayInteractor>().enabled = true;
            _leftController.GetComponentInChildren<XRRayInteractor>().enabled = false;
            await System.Threading.Tasks.Task.Delay(5000);
        }
        else
        {
            _leftController.GetComponentInChildren<XRRayInteractor>().enabled = true;
            _rightController.GetComponentInChildren<XRRayInteractor>().enabled = true;
        }

    }

    public void ExitPlay()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Exited Play Mode");
#else
        Application.Quit();
        Debug.Log("Exited App");
#endif
    }

    public void OnDestroy()
    {
        _inputManager.OnButtonPressed -= OnButtonPressed;
        _inputManager.OnButtonReleased -= OnButtonReleased;
    }

#region Offset Calculation
    /*public Transform controllerReference;
    public Transform menuReference;
    public Vector3 posOffset;

    [ContextMenu("GetOffset")]
    public void GetOffset()
    {
        posOffset = controllerReference.position - menuReference.position;
    }*/
#endregion
}
