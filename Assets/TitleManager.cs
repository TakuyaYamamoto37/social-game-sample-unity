using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Text userID;

    private UserProfileModel userProfileModel;

    string dbPath;

    void Awake()
    {
        //string DBPath = Application.persistentDataPath + "/Service.db";
        //string DBPath = Application.dataPath + "/StreamingAssets/Service.db";
        this.dbPath = Application.dataPath + "/Service.db";

        if (!File.Exists(dbPath))
        {
            File.Create(dbPath);
        }

        UserProfile.CreateTable(dbPath);
    }


    // Start is called before the first frame update
    void Start()
    {
        userProfileModel = UserProfile.Get(dbPath);
        if (!string.IsNullOrEmpty(userProfileModel.user_id))
        {
            userID.text = "ID : " + userProfileModel.user_id;
        }
    }

    public void LoginButtonEvent()
    {
        Action action = () =>
        {
            userProfileModel = UserProfile.Get(dbPath);
            if (!string.IsNullOrEmpty(userProfileModel.user_id))
            {
                userID.text = "ID : " + userProfileModel.user_id;
            }
        };
        StartCoroutine(CommunicationManager.ConnectServer("registration", "", action));
    }
}
