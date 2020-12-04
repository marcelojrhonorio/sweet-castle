using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallStepsController : MonoBehaviour
{
    public GameObject main;
    public GameObject step1;
    public GameObject step2;

    public static CallStepsController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    } 

    public void CallRegisterStep1()
    {
        main.SetActive(false);
        step1.SetActive(true);
        step2.SetActive(false);
    }

   public void CallRegisterStep2()
   {
       step1.SetActive(false);
       step2.SetActive(true);
   }

   public void CallMain()
   {
       step1.SetActive(false);
       step2.SetActive(false);
       main.SetActive(true);
   }
}
