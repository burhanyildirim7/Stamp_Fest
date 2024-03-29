using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;
    public bool startGame = false;
    [SerializeField] Slider slider;

    public GameObject upgradeIncome;
    public GameObject upgradeStamina;
    public GameObject upgradeStamp;

    public GameObject damga;
    public GameObject paperController;
    public GameObject sekreter;
   //int gameLevel = 1;
    private void Awake()
    {
        if (instance == null) instance = this;

   

        //else Destroy(this);
    }

    void Start()
    {
        StartingEvents();

   
    }

    /// <summary>
    /// Playerin collider olaylari.. collectible, engel veya finish noktasi icin. Burasi artirilabilir.
    /// elmas icin veya baska herhangi etkilesimler icin tag ekleyerek kontrol dongusune eklenir.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku

        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
            if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
			{
                // FAİL EVENTLERİ BURAYA YAZILACAK..
                GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
			}


        }
        else if (other.CompareTag("finish")) 
        {
            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
            // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..

            
        }

    }

     void Update()
    {

        
    }
    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {
        GameObject paperControl = GameObject.FindGameObjectWithTag("PaperControl");

        
        upgradeIncome.GetComponent<UpgradeIncome>().incomeLevel = PlayerPrefs.GetInt("incomeLevel");
        upgradeStamina.GetComponent<UpgradeStamina>().staminaLevel = PlayerPrefs.GetInt("staminaLevel");
       
        upgradeStamp.GetComponent<UpgradeStamp>().stampLevel = PlayerPrefs.GetInt("stampLevel");


        
        paperControl.GetComponent<PaperControl>().spawnPaperNumber = PlayerPrefs.GetInt("spawnPaperNumber");    // SPAWN EDİLECEK KAĞIT SAYISI
        paperControl.GetComponent<PaperControl>().spawnPaperTower = paperControl.GetComponent<PaperControl>().spawnPaperNumber;      
        paperControl.GetComponent<PaperControl>().currentPaperNumber = 0;
        paperControl.GetComponent<PaperControl>().damgaPaperSayisi = 0;
        paperControl.GetComponent<PaperControl>().totalPoint = PlayerPrefs.GetInt("totalPoint");
        paperControl.GetComponent<PaperControl>().totalPointFake = PlayerPrefs.GetInt("totalPointFake");
        paperControl.GetComponent<PaperControl>().paperMoveSpeed = 1;
        paperControl.GetComponent<PaperControl>().sendPaperToTable = true;
        paperControl.GetComponent<PaperControl>().DeletePapers();
        paperControl.GetComponent<PaperControl>().spawnPaperFunc();
        paperControl.GetComponent<PaperControl>().dolarMiktari = PlayerPrefs.GetInt("dolarMiktari");

        paperControl.GetComponent<PaperControl>().anlikKazanc = 0;
        PlayerPrefs.SetInt("anlikKazanc", paperControl.GetComponent<PaperControl>().anlikKazanc);



        if (PlayerPrefs.GetInt("damgaHakki")<= 0)
        {
            PlayerPrefs.SetInt("damgaHakki", 5);
           
        }
        slider.maxValue = PlayerPrefs.GetInt("damgaHakki");
        slider.value = slider.maxValue;
        GetComponent<DamgaControl>().elHakki = PlayerPrefs.GetFloat("elHakki"); 
        GetComponent<DamgaControl>().damgaHakki = PlayerPrefs.GetInt("damgaHakki"); 
        GetComponent<DamgaControl>().damgaLevel = PlayerPrefs.GetInt("damgaLevel"); 
        GetComponent<DamgaControl>().elHakkiLimit = GetComponent<DamgaControl>().elHakki;
        GetComponent<DamgaControl>().damgaSpeed = 1;
        GetComponent<DamgaControl>().canDamga = false;
        GetComponent<DamgaControl>().smokeParticle.Stop();
        GetComponent<DamgaControl>().handMaterial.color = Color.blue;
        GetComponent<DamgaControl>().firstHandColor = Color.blue;



        UIController.instance.upgradeIncome.SetActive(true);
        UIController.instance.upgradeStamina.SetActive(true);
        UIController.instance.blockClickWall.SetActive(true);
        UIController.instance.upgradeStamp.SetActive(true);

    }

    public void WinFinish()
    {
    
        GameController.instance.isContinue = false;
        GameController.instance.SetScore(100);
        GameController.instance.ScoreCarp(1);
                                              
        UIController.instance.ActivateWinScreen(); 

    }

}
