using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterTextModel
{
    public string text_id;
    public int region;
    public string message_text;
}

public class MasterText
{
    public static void CreateTable()
    {
        string query = "create table if not exists master_text (text_id text, region int, message_text text, primary key(text_id, region));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(MasterTextModel[] master_text_model_list)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        foreach (MasterTextModel masterTextModel in master_text_model_list)
        {
            string query = string.Format("insert or replace into master_text (text_id, region, message_text) values (\'{0}\', \'{1}\', \'{2}\');", masterTextModel.text_id, masterTextModel.region, masterTextModel.message_text);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    public static string GetMasterText(string text_id)
    {
        string messageText = "";
        string query = "select * from master_text where text_id = \"" + text_id + "\"";
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            query += " and region = 1;"; 
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            query += " and region = 2;";
        }
        else
        {
            return messageText;
        }
        Debug.Log(query);
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        foreach (DataRow dr in dataTable.Rows)
        {
            messageText = dr["message_text"].ToString();
        }

        return messageText;
    }
}
