using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperControl : MonoBehaviour
{
    bool sendPaperToTable = true;
    public List<GameObject> paperList = new List<GameObject>();

    int currentPaperNumber = 0;
    GameObject Table;
    GameObject Damga;
    GameObject CompletedTable;
    void Start()
    {
        Table = GameObject.FindGameObjectWithTag("table");
        Damga = GameObject.FindGameObjectWithTag("damga");
        CompletedTable = GameObject.FindGameObjectWithTag("completedTable");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentPaperNumber);
        if (sendPaperToTable)
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x,0,Table.transform.position.z),1).OnComplete(()=> { Damga.GetComponent<DamgaControl>().canDamga = true; });
          
            sendPaperToTable = false;

        }


        if (paperList[currentPaperNumber].gameObject.tag == "damgaVar")
        {

            StartCoroutine(MoveCompleteTable());
        }
    
    }
    IEnumerator MoveCompleteTable()
    {
        paperList[currentPaperNumber].gameObject.tag = "damgaYok";
        yield return new WaitForSeconds(1);
        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, 0, CompletedTable.transform.position.z), 1).OnComplete(()=> { sendPaperToTable = true; paperList.Remove(paperList[currentPaperNumber]); });


    }
}
