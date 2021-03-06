using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentManager : MonoBehaviour
{
    [SerializeField]
    GameObject presentList;

    [SerializeField]
    GameObject contents;

    [SerializeField]
    GameObject presentPrefab;

    [SerializeField]
    public GameObject dialog;

    private int contentsCount = 2;
    private float contentsWidth = 250.0f;

    public void PresentListEvent()
    {
        Action action = () =>
        {
            Dictionary<int, UserPresentModel> userPresentModelList = UserPresent.GetUserPresentList();
            if (userPresentModelList.Count == 0)
            {
                Debug.LogError("プレセントがありません。");
                return;
            }
            int i = 0;
            foreach (UserPresentModel userPresentModel in userPresentModelList.Values)
            {
                GameObject presentItemObject = Instantiate(presentPrefab) as GameObject;
                presentItemObject.transform.SetParent(contents.transform);
                presentItemObject.transform.localPosition = new Vector3(175.0f + i % contentsCount * contentsWidth, -150.0f - i / contentsCount * contentsWidth, 0.0f);
                PresentItem presentItem = presentItemObject.GetComponent<PresentItem>();
                if (presentItem == null)
                {
                    Debug.LogError("PresentItemがアタッチされていません");
                    break;
                }

                presentItem.userPresentModel = userPresentModel;
                presentItemObject.SetActive(true);
                presentList.SetActive(true);
                presentPrefab.SetActive(false);
                i++;
            }
        };
        
        UserProfileModel userProfileModel = UserProfile.Get();
        if (string.IsNullOrEmpty(userProfileModel.user_id))
        {
            Debug.LogError("TitleSceneを起動してユーザー登録を行ってください");
            return;
        }

        StartCoroutine(CommunicationManager.ConnectServer("present_list", "&user_id=" + userProfileModel.user_id, action));
    }
}
