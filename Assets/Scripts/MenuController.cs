using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds functions for responding to and recording preferences on menu.
/// </summary>
public class MenuController : MonoBehaviour {

    /// <summary>
    /// Records an alphanumeric participant ID. Hit enter to record. May be entered multiple times
    /// but only last submission is used. Called using a dynamic function in the inspector
    /// of the textfield object.
    /// </summary>
    /// <param name="arg0"></param>
    public void RecordID(string arg0)
    {
        GlobalControl.Instance.participantID = arg0;
    }

    public void RecordTimeLimit(int arg0)
    {
        // Add 1 index and then multiply by 60 to find seconds time limit
        GlobalControl.Instance.timeLimit = Mathf.Round((arg0 + 1) * 60);
    }

    public void RecordLevelNumber(int arg0)
    {
        // Add 1 index to find level that should be loaded
        GlobalControl.Instance.levelNumber = arg0 + 1;
    }

    public void RecordTryNumber(string arg0)
    {
        GlobalControl.Instance.tryNumber = arg0;
    }


    /// <summary>
    /// Loads next scene if wii is connected and participant ID was entered.
    /// </summary>
    public void NextScene()
    {
        if (GlobalControl.Instance.levelNumber == 1)
        {
            SceneManager.LoadScene("Roll-a-ball");
        }
        else
        {
            SceneManager.LoadScene("Roll-a-ball 2");
        }   
    }
}
