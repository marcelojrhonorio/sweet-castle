using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string currentLevel;
    public string points;
    public string createdAt;

    public User(string currentLevel, string points, string createdAt)
    {
        this.currentLevel = currentLevel;
        this.points = points;
        this.createdAt = createdAt;
    }
}
