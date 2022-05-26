using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperControl : MonoBehaviour
{
    public bool sendPaperToTable = true;
    bool paperFinish = false;

    public List<GameObject> paperList = new List<GameObject>();
    
    public GameObject[] damgaliPaperList;


    public GameObject spawnPaperPosition;
    public int totalPoint = 0;
    public int totalPointFake = 0;
    public int spawnPaperNumber; //ne kadar ka��t spawn olacak onu belirliyor
    public int spawnPaperTower;

    public Text totalPointText;


    public float paperMoveSpeed = 1;
    public GameObject paperObje;
    public GameObject paperObje2;
    public GameObject paperObje3;
    public GameObject dolarAnim;
    public GameObject sekreter;

    int totalMoney;
    public int anlikKazanc;
    int paperSiraNumber = 30;  // KAGITLARIN KA�AR TANE �ST �STE D�Z�LECE��N� BURAYA G�R�YORUZ.
    int damgaliPaperTransform = 0;
    public int dolarMiktari;
    int gameLevel = 1;
    public int currentPaperNumber = 0;
    public int damgaPaperSayisi = 0;

    int geriyeKalanPaperNumber;
    GameObject Table;
    GameObject Damga;
    GameObject CompletedTable;
    GameObject PlayerController;
    GameObject UIController;

     void Awake()
    {
        if (damgaliPaperList.Length <= 0)
        {
            PlayerPrefs.SetInt("totalPointFake" , 0);
            totalPointFake = 0;
        }

        if (gameLevel == 1)
        {
            spawnPaperNumber = 1000;  // KA� KA�IT SPAWN ED�LECE�� BURAYA

            //spawnPaperTower = spawnPaperNumber;
            PlayerPrefs.SetInt("spawnPaperNumber", spawnPaperNumber);
        }

        else if (gameLevel == 2)
        {
            spawnPaperNumber = 1500;  // KA� KA�IT SPAWN ED�LECE�� BURAYA

            //spawnPaperTower = spawnPaperNumber;
            PlayerPrefs.SetInt("spawnPaperNumber", spawnPaperNumber);
        }

        else if (gameLevel == 3)
        {
            spawnPaperNumber = 2500;  // KA� KA�IT SPAWN ED�LECE�� BURAYA

            //spawnPaperTower = spawnPaperNumber;
            PlayerPrefs.SetInt("spawnPaperNumber", spawnPaperNumber);
        }
    }
    void Start()
    {
    
        Table = GameObject.FindGameObjectWithTag("table");
        Damga = GameObject.FindGameObjectWithTag("damga");
        CompletedTable = GameObject.FindGameObjectWithTag("completedTable");
        UIController = GameObject.FindGameObjectWithTag("UIController");
       
       
        if (PlayerPrefs.GetInt("dolarMiktari")<=15)
        {
            dolarMiktari = 15;
            PlayerPrefs.SetInt("dolarMiktari", dolarMiktari);
        }

       

      
            
       

        // spawnPaperFunc();
    }

    // Update is called once per frame
    void Update()
    {
        damgaliPaperList = GameObject.FindGameObjectsWithTag("damgaVar");

        if (spawnPaperNumber<=0)
        {
            Debug.Log("OYUN B�TT�");
        }

        for (var i = paperList.Count - 1; i > -1; i--)
        {
            if (paperList[i] == null)
                paperList.RemoveAt(i);
        }

        if (paperMoveSpeed >= 1f)
        {
            paperMoveSpeed = 1f;
        }

        totalPointText.text = PlayerPrefs.GetInt("totalPoint") + "";


        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {

           
       
        dolarAnim.GetComponent<TextMesh>().text = "$" + dolarMiktari; // Para animasyonu ka� olacaksa buraya yaz�yoruz
           



        SendMainTable();   
        }
        else
        {
            PlayerPrefs.SetInt("dolarMiktari" ,dolarMiktari);
        }
      
    }

    void SendMainTable()
    {
        if (sendPaperToTable)  //Ana masaya giden kod
        {

            paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x, paperList[currentPaperNumber].transform.position.y, Table.transform.position.z), paperMoveSpeed).OnComplete(() => { paperList[currentPaperNumber].transform.DOMove(new Vector3(Table.transform.position.x,Table.transform.position.y+0.1f,Table.transform.position.z), paperMoveSpeed).OnComplete(() => Damga.GetComponent<DamgaControl>().canDamga = true); });

            sendPaperToTable = false;

        }
    }
    public void MoveCompleteTable() //Tamamland�ktan sonra gitti�i masa kodu
    {

 
     
        damgaPaperSayisi++;
        Instantiate(dolarAnim, paperList[currentPaperNumber].transform.position, Quaternion.identity);
        totalMoney += dolarMiktari;
        anlikKazanc += dolarMiktari;
        PlayerPrefs.SetInt("totalMoney", totalMoney); 
        PlayerPrefs.SetInt("anlikKazanc", anlikKazanc); 
        //paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, CompletedTable.transform.position.y + damgaliPaperList.Length / 10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(() => { sendPaperToTable = true; currentPaperNumber++; });
        //paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, CompletedTable.transform.position.y + totalPointFake / 10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(() => { sendPaperToTable = true; currentPaperNumber++; });
        totalPoint++;
        PlayerPrefs.SetInt("totalPoint", totalPoint);
        totalPointFake++;
        paperList[currentPaperNumber].transform.DOMove(new Vector3(CompletedTable.transform.position.x, CompletedTable.transform.position.y + totalPointFake / 10f, CompletedTable.transform.position.z), paperMoveSpeed).OnComplete(() => { 
            sendPaperToTable = true;
            currentPaperNumber++;
            spawnPaperNumber--;
            PlayerPrefs.SetInt("spawnPaperNumber", spawnPaperNumber);

            if (damgaliPaperList.Length >= paperSiraNumber && totalPointFake >= paperSiraNumber)
            {
                for (int i = 0; i < damgaliPaperList.Length; i++)
                {//SEKTER ALSIN DİYE KAYDIRILANLAR
                    damgaliPaperList[i].transform.DOJump(new Vector3(damgaliPaperList[i].transform.position.x, damgaliPaperList[i].transform.position.y, damgaliPaperList[i].transform.position.z + 2.7f), 1, 1, paperMoveSpeed).OnComplete(() => {

                        Damga.GetComponent<PlayerController>().startGame = true;
                       
                        
                        for (int a = 0; a < damgaliPaperList.Length; a++)
                        {
                            paperList.Remove(damgaliPaperList[a]);
                        }
                        currentPaperNumber = 0;
                        Instantiate(sekreter, new Vector3(19, -10, 23), Quaternion.identity);


                    });

                }
                totalPointFake = 0;
            }
            damgaliPaperTransform++;
        });
        PlayerPrefs.SetInt("totalPointFake", totalPointFake);
        if (damgaPaperSayisi == paperSiraNumber)
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
        yield return new WaitForSecondsRealtime(paperMoveSpeed);
        
      
            for (int i = 0; i < paperList.Count; i++)
            {
            damgaliPaperList = GameObject.FindGameObjectsWithTag("damgaVar");
    
            if (paperList[i].tag == "damgaYok")  // DAMGASIZLARIN HAREKET� BURADA
            {
            
                    paperList[i].transform.DOJump(new Vector3(paperList[i].transform.position.x + 3f, paperList[i].transform.position.y, paperList[i].transform.position.z), 1, 1, paperMoveSpeed).OnComplete(() => {
                        Damga.GetComponent<PlayerController>().startGame = true;
                        


                    });
              
               
          
           
           
        
              
            }
            else  // DAMGALILARIN HAREKET� BURADA
            {
                /*
                damgaliPaperList[i].transform.DOJump(new Vector3(damgaliPaperList[i].transform.position.x, damgaliPaperList[i].transform.position.y, damgaliPaperList[i].transform.position.z+2.7f), 1, 1, paperMoveSpeed).OnComplete(() => {
                   
                    Damga.GetComponent<PlayerController>().startGame = true;
                    for (int a = 0; a < damgaliPaperList.Length; a++)
                    {
                        paperList.Remove(damgaliPaperList[a]);
                    }
                });
              
                */
            }
          
            }
     
       
        //Instantiate(sekreter, new Vector3(19, -10, 23), Quaternion.identity);
    }

    public void DamgaliKagitlarSekretere()
    {
        for (int i = 0; i < paperSiraNumber; i++)
        {
            damgaliPaperList[i].transform.parent = GameObject.FindGameObjectWithTag("stackPaperPoint").transform;
          
            damgaliPaperList[i].tag = "Untagged";
        }

        

    }

 
    public void spawnPaperFunc()
    {
    
        for (int a = 0; a < (spawnPaperNumber / paperSiraNumber) +2; a++)
        {
          

            if (spawnPaperTower > paperSiraNumber)
            {

                for (int i = paperSiraNumber; i > 0; i--)
                {
                    RandomPaperChoose();
                    var spawnedPaper = Instantiate(paperObje, new Vector3(spawnPaperPosition.transform.position.x-a*2.2f, (i-22) / 20f, spawnPaperPosition.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)));
                    paperList.Add(spawnedPaper);
                    spawnPaperTower--;
                
                }
            }
        else if(spawnPaperTower < paperSiraNumber)
        {
            for (int i = spawnPaperTower; i > 0; i--)
            {
                    RandomPaperChoose();
                var spawnedPaper = Instantiate(paperObje, new Vector3(spawnPaperPosition.transform.position.x -a*2.2f, (i - 22) / 20f, spawnPaperPosition.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)));
                paperList.Add(spawnedPaper);
                    spawnPaperTower--;

            }
        }

        }
    }

    void RandomPaperChoose()
    {
        var RandomPaperNumber = Random.RandomRange(0, 3);

        if (RandomPaperNumber == 0)
        {
            paperObje = paperObje2;
        }

        else if (RandomPaperNumber == 1)
        {
            paperObje = paperObje3;
        }

        else if(RandomPaperNumber==2)
        {
            paperObje = paperObje;
        }
    }
    public void DeletePapers()
    {
        for (int i = 0; i < paperList.Count; i++)
        {
     

            if (paperList[i].gameObject.tag == "damgaYok")
            {
                Destroy(paperList[i]);
            }

        }
        paperList.Clear();


 
    }
}
