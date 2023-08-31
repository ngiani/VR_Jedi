using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner : MonoBehaviour
{
    [SerializeField] Material _outlineMaterial;
    [SerializeField] Color _outlineColor;

    // Start is called before the first frame update
    void Start()
    {
        //_outlineMaterial.SetColor("_EmissionColor", _outlineColor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowOutline()
    {
        _outlineMaterial.SetColor("_EmissionColor", _outlineColor);
        //_outlineMaterial.SetFloat("_EmissionIntensity", 1.5f);

        Debug.Log("OUTLINER SHOWN");
    }

    public void HideOutline()
    {
        _outlineMaterial.SetColor("_EmissionColor", Color.black);
        //_outlineMaterial.SetFloat("_EmissionIntensity", 0);

        Debug.Log("OUTLINER HID");
    }
}
