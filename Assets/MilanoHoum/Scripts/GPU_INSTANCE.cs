using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU_INSTANCE : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
       
}
