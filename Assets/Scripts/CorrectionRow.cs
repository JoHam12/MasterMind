using UnityEngine;

public class CorrectionRow : MonoBehaviour
{
    private CorrectionSlot[] slots = new CorrectionSlot[5]; 
    private Material[] slotsMats = new Material[5];
    private string correction;
    [SerializeField] private Row activeRow;
    [SerializeField] private Material[] answerMats = new Material[5];
    [SerializeField] private Material[] correctionMaterials = new Material[3];
    void Awake(){
        slots = GetComponentsInChildren<CorrectionSlot>();
    }
    public void SetAnswerMats(Material[] answer){
        answerMats = answer;
    }
    public void SetCorrection(string correction){
        this.correction = correction;
    }
    public string GetCorrection(){
        return correction;
    }
    public Material[] GetAnswerMats(){
        return answerMats;
    }

    public void SetCorrectionMaterial(string correction){
        for(int i = 0; i < correction.Length; i++){
            if(correction[i] == '1'){ 
                slotsMats[i] = correctionMaterials[1];
                
            }
            else if(correction[i] == '2'){
                slotsMats[i] = correctionMaterials[2];
            }
            else{
                slotsMats[i] = correctionMaterials[0];
            }
            slots[i].SetColor(slotsMats[i]);
        }
    }   
}