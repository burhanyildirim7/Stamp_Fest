using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DamgaControl : MonoBehaviour
{
    public bool canDamga = false;
    public float combatSpeed;
    public int damgaHakk�;
    public bool brokeDamga = false;
    GameObject paperControl;
    void Start()
    {
        paperControl = GameObject.FindGameObjectWithTag("PaperControl");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Time.timeScale += combatSpeed*Time.deltaTime;
            if (canDamga)
            {
                DamgaBasmaFunction();
            }
         
       
        }
        else
        {
            Time.timeScale = 1;
        }

        if (damgaHakk�<paperControl.GetComponent<PaperControl>().totalPoint)
        {

            // FINISH BURAYA GELECEK
            brokeDamga = true;
            Time.timeScale = 0;
        }
    
    }

    void DamgaBasmaFunction()
    {
        transform.DOMove(new Vector3(0.4f, 0.3f, -0.2f), 0.5f).OnComplete(()=>transform.DOMove(new Vector3(0.8f,0.8f,-0.8f),0.5f)); // Damgan�n bas�laca�� yer kodu
        
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
