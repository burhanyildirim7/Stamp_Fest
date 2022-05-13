using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SekreterControl : MonoBehaviour
{
    Animator sekreterAnim;
    bool isWalk = false;

    public Transform targetTablePoint;
    public Transform leavePerspectivePoint;

     void Start()
    {
        sekreterAnim = GetComponent<Animator>();
     
       
        WalkToTable();
    }

     void Update()
    {
    


    }

    void WalkToTable()
    {

    
            sekreterAnim.SetBool("switchWalkToIdle", true);
            transform.LookAt(targetTablePoint);
        transform.DOMove(targetTablePoint.transform.position, 3).OnComplete(() => {
        isWalk = false;
      
        StartCoroutine(HandsUp());
           
        });
           
    
      
    }

    void WalkToDoor()
    {
        transform.LookAt(leavePerspectivePoint);
        sekreterAnim.SetBool("switchWalkToIdle", true);
        transform.DOMove(leavePerspectivePoint.transform.position, 3);
    }

    IEnumerator HandsUp()
    {
        transform.LookAt(new Vector3(0,-10,0));
        sekreterAnim.SetBool("handsUp", true);
      

        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("PaperControl").GetComponent<PaperControl>().DamgaliKagitlarSekretere();
        sekreterAnim.SetBool("switchWalkToIdle", false);
        WalkToDoor();
    }
}
