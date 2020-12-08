using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class LoginController : MonoBehaviour
{
   //jogar método no onClick
   public void LoginButton()
   {
       //StartCoroutine(StartLogin("andrerodrigo22@gmail.com", "123456")); //mudar para Text TMPro
   }

   private IEnumerator StartLogin(string email, string password)
   {      
       var LoginTask = FirebaseAuthenticator.instance.auth.SignInWithEmailAndPasswordAsync(email, password);
       yield return new WaitUntil(predicate: ()=> LoginTask.IsCompleted);

       if(LoginTask.Exception != null)
       {
           HandleLoginErrors(LoginTask.Exception);
       }
       else
       {
           LoginUser(LoginTask);  
       }
   }
   
   void HandleLoginErrors(System.AggregateException loginException)
   {
       Debug.LogWarning(message: $"Failed to login task with{loginException}");
       FirebaseException firebaseEx = loginException.GetBaseException() as FirebaseException;
       AuthError errorCode = (AuthError) firebaseEx.ErrorCode;
       
       string message = DefineLoginErrorMessage(errorCode);
       //mostrar alert error aqui
       //alert.SetActive(true);
       //mudar texto com `message`
   }

   string DefineLoginErrorMessage(AuthError errorCode)
   {
       switch (errorCode)
       {
           case AuthError.MissingEmail:
                return "Você precisa informar o e-mail.";

            case AuthError.MissingPassword:
                return "Você precisa informar a senha.";

            case AuthError.InvalidEmail:
                return "O e-mail informado é inválido.";

            case AuthError.UserNotFound:
                return "Conta inexistente.";

           default:
                return "Erro desconhecido.";
       }
   }

   void LoginUser(System.Threading.Tasks.Task<Firebase.Auth.FirebaseUser> loginTask)
   {
       FirebaseAuthenticator.instance.User = loginTask.Result;
       Debug.LogFormat("User signed in successfully: {0} ({1})", FirebaseAuthenticator.instance.User.DisplayName, FirebaseAuthenticator.instance.User.Email);       
   }
}
