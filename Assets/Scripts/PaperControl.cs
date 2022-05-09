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

 
    public int totalPoint = 0;
    public int spawnPaperNumber; //ne kadar kaðýt spawn olacak onu belirliyor

    public Text totalPointText;

    public float paperMoveSpeed = 1;
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
        UIController = GameObject.FindGameObjectWithTag("UIController");

 
        spawnPaperFunc();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.timeScale);
       

        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {

        
       
        dolarAnim.GetComponent<TextMesh>().text = "$" + dolarMiktarý; // Para animasyonu kaç olacaksa buraya yazýyoruz


        if (paperList.Count <= 50)
        {
            if (paperFinish == false)
            {
                    for (int i = 0; i < paperList.Count; i++)
                    {
                        paperList[i].transform.DOMoveX(-1.8f,2);
                    }
            }
        
        }
     

        totalPointText.text = totalPoint +"";

        SendMainTable();


        if (paperList[0].gameObject.tag == "damgaVar")
        {
            Instantiate(dolarAnim,paperList[0].transform.position,Quaternion.identity);
           
            StartCoroutine(MoveCompleteTable());
            totalPoint++;
        }
        }
    }

    void SendMainTable()
    {
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[0].transform.DOMove(new Vector3(Table.transform.position.x, paperList[0].transform.position.y, paperList[0].transform.position.z), paperMoveSpeed).OnComplete(() => { paperList[0].transform.DOMove(new Vector3(Table.transform.position.x, 0, Table.transform.position.z), paperMoveSpeed).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true); });

            sendPaperToTable = false;

        }
    }
    IEnumerator MoveCompleteTable() //Tamamlandýktan sonra gittiði masa kodu
    {
        paperList[0].gameObject.tag = "damgaYok";
        yield return new WaitForSeconds(0);
        paperList[0].transform.DOMove(new Vector3(CompletedTable.transform.position.x,totalPoint/10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(()=> { sendPaperToTable = true; paperList.Remove(paperList[0]); });
       

    }

    void spawnPaperFunc()
    {
      
        if (paperList.Count <=100)
        {     
        for (int i = spawnPaperNumber; i > 0; i--)
        {        
                var spawnedPaper = Instantiate(paperObje, new Vector3(-1.8f, i / 10f, 0), Quaternion.identity);
                paperList.Add(spawnedPaper);

                if (i<=spawnPaperNumber-100)
                {
                    var spawnedPaper1 = Instantiate(paperObje, new Vector3(-3.2f, i / 10f, 0), Quaternion.identity);
                    paperList.Add(spawnedPaper);
                }
        }

        }
    


    
    }
}
