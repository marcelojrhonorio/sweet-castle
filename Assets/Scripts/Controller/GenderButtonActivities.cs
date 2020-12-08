using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderButtonActivities : MonoBehaviour
{
    //Gender Buttons
    public GameObject kingPressedButton;
    public GameObject queenPressedButton;
    public GameObject kingUnpressedButton;
    public GameObject queenUnpressedButton;

    public static GenderButtonActivities instance;

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

    public void CallGenderKing()
    {
        kingUnpressedButton.SetActive(false);
        kingPressedButton.SetActive(true);

        queenPressedButton.SetActive(false);
        queenUnpressedButton.SetActive(true);
    }

    public void CallGenderQueen()
    {
        queenUnpressedButton.SetActive(false);
        queenPressedButton.SetActive(true);

        kingPressedButton.SetActive(false);
        kingUnpressedButton.SetActive(true);
    }

    public void CallGenderKingPressed()
    {
        kingUnpressedButton.SetActive(false);
        kingPressedButton.SetActive(true);

        queenPressedButton.SetActive(false);
        queenUnpressedButton.SetActive(true);
    }

    public void CallGenderQueenPressed()
    {
        queenUnpressedButton.SetActive(false);
        queenPressedButton.SetActive(true);

        kingPressedButton.SetActive(false);
        kingUnpressedButton.SetActive(true);
    }
}
