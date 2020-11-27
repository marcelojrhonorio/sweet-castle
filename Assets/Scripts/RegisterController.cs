using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterController : MonoBehaviour
{
    public GameObject main;
    public GameObject step1;
    public GameObject step2;

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
}
