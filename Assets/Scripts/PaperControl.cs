using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PaperControl : MonoBehaviour
{
    bool sendPaperToTable = true;
    public List<GameObject> paperList = new List<GameObject>();

    int currentPaperNumber = 0;
    public int totalPoint = 0;
    public int spawnPaperNumber;

    public Text totalPointText;

    public GameObject paperObje;

    GameObject Table;
    GameObject Damga;
    GameObject CompletedTable;
    GameObject MainController;
    void Start()
    {
        Table = GameObject.FindGameObjectWithTag("table");
        Damga = GameObject.FindGameObjectWithTag("damga");
        CompletedTable = GameObject.FindGameObjectWithTag("completedTable");
        MainController = GameObject.FindGameObjectWithTag("MainController");

   

        for (int i = spawnPaperNumber; i > 0; i--)
        {
            var spawnedPaper = Instantiate(paperObje, new Vector3(-2, i / 10f, 0), Quaternion.identity);
            paperList.Add(spawnedPaper);

        }
    }

    // Update is called once per frame
    void Update()
    {

        totalPointText.text = totalPoint +"";
       
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, paperList[currentPaperNumber].transform.position.y, paperList[currentPaperNumber].transform.position.z), 1).OnComplete(() => { paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, 0, Table.transform.position.z),1).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true);  });
         
            sendPaperToTable = false;

        }


        if (paperList[currentPaperNumber].gameObject.tag == "damgaVar")
        {

            StartCoroutine(MoveCompleteTable());
            totalPoint++;
        }
        Debug.Log(totalPoint);
        
    }
    IEnumerator MoveCompleteTable() //Tamamlandýktan sonra gittiði masa kodu
    {
        paperList[currentPaperNumber].gameObject.tag = "damgaYok";
        yield return new WaitForSeconds(0.5f);
        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, 0, CompletedTable.transform.position.z), 1).OnComplete(()=> { sendPaperToTable = true; paperList.Remove(paperList[currentPaperNumber]); });


    }
}
