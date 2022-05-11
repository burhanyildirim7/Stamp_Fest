using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class DamgaControl : MonoBehaviour
{
    public bool canDamga = false;
    public float comboSpeed;
    public int damgaHakki;
    public int elHakki = 0;
    public bool brokeDamga = false;
    public ParticleSystem smokeParticle;
    GameObject paperControl;
    GameObject PlayerController;
    public Text elHakkiText;

    public float damgaSpeed = 1f;

    void Start()
    {
    
        paperControl = GameObject.FindGameObjectWithTag("PaperControl");
        PlayerController = GameObject.FindGameObjectWithTag("damga");


       damgaHakki = paperControl.GetComponent<PaperControl>().spawnPaperNumber - 5;
     
    }

    // Update is called once per frame
    void Update()
    {
        elHakkiText.text = "El Hakki = " + elHakki;

        if (elHakki <= 0)
        {
            elHakki = 0;
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

                damgaSpeed = Mathf.Clamp(damgaSpeed,0.1f,1);
            }





            if (damgaHakki < paperControl.GetComponent<PaperControl>().totalPoint)
            {

                // FINISH BURAYA GELECEK
                brokeDamga = true;
                GetComponent<DamgaControl>().enabled = false;
                paperControl.SetActive(false);

                GameController.instance.isContinue = false;
                GameController.instance.SetScore(100);
                GameController.instance.ScoreCarp(1);

                UIController.instance.ActivateWinScreen();
            }
        }
    }

    void DamgaBasmaFunction()
    {
        transform.DOMove(new Vector3(0.4f, 0.3f, -0.2f), damgaSpeed).OnComplete(()=>transform.DOMove(new Vector3(0.8f,0.8f,-0.8f), damgaSpeed)); // Damganýn basýlacaðý yer kodu
        
        canDamga = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "damgaYok")
        {
            paperControl.GetComponent<PaperControl>().MoveCompleteTable();
            other.gameObject.tag = "damgaVar";
           
        
        }
    }

    void SmokeControl()
    {
        var dumanScale = 0.01f;
        Mathf.Clamp(dumanScale, 0.01f, 3f);
        elHakki = Mathf.Clamp(elHakki, 0, 700);


        if (elHakki >= 600)
        {
            GameController.instance.isContinue = false; // Nedense çalýþmýyor.
            PlayerController.GetComponent<PlayerController>().startGame = false;
            UIController.instance.ActivateLooseScreen();
           
        }

        if (damgaSpeed <= 0.2f)
        {
            elHakki+=1;
            smokeParticle.Play();
         
            smokeParticle.transform.localScale += new Vector3(dumanScale, dumanScale, dumanScale);
            smokeParticle.startColor = Color.Lerp(smokeParticle.startColor, Color.black, 0.5f * Time.deltaTime);

            if (smokeParticle.transform.localScale.x >= 2f )
            {
                smokeParticle.transform.localScale = new Vector3(2,2,2);
            }
           
        }
        else
        {
            if (elHakki>= 2)
            {
                elHakki -= 2;
            }
          
            smokeParticle.startColor = Color.Lerp(smokeParticle.startColor, Color.white, 1f * Time.deltaTime);
            smokeParticle.transform.localScale -= new Vector3(dumanScale*2, dumanScale*2, dumanScale*2);
            if (smokeParticle.transform.localScale.x <= 0.5f)
            {
                smokeParticle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                smokeParticle.Stop();
            }
        }
    }
}
