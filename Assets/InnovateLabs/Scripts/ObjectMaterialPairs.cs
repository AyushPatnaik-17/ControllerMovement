using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectMaterialPair
{
    public GameObject Part;
    public MeshRenderer MeshRen;
    public List<Material> Materials;

    public ObjectMaterialPair(GameObject part, MeshRenderer meshRen, List<Material> materials)
    {
        Part = part;
        MeshRen = meshRen;
        Materials = materials;
    }
    public void SetDefaultMaterials()
    {
        MeshRen.materials = Materials.ToArray();
    }

    public void SetMaterial(Material material)
    {
        Material[] newMaterials = new Material[MeshRen.sharedMaterials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = material;
        }
        MeshRen.materials = newMaterials;
    }
}
[System.Serializable]
public struct PipeStructures
{
    public GameObject WaterPipes;
    public GameObject GasPipes;
    public GameObject SewerPipes;
    public GameObject Comms;

    public List<GameObject> CityInfoCards;

    public void ToggleAll(bool toggle)
    {
        WaterPipes.SetActive(toggle);
        GasPipes.SetActive(toggle);
        SewerPipes.SetActive(toggle);
        Comms.SetActive(toggle);
    }

    public void ToggleThis(GameObject structure, bool showCards = false)
    {
        ToggleAll(false);
        structure.SetActive(true);
        int c = CityInfoCards.Count;
        if (c == 0) return;
        for(int i = 0; i < c; i++)
        {
            CityInfoCards[i].SetActive(showCards);
        }
        
    }
} 
