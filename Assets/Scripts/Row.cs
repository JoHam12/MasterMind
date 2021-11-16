using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private Slot[] slots = new Slot[5]; 
    [SerializeField] private Material[] slotsMats = new Material[5];
    private bool[] alreadyUsed_1 = new bool[5]{false, false, false, false, false};
    private bool[] alreadyUsed_2 = new bool[5]{false, false, false, false, false};
    [SerializeField] private Material[] allPossibleColors = new Material[8];

    private void Awake(){
        slots = GetComponentsInChildren<Slot>();
    }

    void Update() {
        for(int i = 0; i < slots.Length; i++){
            slotsMats[i] = slots[i].getColor();
        }
    }

    private bool isInAnswer(Material[] answer, Material slotGuess){
        for(int i = 0; i < answer.Length; i++){
            if(slotGuess.Equals(answer[i]) && !alreadyUsed_1[i]){ alreadyUsed_1[i]=true; return true; }
        }
        return false;
    }



    public string Compare(Material[] answer){
        string res = "";
        int sum1, sum2;
        sum1 = sum2 = 0;


        for(int i = 0; i < answer.Length; i++){
            if(answer[i].Equals(slotsMats[i])){ sum1 += 1; alreadyUsed_1[i]=true; alreadyUsed_2[i]=true;}
        }
        for(int i = 0; i < answer.Length; i++){
            if(!alreadyUsed_2[i] && isInAnswer(answer, slotsMats[i]) ){ sum2+=1; alreadyUsed_2[i]=true;}
        }

        for(int i = 0; i < sum1; i++){ res+="1"; }
        for(int i = 0; i < sum2; i++){ res+="2"; }
        for(int i = 0; i < 5-sum1-sum2; i++){ res+="0"; }
        
        return res;
    }
    public void SetRowColor(Material[] materials){
        slotsMats = materials;
    }
    public Material[] GetRowColors(){
        return slotsMats;
    }
    public void ResetAlreadyUsed(){
        alreadyUsed_1 = new bool[5]{false, false, false, false, false};
        alreadyUsed_2 = new bool[5]{false, false, false, false, false};
    }
    public Material[] GetAllPossibleColors(){
        return allPossibleColors;
    }
}
 