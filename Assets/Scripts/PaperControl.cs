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
    public List<GameObject> paperList1 = new List<GameObject>();
    public List<GameObject> paperList2 = new List<GameObject>();


    public int totalPoint = 0;
    public int spawnPaperNumber; //ne kadar kaðýt spawn olacak onu belirliyor
    int spawnPaperTower;

    public Text totalPointText;

    public float paperMoveSpeed = 1;
    public GameObject paperObje;
    public GameObject dolarAnim;
    public int dolarMiktarý;
    public int currentPaperNumber = 0;
    int damgaPaperSayisi = 0;

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
        UIController = GameObject.FindGameObjectWithTag("UIController");
       
        spawnPaperTower = spawnPaperNumber;

        spawnPaperFunc();
    }

    // Update is called once per frame
    void Update()
    {

      

        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {

        
       
        dolarAnim.GetComponent<TextMesh>().text = "$" + dolarMiktarý; // Para animasyonu kaç olacaksa buraya yazýyoruz


       
     

        totalPointText.text = totalPoint +"";

        SendMainTable();


        if (paperList[currentPaperNumber].gameObject.tag == "damgaVar")
        {
            Instantiate(dolarAnim,paperList[currentPaperNumber].transform.position,Quaternion.identity);
           
            MoveCompleteTable();
            totalPoint++;
        }
        }
    }

    void SendMainTable()
    {
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, paperList[currentPaperNumber].transform.position.y, paperList[currentPaperNumber].transform.position.z), paperMoveSpeed).OnComplete(() => { paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, 0, Table.transform.position.z), paperMoveSpeed).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true); });

            sendPaperToTable = false;

        }
    }
    void MoveCompleteTable() //Tamamlandýktan sonra gittiði masa kodu
    {
        paperList[currentPaperNumber].gameObject.tag = "damgaYok";

        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x,totalPoint/10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(()=> { sendPaperToTable = true; currentPaperNumber++; damgaPaperSayisi++; });

        if (damgaPaperSayisi == 99 )
        {
            StartCoroutine(Swerve());
        }
    }

    IEnumerator Swerve()
    {
        damgaPaperSayisi = 0;
        Time.timeScale = 1;
        Damga.GetComponent<PlayerController>().startGame =false;
        yield return new WaitForSecondsRealtime(1.5f);
        for (int i = 0; i < paperList.Count; i++)
        {
            paperList[i].transform.DOMoveX(paperList[i].transform.position.x+1.8f, 2).OnComplete(()=> { Damga.GetComponent<PlayerController>().startGame = true; });
        }
      

    }
    void spawnPaperFunc()
    {
        /*
          if (paperList.Count <=100)
          {     
          for (int i = spawnPaperNumber; i > 0; i--)
          {        
                  var spawnedPaper = Instantiate(paperObje, new Vector3(-1.8f, i / 10f, 0), Quaternion.identity);
                  paperList.Add(spawnedPaper);

                  if (i<=spawnPaperNumber-100)
                  {
                      var spawnedPaper1 = Instantiate(paperObje, new Vector3(-3.2f, i / 10f, 0), Quaternion.identity);
                      paperList.Add(spawnedPaper1);
                  }
          }

          }
          */

        for (int a = 1; a < (spawnPaperNumber / 100)+2; a++)
        {

            if (spawnPaperTower > 100)
            {
                for (int i = 100; i > 0; i--)
                {
                    var spawnedPaper = Instantiate(paperObje, new Vector3(-1.8f*a, i / 10f, 0), Quaternion.identity);
                    paperList.Add(spawnedPaper);
                    spawnPaperTower--;
                
                }
            }
        else if(spawnPaperTower < 100)
        {
            Debug.Log("Çalýþýyor");
            for (int i = spawnPaperTower; i > 0; i--)
            {
                var spawnedPaper = Instantiate(paperObje, new Vector3(-1.8f*a, i / 10f, 0), Quaternion.identity);
                paperList.Add(spawnedPaper);
                    spawnPaperTower--;

            }
        }

        }
    }
}
