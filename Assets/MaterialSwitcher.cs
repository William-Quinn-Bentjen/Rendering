using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour {
    public List<Material> availableMaterials = new List<Material>();
    public MeshRenderer meshRenderer;
    private void Reset()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void SwitchMaterials(Material material)
    {
        meshRenderer.material = material;
    }
    public void SwitchMaterials(string materialName)
    {
        for (int i = 0; i < availableMaterials.Count; i++)
        {
            if (materialName == availableMaterials[i].name)
            {
                SwitchMaterials(availableMaterials[i]);
                break;
            }
        }
    }

}
