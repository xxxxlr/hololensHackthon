using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserModeManager : MonoBehaviour
{

    public string userMode;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUserMode(string newMode) {
        userMode = newMode;
    }

    public bool isLearnMode() {
        return userMode == "learn";
    }
}
