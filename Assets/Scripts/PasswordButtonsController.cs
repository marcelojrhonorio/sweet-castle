using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordButtonsController : MonoBehaviour
{   
    public GameObject showPassword;
    public GameObject showPasswordConfirmation;
    public GameObject hidePassword;
    public GameObject hidePasswordConfirmation;

    public TMP_InputField inputPassword;
    public TMP_InputField inputPasswordConfirmation;

    public void ShowHidePasswrod()
    {
        showPassword.SetActive(false);
        hidePassword.SetActive(true);
        SetPasswordContentType(inputPassword, TMP_InputField.ContentType.Standard);
    }

    private void SetPasswordContentType(TMP_InputField tmp_if,TMP_InputField.ContentType contetTypePass)
    {
        tmp_if.contentType = contetTypePass ;
        tmp_if.DeactivateInputField ( ) ;
        tmp_if.ActivateInputField ( ) ;
    }

    public void HideHidePassword()
    {
        showPassword.SetActive(true);
        hidePassword.SetActive(false);
        SetPasswordContentType(inputPassword, TMP_InputField.ContentType.Password);
    }

    public void ShowHidePasswordConfirmation()
    {
        hidePasswordConfirmation.SetActive(true);
        showPasswordConfirmation.SetActive(false);
        SetPasswordContentType(inputPasswordConfirmation, TMP_InputField.ContentType.Standard) ;
    }

    public void HideHidePasswordConfirmation()
    {
        hidePasswordConfirmation.SetActive(false);
        showPasswordConfirmation.SetActive(true);
        SetPasswordContentType(inputPasswordConfirmation, TMP_InputField.ContentType.Password) ;
    }
}
