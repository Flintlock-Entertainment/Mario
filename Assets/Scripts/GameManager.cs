using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Level levelNumber;

    void Awake()
    {
        Instance = this;
    }
    public void ChangeState(Level newLevelNumber)
    {
        levelNumber = newLevelNumber;
        switch (levelNumber)
        {
            case Level.One:
                LevelOne();
                break;
            case Level.Two:
                LevelTwo();
                break;
            case Level.Three:
                LevelThree();
                break;
            case Level.Win:
                Win();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevelNumber), newLevelNumber, null);
        }
    }

    public void LevelOne()
    {
        Debug.Log("one");
    }
    public void LevelTwo()
    {
        Debug.Log("two");
    }
    public void LevelThree()
    {
        Debug.Log("three");
    }
    public void Win()
    {
        Debug.Log("win");
    }
}

public enum Level { One = 1, Two = 2, Three = 3 , Win = 4};
