using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InCityHandler : MonoBehaviour
{
    [SerializeField] private List<ObjectMaterialPair> _objMatPairs = new();
    private CityUIHandler _cityUIHandler;
    public List<GameObject> AssociatedParts = new();
    public Material Shader;

    public Button CityViewBtn;
    public Button AllStructsBtn;
    public Button WaterBtn;
    public Button GasBtn;
    public Button SewageBtn;
    public Button CommsBtn;

    public List<ObjectMaterialPair> ObjMatPairs { get => _objMatPairs; set => _objMatPairs = value; }
    private int _partCount = 0;
    private void Awake()
    {
        _partCount = AssociatedParts.Count;
        for (int i = 0; i < _partCount; i++)
        {
            ObjectMaterialPair objMatPair = new ObjectMaterialPair();
            objMatPair.Part = AssociatedParts[i];
            MeshRenderer meshRen = objMatPair.MeshRen = AssociatedParts[i].GetComponent<MeshRenderer>();
            objMatPair.Materials = meshRen.materials.ToList();
            _objMatPairs.Add(objMatPair);
        }
        _cityUIHandler = new CityUIHandler(this, CityViewBtn, AllStructsBtn, WaterBtn, GasBtn, 
                                            SewageBtn, CommsBtn, _objMatPairs);          
    }

    public void AddMaterialToObjects(out bool hasMatChanged)
    {
        int count = _objMatPairs.Count;
        Debug.Log("Are we even reaching here");
        Debug.Log("Count :" + count);

        for (int i = 0; i < count; i++)
        {

            Debug.Log($"Part name : {_objMatPairs[i].Part.name}");
            _objMatPairs[i].SetMaterial(Shader);
        }
        hasMatChanged = true;
    }
}

public class CityUIHandler
{
    private InCityHandler _inCityHandler;

    public Button CityViewBtn;
    private Button _allStructsBtn;
    private Button _waterBtn;
    private Button _gasBtn;
    private Button _sewageBtn;
    private Button _commsBtn;

    private List<ObjectMaterialPair> _objMatPairs;
    private StructureHandler _structureHandler;
    public bool HasMatChanged = false;

    public CityUIHandler(InCityHandler inCityHandler, Button cityViewBtn, Button allStructsBtn, Button waterBtn,
                                    Button gasBtn, Button sewageBtn, Button commsBtn, List<ObjectMaterialPair> matPairs)
    {
        _inCityHandler = inCityHandler;
        CityViewBtn = cityViewBtn;
        _allStructsBtn = allStructsBtn;
        _waterBtn = waterBtn;
        _gasBtn = gasBtn;
        _sewageBtn = sewageBtn;
        _commsBtn = commsBtn;
        _objMatPairs = matPairs;

        _structureHandler = MonoBehaviour.FindObjectOfType<StructureHandler>();

        AddListeners();
    }

    private void AddListeners()
    {
        int count = _objMatPairs.Count;
        Debug.Log("Count fromt the listeners section : " + count);
        var pipes = _structureHandler.CityPipeStructures;
        CityViewBtn.onClick.AddListener(delegate
        {
            for (int i = 0; i < count; i++)
            {
                _objMatPairs[i].SetDefaultMaterials();
            }
            HasMatChanged = false;

        });
        _allStructsBtn.onClick.AddListener(delegate
        {
            pipes.ToggleAll(true);
            Debug.Log($"Has mat changed?: {HasMatChanged}");
            if (HasMatChanged == true) return;
            _inCityHandler.AddMaterialToObjects(out HasMatChanged);

        });
        _waterBtn.onClick.AddListener(delegate
        {
            pipes.ToggleThis(pipes.WaterPipes, true);
            if (HasMatChanged == true) return;
            _inCityHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _gasBtn.onClick.AddListener(delegate
        {
            pipes.ToggleThis(pipes.GasPipes);
            if (HasMatChanged == true) return;
            _inCityHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _sewageBtn.onClick.AddListener(delegate
        {
            pipes.ToggleThis(pipes.SewerPipes);
            if (HasMatChanged == true) return;
            _inCityHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _commsBtn.onClick.AddListener(delegate
        {
            pipes.ToggleThis(pipes.Comms);
            if (HasMatChanged == true) return;
            _inCityHandler.AddMaterialToObjects(out HasMatChanged);
        });
    }
}
