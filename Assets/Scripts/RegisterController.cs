 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;

public class RegisterController : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;

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
           
        foreach (var item in result)
        {
            alert.SetActive(false);

            if ("" != item.Key) 
            {
                //mostrar erro ao usuário
                //Debug.Log("Tem key! Não pode cadastrar. " + item.Key);
                alert.SetActive(true);
                alertMessage.text = "Já existe um usuário com esse nome!";
                return;
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

        Debug.Log(email.text);
        Debug.Log(password.text);
        Debug.Log(passwordConfirmation.text);

        Debug.Log(userNameString);
        Debug.Log(castleNameString);
        Debug.Log(gender); 

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
