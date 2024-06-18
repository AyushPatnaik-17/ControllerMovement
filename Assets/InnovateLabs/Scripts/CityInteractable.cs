using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CityInteractable : MonoBehaviour
{
    //private XRSimpleInteractable Interactable;
    private Button Interactable;
    public GameObject InfoCard;
    private bool _isSelected = false;

    private void OnEnable()
    {
        //Interactable = GetComponent<XRSimpleInteractable>();
        Interactable = GetComponent<Button>();
        AddListeners();
    }

    private void AddListeners()
    {
        Interactable.onClick.AddListener(delegate
        {
            InfoCard.SetActive(!InfoCard.activeSelf);
        });
        //Interactable.hoverEntered.AddListener(delegate
        //{
        //    Debug.Log("Hover Entered");
        //    if (_isSelected == true) return;
        //    HandleSelectionState(show: true);
        //});
        //Interactable.hoverExited.AddListener(delegate
        //{
        //    Debug.Log("Hover exited");
        //    if (_isSelected == true) return;
        //    HandleSelectionState(show: false);
        //});
        //Interactable.selectEntered.AddListener(delegate
        //{
        //    Debug.Log("select Entered");
        //    _isSelected = !_isSelected;
        //    HandleSelectionState(show: _isSelected);
        //});

    }

    //private void HandleSelectionState(bool show)
    //{
    //    InfoCard.SetActive(show);
    //}
}
