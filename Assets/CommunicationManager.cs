using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;


[Serializable]
public class ResponseObjects
{
    public UserProfileModel user_profile;
    public int master_data_version;
//    public MasterLoginItemModel[] master_login_item;
}

public class CommunicationManager : MonoBehaviour
{
    const string URL = "http://192.168.3.16:8000/";
    private const string ERROR_MASTER_DATA_UPDATE = "1";
    private const string ERROR_DB_UPDATE = "2";


    public static IEnumerator ConnectServer(string endpoint, string paramater, Action action = null)
    {
        int masterDataVersion = 22;

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(URL + endpoint + "?client_master_version=" + masterDataVersion + paramater);

        /*
        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://192.168.3.16:8000/name?client_master_version=21&user_id=4b3403665fea6&user_name=em_connect");*/

        yield return unityWebRequest.SendWebRequest();

        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            //error
            Debug.LogError(unityWebRequest.error);
        }

        //レスポンス取得
        string text = unityWebRequest.downloadHandler.text;


        ResponseObjects responseObjects = new ResponseObjects();

        if (text.All(char.IsNumber))
        {
            //エラーの場合
            switch (text)
            {
                case ERROR_MASTER_DATA_UPDATE:
                    UnityWebRequest masterDataRequest = UnityWebRequest.Get("http://192.168.3.16:8000/master_data");
                    yield return masterDataRequest.SendWebRequest();


                    //レスポンスを取得
                    string masterText = masterDataRequest.downloadHandler.text;


                    responseObjects = JsonUtility.FromJson<ResponseObjects>(masterText);

                    /*
                    //マスターデータのバージョンはローカルに保存
                    PlayerPrefs.SetInt("master_data_version", responseObjects.master_data_version);

                    //MasterLginItemをSQLiteへ保存チャプター５参考
         //           MasterLoginItem.set(responseObjects.master_data_version);
                    */
                    break;
                case ERROR_DB_UPDATE:
                    Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                    break;
                default:
                    break;
            }
        }
        else
        {
            responseObjects = JsonUtility.FromJson<ResponseObjects>(text);
        }


        //UserProfileをSQLiteへ保存
        if (!string.IsNullOrEmpty(responseObjects.user_profile.user_id))
        {
            UserProfile.Set(responseObjects.user_profile);
        }

        if (action != null)
        {
            action();
        }
    }
}



