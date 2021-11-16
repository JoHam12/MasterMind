using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Material slotColor;
    [SerializeField] private Row parentRow;
    [SerializeField] private MeshRenderer mesh;
    public int activeColorIndex = 0;
    private void Awake() {
        mesh = GetComponent<MeshRenderer>();
        parentRow = GetComponentInParent<Row>();
        slotColor = parentRow.GetAllPossibleColors()[activeColorIndex];
    }
    // Update is called once per frame
    void Update()
    {   
        slotColor = parentRow.GetAllPossibleColors()[activeColorIndex];
        mesh.material = slotColor;
    }
    public Material getColor(){
        return slotColor;
    }
    public void SetColor(Material material){
        slotColor = material;
    }
}
