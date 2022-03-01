using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserPresentModel
{
    public int present_id;
    public int item_type;
    public int item_count;
    public string description;
    public string limited_at;
}

public class UserPresent
{
    public static void CreateTable()
    {
        string query = "create table if not exists user_present(present_id int, item_type int, item_count int, description text, limited_at text, primary key(present_id));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(UserPresentModel[] user_present_model_list)
    {
        //プレゼントが取得されてもデータが残り続けないように一度ドロップする
        string dropQuery = "drop table if exists user_present;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(dropQuery);

        CreateTable();
            
        foreach (UserPresentModel userPresentModel in user_present_model_list)
        {
            string query = string.Format("insert or replace into user_present(present_id, item_type, item_count, description, limited_at) values (\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\');", userPresentModel.present_id, userPresentModel.item_type, userPresentModel.item_count, userPresentModel.description, userPresentModel.limited_at);
            sqlDB.ExecuteNonQuery(query);
        }
        
    }
}
