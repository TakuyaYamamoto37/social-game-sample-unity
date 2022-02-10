﻿using System.Collections;
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

        UserProfile.CreateTable(sqliteDBpath);
        UserLogin.CreateTable(sqliteDBpath);
        MasterLoginItem.CreateTable(sqliteDBpath);
    }
}
