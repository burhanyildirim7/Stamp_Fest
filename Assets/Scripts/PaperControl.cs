using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PaperControl : MonoBehaviour
{
    public bool sendPaperToTable = true;
    bool paperFinish = false;

    public List<GameObject> paperList = new List<GameObject>();
    
    public GameObject[] damgaliPaperList;



    public int totalPoint = 0;
    public int totalPointFake = 0;
    public int spawnPaperNumber; //ne kadar kaðýt spawn olacak onu belirliyor
    public int spawnPaperTower;

    public Text totalPointText;

    public float paperMoveSpeed = 1;
    public GameObject paperObje;
    public GameObject dolarAnim;
    public int dolarMiktarý;
    public int currentPaperNumber = 0;
    public int damgaPaperSayisi = 0;

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

       // spawnPaperFunc();
    }

    // Update is called once per frame
    void Update()
    {
        if (paperMoveSpeed >= 1f)
        {
            paperMoveSpeed = 1f;
        }

        totalPointText.text = totalPoint + "";
        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {

           
       
        dolarAnim.GetComponent<TextMesh>().text = "$" + dolarMiktarý; // Para animasyonu kaç olacaksa buraya yazýyoruz
      
        SendMainTable();   
        }
      
    }

    void SendMainTable()
    {
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, paperList[currentPaperNumber].transform.position.y, Table.transform.position.z), paperMoveSpeed).OnComplete(() => { paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x,Table.transform.position.y+0.5f,Table.transform.position.z), paperMoveSpeed).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true); });

            sendPaperToTable = false;

        }
    }
    public void MoveCompleteTable() //Tamamlandýktan sonra gittiði masa kodu
    {

 
     
        damgaPaperSayisi++;
        Instantiate(dolarAnim, paperList[currentPaperNumber].transform.position, Quaternion.identity);
        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, CompletedTable.transform.position.y + totalPointFake / 10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(() => { sendPaperToTable = true; currentPaperNumber++; });
        totalPoint++;
        totalPointFake++;
        if (damgaPaperSayisi == 100)
        {
            StartCoroutine(Swerve());
        }
    }


    IEnumerator Swerve()
    {
        damgaPaperSayisi = 0;
        Time.timeScale = 1;

        paperMoveSpeed = Mathf.Clamp(paperMoveSpeed,0.1f,1);

        Damga.GetComponent<PlayerController>().startGame = false;
        yield return new WaitForSecondsRealtime(1.5f);
        
        totalPointFake = 0;
            for (int i = 0; i < paperList.Count; i++)
            {
            damgaliPaperList = GameObject.FindGameObjectsWithTag("damgaVar");

            if (paperList[i].tag == "damgaYok")  // DAMGASIZLARIN HAREKETÝ BURADA
            {
                paperList[i].transform.DOJump(new Vector3(paperList[i].transform.position.x + 1.8f, paperList[i].transform.position.y, paperList[i].transform.position.z), 1, 1, 0.5f).OnComplete(() => {
                    Damga.GetComponent<PlayerController>().startGame = true;
                });
            }
            else  // DAMGALILARIN HAREKETÝ BURADA
            {
                damgaliPaperList[i].transform.DOJump(new Vector3(damgaliPaperList[i].transform.position.x, damgaliPaperList[i].transform.position.y, damgaliPaperList[i].transform.position.z+2.7f), 1, 1, 0.5f).OnComplete(() => {
                    Damga.GetComponent<PlayerController>().startGame = true;
                });
            }
          
            }
      

    }
    public void spawnPaperFunc()
    {
     
        for (int a = 1; a < (spawnPaperNumber / 100)+2; a++)
        {

            if (spawnPaperTower > 100)
            {
                for (int i = 100; i > 0; i--)
                {
                    var spawnedPaper = Instantiate(paperObje, new Vector3(-1f*a*2.5f, (i-10) / 10f, 3.5f), Quaternion.identity);
                    paperList.Add(spawnedPaper);
                    spawnPaperTower--;
                
                }
            }
        else if(spawnPaperTower < 100)
        {
            for (int i = spawnPaperTower; i > 0; i--)
            {
                var spawnedPaper = Instantiate(paperObje, new Vector3(-1f*a*2.5f, (i-10) / 10f, 3.5f), Quaternion.identity);
                paperList.Add(spawnedPaper);
                    spawnPaperTower--;

            }
        }

        }
    }

    public void DeletePapers()
    {
        for (int i = 0; i < paperList.Count; i++)
        {
            Destroy(paperList[i]);
        
          

        }
        paperList.Clear();


 
    }
}
