using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    // Member Variables
    [SerializeField] MeshRenderer meshRenderer;

    // Class Methods
    public void Show(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
