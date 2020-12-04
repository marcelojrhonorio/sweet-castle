using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string email;
    public string username;
    public string castlename;
    public string password;
    public string gender;

    public User()
    {

    }

    public User(string email, string username, string castlename, string password, string gender)
    {
        this.email = email;
        this.username = username;
        this.castlename = castlename;
        this.password = password;
        this.gender = gender;
    }
}
