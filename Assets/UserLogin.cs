using System;
using UnityEngine;

[Serializable]
public class UserLoginModel
{
    public string user_id;
    public int login_day;
    public string last_login_at;
}


public static class UserLogin
{
    public static void CreateTable()
    {
        string query = "create table if not exists user_login (user_id text, login_day int, last_login_at text, primary key(user_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(UserLoginModel user_login)
    {
        string query = string.Format("insert or replace into user_login(user_id, login_day, last_login_at) values (\'{0}\', \'{1}\', \'{2}\');", user_login.user_id, user_login.login_day, user_login.last_login_at);
        Debug.Log(query);
        SqliteDatabase sqlDB = new SqliteDatabase(Application.dataPath + "/Service.db");
        sqlDB.ExecuteQuery(query);
    }

    public static UserLoginModel Get()
    {
        string query = "select * from user_login";
        SqliteDatabase sqlDB = new SqliteDatabase(Application.dataPath + "/Service.db");
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        UserLoginModel userLoginModel = new UserLoginModel();
        foreach (DataRow dr in dataTable.Rows)
        {
            userLoginModel.user_id = dr["user_id"].ToString();
            userLoginModel.login_day = int.Parse(dr["login_day"].ToString());
            userLoginModel.last_login_at = dr["last_login_at"].ToString();
        }

        return userLoginModel;
    }
}
