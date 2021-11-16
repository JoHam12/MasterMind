using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private static int maxNumberRounds = 5;
    private int round;
    private int score;
    private Row[] guessRows = new Row[maxNumberRounds];
    [SerializeField] private Row activeRow;
    private Slot[] slotList = new Slot[5];
    private int activeSlotIndex = 0;
    private CorrectionRow activeCorrectionRow;
    [SerializeField] private GameObject wholeRowPrefab, wholeRowGobj;
    [SerializeField] private Material[] answerColors = new Material[5];
    [SerializeField] private Transform rowParent;
    private bool rowActivated = false;
    [SerializeField] private RawImage cursor;
    [SerializeField] private float y;
    private bool start = true;
    private bool foundCombination;

    void Start(){
        round = 0;
        score = 0;
        foundCombination = false;
    }
    void Update() {
        if(round == 0){ ChoseStartingCombination(); }
        if(!foundCombination && round < maxNumberRounds && Input.GetKeyDown(KeyCode.A) && !start){
            rowActivated = true;
            round += 1;
            SpawnChild(wholeRowPrefab, new Vector3(-round*.1f, .5f, 0), rowParent);
            guessRows = wholeRowGobj.GetComponentsInChildren<Row>();
            activeCorrectionRow = wholeRowGobj.GetComponentInChildren<CorrectionRow>();
            activeRow = guessRows[0];
            slotList = activeRow.GetComponentsInChildren<Slot>();
            
        }
        if (rowActivated){
            int i = 0;
            if(Input.GetKeyDown(KeyCode.UpArrow)){ i = 1; }
            if(Input.GetKeyDown(KeyCode.DownArrow)){ i = -1; }
            slotList[activeSlotIndex].activeColorIndex = (slotList[activeSlotIndex].activeColorIndex+i)%8;
            if(slotList[activeSlotIndex].activeColorIndex == -1){ slotList[activeSlotIndex].activeColorIndex = 7; }

            int j = 0;
            if(Input.GetKeyDown(KeyCode.RightArrow)){ j = 1; }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){j = -1; }
            activeSlotIndex = (activeSlotIndex+j)%5;
            if(activeSlotIndex == -1){ activeSlotIndex = 4; }

            cursor.rectTransform.localPosition = new Vector3(21.8f*activeSlotIndex-63, -85+y*(round-1)*2.5f, 0);
            slotList[activeSlotIndex].SetColor(activeRow.GetAllPossibleColors()[slotList[activeSlotIndex].activeColorIndex]);

        }
        if(round <= maxNumberRounds && Input.GetKeyDown(KeyCode.Space) && rowActivated && !start){
            string cor = activeRow.Compare(answerColors);
            activeCorrectionRow.SetCorrection(cor);
            activeCorrectionRow.SetCorrectionMaterial(activeCorrectionRow.GetCorrection());
            rowActivated = false;
            activeRow.ResetAlreadyUsed();
            if (cor.Equals("11111")){ 
                foundCombination = true;
                Debug.Log("Found Combination in " + round + " rounds!");
                return ;
            }
        }

    }
    private void SpawnChild(GameObject prefab, Vector3 relativePosition, Transform parent)
    {
        wholeRowGobj = Instantiate(prefab);
        wholeRowGobj.transform.parent = parent;
        wholeRowGobj.transform.localPosition = relativePosition;
        wholeRowGobj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        wholeRowGobj.transform.localScale = Vector3.one;
    }

    void ChoseStartingCombination(){
        slotList = activeRow.GetComponentsInChildren<Slot>();
        rowActivated = true;
        if(Input.GetKeyDown(KeyCode.Space)){
            start = false;
            rowActivated = false;
            round+=1;
            slotList = activeRow.GetComponentsInChildren<Slot>();
            answerColors = activeRow.GetRowColors();
            activeRow.ResetAlreadyUsed();
        }
    }
    public int GetMaxNumberRounds(){ return maxNumberRounds; }
    public int GetRound(){ return round; }
    public int GetScore(){ return score; }
    public Row GetActiveRow(){ return activeRow; }
}
