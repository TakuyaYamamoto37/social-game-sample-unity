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

    void Awake()
    {
        Sqlite.CreateTable();
    }


    // Start is called before the first frame update
    void Start()
    {
        userProfileModel = UserProfile.Get();
        if (!string.IsNullOrEmpty(userProfileModel.user_id))
        {
            userID.text = "ID : " + userProfileModel.user_id;
        }
    }

    public void LoginButtonEvent()
    {
        userProfileModel = UserProfile.Get();
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Action action = () =>
            {
                Debug.Log("登録完了しました");
            };
            StartCoroutine(CommunicationManager.ConnectServer("registration", "", action));
        }
        else
        {
            Action action = () =>
            {
                //ログイン後の処理
            };
            StartCoroutine(CommunicationManager.ConnectServer("login", "&user_id=" + userProfileModel.user_id, action));
        }
    }
}
