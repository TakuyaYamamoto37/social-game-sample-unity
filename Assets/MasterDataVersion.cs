using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDataVersionModel
{
    public int master_data_version;
}

public class MasterDataVersion
{
    public static void CreateTable()
    {
        string query = "create table if not exists master_data_version(master_data_version int, primary key(master_data_version));";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteQuery(query);
    }

    public static void Set(int masterDataVersion)
    {
        string query = "delete from master_data_version;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        sqlDB.ExecuteNonQuery(query);

        query = string.Format("insert or replace into master_data_version (master_data_version) values ({0});", masterDataVersion);
        sqlDB.ExecuteNonQuery(query);
    }

    public static MasterDataVersionModel GetMasterDataVersion()
    {
        MasterDataVersionModel masterDataVersionModel = new MasterDataVersionModel();
        string query = "select * from master_data_version;";
        SqliteDatabase sqlDB = new SqliteDatabase(Sqlite.sqliteDBpath);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            masterDataVersionModel.master_data_version = int.Parse(dr["master_data_version"].ToString());
        }

        return masterDataVersionModel;
    }
}
