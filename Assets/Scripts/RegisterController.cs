﻿ using System.Collections;
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
    public string gender;

    //Steps variables
    public GameObject main;
    public GameObject step1;
    public GameObject step2;

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

    DatabaseReference reference;

     //Function for the register button
    public void RegisterStep1()
    {
        //Call the register coroutine passing the email, password, and username
        //StartCoroutine(Register(userName.text, castleName.text));

        //reference = FirebaseDatabase.DefaultInstance.RootReference;

        //Firebase.Database.Query data = FirebaseDatabase.DefaultInstance
        //    .GetReference("users").EqualTo(userName.text);
        //reference.Child("users").Child(userId).SetRawJsonValueAsync(json);

        //Debug.Log(data);
 
        //Debug.Log(userName.text);
        //Debug.Log(castleName.text);
        //Debug.Log(gender);

        //go to step 2
        step1.SetActive(false);
        step2.SetActive(true);
    }

   /* private IEnumerator Register(string userName, string castleName)
    {
        Debug.Log(userName);
        Debug.Log(castleName);
        
        if (_username == "")
        {
            //If the username field is blank show a warning
            //warningRegisterText.text = "Missing Username";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            //warningRegisterText.text = "Password Does Not Match!";
        }
         else 
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                //warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        //warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        
                        //UIManager.instance.LoginScreen();
                        //warningRegisterText.text = "";
                    }
                }
            }
        }
        
    }
*/

    public void RegisterStep2()
    {
        Debug.Log(email.text);
        Debug.Log(password.text);
        Debug.Log(passwordConfirmation.text);

        if(password.text != passwordConfirmation.text) 
        {
            Debug.Log("As senhas não correspondem!");
            return;
        }

        //go to step 2
        step1.SetActive(false);
        step2.SetActive(false);
        main.SetActive(true);
    }

    public void GenderKing()
    {
        gender = "king";
    }

    public void GenderQueen()
    {
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

    
}
