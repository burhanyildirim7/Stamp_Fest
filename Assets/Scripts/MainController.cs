using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    GameObject PaperControl;
    GameObject DamgaControl;
    void Start()
    {
        PaperControl = GameObject.FindGameObjectWithTag("PaperControl");
        DamgaControl = GameObject.FindGameObjectWithTag("damga");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
