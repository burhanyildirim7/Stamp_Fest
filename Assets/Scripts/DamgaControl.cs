using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class DamgaControl : MonoBehaviour
{
    private Vector3 firstPositionDamga;
    private Vector3 firstRotationDamga;

    public bool canDamga = false;
    public bool brokeDamga = false;

    public float comboSpeed;
    public float elHakki;
    public float elHakkiLimit;
    public float damgaSpeed = 1f;
    public int damgaHakki = 5;
    public int damgaLevel = 1;
    public ParticleSystem smokeParticle;

    GameObject paperControl;
    GameObject PlayerController;


    public Text elHakkiText;

    public Material handMaterial;
    public Color firstHandColor;
    



    void Start()
    {
        firstHandColor = handMaterial.color;
        firstPositionDamga = transform.position;
        firstRotationDamga = transform.eulerAngles;
        paperControl = GameObject.FindGameObjectWithTag("PaperControl");
        PlayerController = GameObject.FindGameObjectWithTag("damga");
        damgaHakki = 5;
        PlayerPrefs.SetInt("damgaHakki", damgaHakki);
        elHakki = 10;
        PlayerPrefs.SetFloat("elHakki", elHakki);
        damgaLevel = 1;
        PlayerPrefs.SetInt("damgaLevel", damgaLevel);

    }

 
    // Update is called once per frame
    void Update()
    {
        Debug.Log(damgaLevel);
        if (damgaLevel == 1)
        {
            damgaHakki = 5;
            PlayerPrefs.SetInt("damgaHakki", damgaHakki);
            PlayerPrefs.SetInt("damgaLevel", damgaLevel);
        }

        else if (damgaLevel == 2)
        {
            damgaHakki = 7;
            PlayerPrefs.SetInt("damgaHakki", damgaHakki);
            PlayerPrefs.SetInt("damgaLevel", damgaLevel);
        }


        else if (damgaLevel == 3)
        {
            damgaHakki = 9;
            PlayerPrefs.SetInt("damgaHakki", damgaHakki);
            PlayerPrefs.SetInt("damgaLevel", damgaLevel);
        }



        elHakkiText.text = "El Hakki = " + Mathf.RoundToInt(elHakki);

        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame == false)
        {
          
            elHakki = elHakkiLimit;
            PlayerPrefs.SetFloat("elHakki",elHakki);
           
        }

        if (elHakki >= elHakkiLimit)
        {
            elHakki = elHakkiLimit;
        }
        
          else if (elHakki<=0)
          {
              GameController.instance.isContinue = false; // Nedense �al��m�yor.
              PlayerController.GetComponent<PlayerController>().startGame = false;
              UIController.instance.ActivateLooseScreen();

          }
        

       
        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {
          
            SmokeControl();


            if (Input.GetMouseButton(0))
            {
                damgaSpeed -= comboSpeed * Time.deltaTime;
                paperControl.GetComponent<PaperControl>().paperMoveSpeed -= comboSpeed * Time.deltaTime;

                if (damgaSpeed<= 0.1f)
                {
                    damgaSpeed = 0.1f;
                }

                else if (damgaSpeed >=1f)
                {
                    damgaSpeed = 1f;
                }

          

             

                if (paperControl.GetComponent<PaperControl>().paperMoveSpeed<=0.1f)
                {
                    paperControl.GetComponent<PaperControl>().paperMoveSpeed = 0.1f;
                }

                if (canDamga)
                {
                    DamgaBasmaFunction();
                }

            }
            else
            {
                damgaSpeed += 1.5f*comboSpeed * Time.deltaTime;
                paperControl.GetComponent<PaperControl>().paperMoveSpeed += 1.5f*comboSpeed * Time.deltaTime;
                elHakki+= 2*Time.deltaTime;

                damgaSpeed = Mathf.Clamp(damgaSpeed,0.1f,1);

              
            }





            if (damgaHakki <= paperControl.GetComponent<PaperControl>().totalPoint)
            {

                // FINISH BURAYA GELECEK
                brokeDamga = true;
              

                GameController.instance.isContinue = false;
                PlayerController.GetComponent<PlayerController>().startGame = false;
                GameController.instance.SetScore(100);
                GameController.instance.ScoreCarp(1);

                UIController.instance.ActivateWinScreen();
            }
        }

   
    
    }

    void DamgaBasmaFunction()
    {
       
        transform.DORotate(new Vector3(0, transform.rotation.y, transform.rotation.z), damgaSpeed/1.5f).OnComplete(() => transform.DORotate(firstRotationDamga, damgaSpeed/ 1.5f)) ;
        transform.DOMove(new Vector3(transform.position.x - 1f, transform.position.y-0.7f, transform.position.z), damgaSpeed/ 1.5f).OnComplete(() => transform.DOMove(firstPositionDamga, damgaSpeed/ 1.5f)); // Damgan�n bas�laca�� yer kodu

        canDamga = false;
        
  

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "damgaYok")
        {
            paperControl.GetComponent<PaperControl>().MoveCompleteTable();
            other.gameObject.tag = "damgaVar";
            elHakki--;
        }
  
    }

    void SmokeControl()
    {
        var dumanScale = 0.01f;
        Mathf.Clamp(dumanScale, 0.01f, 3f);
      


      

        if (elHakki > 5 && elHakki <=8)
        {
      
            smokeParticle.Play();

            smokeParticle.transform.DOScale(new Vector3(1,1,1),1);
            smokeParticle.startColor = Color.Lerp(smokeParticle.startColor, Color.black, 0.5f * Time.deltaTime);
            handMaterial.color = Color.Lerp(handMaterial.color, Color.red, 0.5f * Time.deltaTime);



        }

        if (elHakki< 5)
        {
            smokeParticle.transform.DOScale(new Vector3(2, 2, 2), 1);

        }

        if (elHakki>8)
        {
            smokeParticle.Stop();
            smokeParticle.startColor = Color.white;
            handMaterial.color = Color.Lerp(handMaterial.color, firstHandColor, 1 * Time.deltaTime);
        }
    }
}
