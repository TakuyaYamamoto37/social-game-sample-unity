using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Sqlite
{
    public static readonly string sqliteDBpath = Application.dataPath + "/Service.db";

    public static void CreateTable()
    {
        if (!File.Exists(sqliteDBpath))
        {
            File.Create(sqliteDBpath);
        }

        MasterDataVersion.CreateTable();
        UserProfile.CreateTable();
        UserLogin.CreateTable();
        MasterLoginItem.CreateTable();
        MasterQuest.CreateTable();
        UserQuest.CreateTable();
        MasterCharacter.CreateTable();
        UserCharacter.CreateTable();
    }
}
