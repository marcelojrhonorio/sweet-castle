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
    public class Usuario 
    {
        public string email;
        public string username;
        public string castlename;
        public string password;
        public string gender;

        public Usuario()
        {

        }

        public Usuario(string email, string username, string castlename, string password, string gender)
        {
            this.email = email;
            this.username = username;
            this.castlename = castlename;
            this.password = password;
            this.gender = gender;
        }
    }

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
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

    //Gender Buttons
    public GameObject kingPressedButton;
    public GameObject queenPressedButton;
    public GameObject kingUnpressedButton;
    public GameObject queenUnpressedButton;

    public GameObject alert;
    public TMP_Text alertMessage;

    public string userNameString;
    public string castleNameString;
    public string gender;

     void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;       
    }

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

        Debug.Log(result.Count);    

        if(result.Count != 0)
        {
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
        step1.SetActive(false);
        step2.SetActive(true);
    }

    public void RegisterStep2()
    {  
        if(password.text != passwordConfirmation.text) 
        {
            //mostrar erro ao usuário
            Debug.Log("As senhas não correspondem!");
            return;
        }   

        //random hash generator
        RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        byte[] randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        var userId = System.Convert.ToBase64String(randomBytes);  
      
        //Create the salt value with a cryptographic PRNG
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]); 
        
        //Create the Rfc2898DeriveBytes and get the hash value
        var pbkdf2 = new Rfc2898DeriveBytes(password.text, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        //Combine the salt and password bytes for later use
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        
        //Turn the combined salt+hash into a string 
        string savedPasswordHash = System.Convert.ToBase64String(hashBytes);    

        Usuario usuario = new Usuario(email.text, userNameString, castleNameString, savedPasswordHash, gender); 
        string json = JsonUtility.ToJson(usuario);

        reference = FirebaseDatabase.DefaultInstance.RootReference;  

        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);

        step1.SetActive(false);
        step2.SetActive(false);
        main.SetActive(true);
    }

    public void GenderKing()
    {
        kingUnpressedButton.SetActive(false);
        kingPressedButton.SetActive(true);

        queenPressedButton.SetActive(false);
        queenUnpressedButton.SetActive(true);
        gender = "king";
    }

    public void GenderQueen()
    {
        queenUnpressedButton.SetActive(false);
        queenPressedButton.SetActive(true);

        kingPressedButton.SetActive(false);
        kingUnpressedButton.SetActive(true);
        gender = "queen";
    }

    public void GenderKingPressed()
    {
        kingUnpressedButton.SetActive(false);
        kingPressedButton.SetActive(true);

        queenPressedButton.SetActive(false);
        queenUnpressedButton.SetActive(true);
        gender = "king";
    }

    public void GenderQueenPressed()
    {
        queenUnpressedButton.SetActive(false);
        queenPressedButton.SetActive(true);

        kingPressedButton.SetActive(false);
        kingUnpressedButton.SetActive(true);

        gender = "queen";
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

    public void CloseAlert()
    {
        alert.SetActive(false);
    }

    
}
