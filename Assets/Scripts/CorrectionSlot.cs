using UnityEngine;

public class CorrectionSlot : MonoBehaviour
{
    private Material color;
    private MeshRenderer meshRenderer;
    void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update(){
        meshRenderer.material = color;
    }
    public void SetColor(Material material){
        color = material;
    }
    public Material GetColor(){
        return color;
    }
}
