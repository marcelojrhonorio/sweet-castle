using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterController : MonoBehaviour
{
    public GameObject step1;
    public GameObject step2;

   public void CallRegisterStep2()
   {
       step1.SetActive(false);
       step2.SetActive(true);
   }
}
