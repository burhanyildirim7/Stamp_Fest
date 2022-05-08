using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PaperControl : MonoBehaviour
{
    bool sendPaperToTable = true;
    bool paperFinish = false;

    public List<GameObject> paperList = new List<GameObject>();

    int currentPaperNumber = 0;
    public int totalPoint = 0;
     int spawnPaperNumber = 15; //ne kadar kaðýt spawn olacak onu belirliyor

    public Text totalPointText;

    public GameObject paperObje;
    public GameObject dolarAnim;
    public int dolarMiktarý;

    GameObject Table;
    GameObject Damga;
    GameObject CompletedTable;
    GameObject PlayerController;
    GameObject UIController;
    void Start()
    {
        Table = GameObject.FindGameObjectWithTag("table");
        Damga = GameObject.FindGameObjectWithTag("damga");
        CompletedTable = GameObject.FindGameObjectWithTag("completedTable");
        PlayerController = GameObject.FindGameObjectWithTag("PlayerController");
        UIController = GameObject.FindGameObjectWithTag("UIController");
             

        spawnPaperFunc();
    }

    // Update is called once per frame
    void Update()
    {
       
       
        dolarAnim.GetComponent<TextMesh>().text = "$" + dolarMiktarý; // Para animasyonu kaç olacaksa buraya yazýyoruz


        if (currentPaperNumber == paperList.Count)
        {
            if (paperFinish == false)
            {
                spawnPaperFunc();
                paperFinish = true;
            }
        
        }
        else
        {
            paperFinish = false;
        }

        totalPointText.text = totalPoint +"";

        SendMainTable();


        if (paperList[currentPaperNumber].gameObject.tag == "damgaVar")
        {
            Instantiate(dolarAnim,paperList[currentPaperNumber].transform.position,Quaternion.identity);
           
            StartCoroutine(MoveCompleteTable());
            totalPoint++;
        }
      
    }

    void SendMainTable()
    {
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, paperList[currentPaperNumber].transform.position.y, paperList[currentPaperNumber].transform.position.z), 1).OnComplete(() => { paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, 0, Table.transform.position.z), 1).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true); });

            sendPaperToTable = false;

        }
    }
    IEnumerator MoveCompleteTable() //Tamamlandýktan sonra gittiði masa kodu
    {
        paperList[currentPaperNumber].gameObject.tag = "damgaYok";
        yield return new WaitForSeconds(0.5f);
        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, 0, CompletedTable.transform.position.z), 1).OnComplete(()=> { sendPaperToTable = true; paperList.Remove(paperList[currentPaperNumber]); });


    }

    void spawnPaperFunc()
    {
        for (int i = spawnPaperNumber; i > 0; i--)
        {
            var spawnedPaper = Instantiate(paperObje, new Vector3(-2, i / 10f, 0), Quaternion.identity);
            paperList.Add(spawnedPaper);

        }
    }
}
