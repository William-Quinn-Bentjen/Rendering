using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Material_Switcher : MonoBehaviour {
    public Dropdown dropdown;
    public MaterialSwitcher materialSwitcher;
    public void ChangeMaterial()
    {
        materialSwitcher.SwitchMaterials(materialSwitcher.availableMaterials[dropdown.value]);
    }
}
