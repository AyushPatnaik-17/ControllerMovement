using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using MixedReality.Toolkit;
using UnityEngine.UI;

public enum Modes
{
    Mini,
    Scaled
}
public class AreaStateHandler : MonoBehaviour
{

    private XRSimpleInteractable _simpleInteractable;
    [SerializeField] private List<AreaStateHandler> StateHandlers = new();
    private StatefulInteractable _statefulInteractable;
    private Outline _outline;
    [SerializeField] private List<ObjectMaterialPair> _objMatPairs = new();
    private InfoCardUIHandler _infoCardUIHandler;

    public List<GameObject> AssociatedParts = new();
    public GameObject InfoCard;
    public Material Shader;

    public Button CityViewBtn;
    public Button AllStructsBtn;
    public Button WaterBtn;
    public Button GasBtn;
    public Button SewageBtn;
    public Button CommsBtn;

    public List<ObjectMaterialPair> ObjMatPairs { get => _objMatPairs; set => _objMatPairs = value; }
    public Outline Outline { get => _outline; set => _outline = value; }

    private bool _isSelected = false;
    private int _partCount = 0;

    private void Awake()
    {
        _simpleInteractable = GetComponent<XRSimpleInteractable>();
        //_statefulInteractable = GetComponent<StatefulInteractable>();
        _outline = transform.parent.GetComponent<Outline>();
        AddListeners();
        _partCount = AssociatedParts.Count;
        for (int i = 0; i < _partCount; i++)
        {
            ObjectMaterialPair objMatPair = new ObjectMaterialPair();
            objMatPair.Part = AssociatedParts[i];
            MeshRenderer meshRen = objMatPair.MeshRen = AssociatedParts[i].GetComponent<MeshRenderer>();
            objMatPair.Materials = meshRen.materials.ToList();
            _objMatPairs.Add(objMatPair);
        }
        _infoCardUIHandler = new InfoCardUIHandler(this, CityViewBtn, AllStructsBtn, WaterBtn,
                                                    GasBtn, SewageBtn, CommsBtn, _objMatPairs);
    }

    private void AddListeners()
    {
        _simpleInteractable.hoverEntered.AddListener(delegate
        {
            if (_isSelected == true) return;
            HandleSelectionState(show: true);
        });
        _simpleInteractable.hoverExited.AddListener(delegate
        {
            if (_isSelected == true) return;
            HandleSelectionState(show: false);
        });
        _simpleInteractable.selectEntered.AddListener(delegate
        {
            _isSelected = !_isSelected;
            HandleSelectionState(show: _isSelected);
            if(_isSelected == false)
            {
                _infoCardUIHandler.CityViewBtn.onClick.Invoke();
                _infoCardUIHandler.HasMatChanged = false;
            }
            else
            {
                foreach (var interactor in StateHandlers)
                {
                    interactor.ResetState();
                }
            }
        });

    }

    public void ResetState()
    {
        _isSelected = false;
        InfoCard.SetActive(false);
        _outline.enabled = false;
        _infoCardUIHandler.HasMatChanged = false;
        int count = _objMatPairs.Count;
        for (int i = 0; i < count; i++)
        {
            _objMatPairs[i].SetDefaultMaterials();
        }
        StructureHandler structureHandler = FindObjectOfType<StructureHandler>();
        structureHandler.MiniaturePipeStructures.ToggleAll(true);
    }

    private void HandleSelectionState(bool show)
    {
        //if(_isSelected == true)
        //{
        //    _infoCardUIHandler.CityViewBtn.onClick.Invoke();
        //    _infoCardUIHandler.HasMatChanged = false;
        //}
        InfoCard.SetActive(show);
        _outline.enabled = show;
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

public class InfoCardUIHandler
{
    private AreaStateHandler _areaHandler;

    public Button CityViewBtn;
    private Button _allStructsBtn;
    private Button _waterBtn;
    private Button _gasBtn;
    private Button _sewageBtn;
    private Button _commsBtn;
    private Modes _currentMode;

    private List<ObjectMaterialPair> _objMatPairs;
    private StructureHandler _structureHandler;
    public bool HasMatChanged = false;
    public InfoCardUIHandler(AreaStateHandler areaStateHandler, Button cityViewBtn, Button allStructsBtn, Button waterBtn,
                                    Button gasBtn, Button sewageBtn, Button commsBtn, List<ObjectMaterialPair> matPairs)
    {
        _areaHandler = areaStateHandler;
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
        var miniPipes = _structureHandler.MiniaturePipeStructures;
        CityViewBtn.onClick.AddListener(delegate
        {
            for (int i = 0; i < count; i++)
            {
                _objMatPairs[i].SetDefaultMaterials();
            }
            HasMatChanged = false;
            _areaHandler.Outline.enabled = true;

        });
        _allStructsBtn.onClick.AddListener(delegate
        {
            _areaHandler.Outline.enabled = false;
            miniPipes.ToggleAll(true);
            Debug.Log($"Has mat changed?: {HasMatChanged}");
            if (HasMatChanged == true) return;
            _areaHandler.AddMaterialToObjects(out HasMatChanged);

        });
        _waterBtn.onClick.AddListener(delegate
        {
            miniPipes.ToggleThis(miniPipes.WaterPipes);
            if (HasMatChanged == true) return;
            _areaHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _gasBtn.onClick.AddListener(delegate
        {
            miniPipes.ToggleThis(miniPipes.GasPipes);
            if (HasMatChanged == true) return;
            _areaHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _sewageBtn.onClick.AddListener(delegate
        {
            miniPipes.ToggleThis(miniPipes.SewerPipes);
            if (HasMatChanged == true) return;
            _areaHandler.AddMaterialToObjects(out HasMatChanged);
        });
        _commsBtn.onClick.AddListener(delegate
        {
            miniPipes.ToggleThis(miniPipes.Comms);
            if (HasMatChanged == true) return;
            _areaHandler.AddMaterialToObjects(out HasMatChanged);
        });
    }
    
}
