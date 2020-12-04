using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using System.Security.Cryptography;
using System;

public class RegisterController : MonoBehaviour
{    
    //Firebase variables
   /* [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;*/
    DatabaseReference reference;

    //Register variables
    [Header("Register")]
    public TMP_InputField userName;
    public TMP_InputField castleName;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField passwordConfirmation;   

    //Steps variables
    public GameObject main;
    public GameObject step1;
    public GameObject step2;

    public GameObject alert;
    public TMP_Text alertMessage;

    public string userNameString;
    public string castleNameString;
    public string gender;

    //Function for the register button
    public void RegisterStep1()
    {
        alert.SetActive(false);

        if ("" == userName.text || "" == castleName.text || "" == gender)
        {
            //mostrar erro ao usuário
            alert.SetActive(true);
            alertMessage.text = "Todos os campos são obrigatórios!";
            return;
        }

        alert.SetActive(false);

        userNameString = userName.text;
        castleNameString = castleName.text;

        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("username").EqualTo(userName.text).ValueChanged += HandleValueChanged; 
        
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args) 
    {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }        
                
        var result = args.Snapshot.Value as Dictionary<string, System.Object>;      

        if(result.Count != 0)
        {  
            alert.SetActive(false);

            foreach (var item in result)
            {
                alert.SetActive(false);

                if ("" != item.Key) 
                {
                    //mostrar erro ao usuário
                    alert.SetActive(true);
                    alertMessage.text = "Já existe um usuário com esse nome!";
                    return;
                }
            }
        }

        alert.SetActive(false);

        //go to step two
        CallStepsController.instance.CallRegisterStep2();
    }

    public void RegisterStep2()
    {  
        if(password.text != passwordConfirmation.text) 
        {
            Debug.Log("As senhas não correspondem!");
            //mostrar erro ao usuário
            alert.SetActive(true);
            alertMessage.text = "As senhas não correspondem!";
            return;
        }   

        //random hash generator
        RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        byte[] randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        var userId = System.Convert.ToBase64String(randomBytes);  
      
        //Create the salt value with a cryptographic PRNG
        byte[] salt;
        //new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]); 
        rngCryptoServiceProvider.GetBytes(salt = new byte[16]); 
        
        //Create the Rfc2898DeriveBytes and get the hash value
        var pbkdf2 = new Rfc2898DeriveBytes(password.text, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        //Combine the salt and password bytes for later use
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        
        //Turn the combined salt+hash into a string 
        string savedPasswordHash = System.Convert.ToBase64String(hashBytes);    

        User usuario = new User(email.text, userNameString, castleNameString, savedPasswordHash, gender); 
        string json = JsonUtility.ToJson(usuario);

        reference = FirebaseDatabase.DefaultInstance.RootReference;  

        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);

        //go to main
        CallStepsController.instance.CallMain();

    }

    public void GenderKing()
    {
        gender = "king";
        GenderButtonActivities.instance.CallGenderKing();
    }

    public void GenderQueen()
    {        
        GenderButtonActivities.instance.CallGenderQueen();
        gender = "queen";
    }

    public void GenderKingPressed()
    {
        GenderButtonActivities.instance.CallGenderKingPressed();
        gender = "king";
    }

    public void GenderQueenPressed()
    {
        GenderButtonActivities.instance.CallGenderQueenPressed();
        gender = "queen";
    }

    public void CloseAlert()
    {
        alert.SetActive(false);
    }    
}
