using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DamgaControl : MonoBehaviour
{
    public bool canDamga = false;
    public float comboSpeed;
    public int damgaHakký;
    public bool brokeDamga = false;
    GameObject paperControl;
    GameObject PlayerController;

    void Start()
    {
        paperControl = GameObject.FindGameObjectWithTag("PaperControl");
        PlayerController = GameObject.FindGameObjectWithTag("damga");

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame)
        {
            Debug.Log("Çalýþýyor");

            if (Input.GetMouseButton(0))
            {
                Time.timeScale += comboSpeed * Time.deltaTime;
                if (canDamga)
                {
                    DamgaBasmaFunction();
                }


            }
            else
            {
                Time.timeScale = 1;
            }

            if (damgaHakký < paperControl.GetComponent<PaperControl>().totalPoint)
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
        transform.DOMove(new Vector3(0.4f, 0.3f, -0.2f), 0.5f).OnComplete(()=>transform.DOMove(new Vector3(0.8f,0.8f,-0.8f),0.5f)); // Damganýn basýlacaðý yer kodu
        
        canDamga = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "damgaYok")
        {
            other.gameObject.tag = "damgaVar";
        }
    }
}
