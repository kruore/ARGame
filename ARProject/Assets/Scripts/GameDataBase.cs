using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

public class GameDataBase : Singleton<GameDataBase>
{
    public List<GpsData> places = new List<GpsData>();
    public List<GpsData> inventory_places = new List<GpsData>();
    public List<Item> cards = new List<Item>();
    string conn = string.Empty;
    public void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            conn = "URI=file:" + Application.persistentDataPath + "/testDB.sqlite";
        }
        else
        {
            conn = "URI=file:" + Application.dataPath + "/StreamingAssets/testDB.sqlite";
        }
        Ds_CopyDB();
        DS_GpsplaceSetting();
        CardSetting();
        Ds_InventoryInsert(0);
    }
    //사용플렛폼이 안드로이드 일경우에 일반적인 경로로 DB접근이 되지않아 DB를 복사하여 사용가능한 위치에 놓아준다.
    void Ds_CopyDB()
    {
        if(Application.platform==RuntimePlatform.Android)
        {
            conn = Application.persistentDataPath + "/testDB.sqlite";
            if(!File.Exists(conn))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "/assets/testDB.sqlite");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone)
                {
                    File.WriteAllBytes(conn, loadDB.bytes);
                }
            }
        }
    }
    //GpsplaceSetting
    public void DS_GpsplaceSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Gpsplace";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int Num = inventory_places.Count;
            string Name = reader.GetString(1);
            double Latitude = reader.GetDouble(2);
            double Longitude = reader.GetDouble(3);
            Element element = (Element)reader.GetInt32(4);
            string quad = reader.GetString(5);
            inventory_places.Add(new GpsData(Num, Name, Latitude, Longitude,element,quad));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void CardSetting()
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM Cards";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int num = reader.GetInt32(0);
            string Name = reader.GetString(4);
            Element element = (Element)reader.GetInt32(5);
            cards.Add(new Item(num,10,10,Name,element));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
   
    public void Ds_InventoryInsert(int DBnum)
    {
        Debug.Log(DBnum);
        Debug.Log(inventory_places.Count + 1);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "insert into Inventory select* from Cards where num = " + DBnum + "; ";
        sqlQuery += "update Inventory set Num = " + (inventory_places.Count + 1) + " where Num = " + DBnum + "; ";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    int Num = reader.GetInt32(0);
        //    string Races = reader.GetString(1);
        //    string Rarelty = reader.GetString(2);
        //    string Type = reader.GetString(3);
        //    int HP = reader.GetInt32(4);
        //    int Depensive = reader.GetInt32(5);
        //    int MoveRange = reader.GetInt32(6);
        //    int AttackRange = reader.GetInt32(7);
        //    inventory_places.Add(new GpsData(Num, Races, Rarelty, Type, HP, Depensive, MoveRange, AttackRange));
        //}
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    } 

    //public void DS_DbSetting()
    //{
    //    {
    //        IDbConnection dbconn;
    //        IDbCommand dbcmd;
    //        string sqlQuery;
    //        IDataReader reader;

    //        if (Application.platform == RuntimePlatform.Android)
    //        {
    //            conn = "URI=file:" + Application.persistentDataPath + "/testDB.sqlite";
    //        }
    //        else
    //        {
    //            conn = "URI=file:" + Application.dataPath + "/StreamingAssets/testDB.sqlite";
    //        }
    //            dbconn = (IDbConnection)new SqliteConnection(conn);
    //            dbconn.Open();

    //            dbcmd = dbconn.CreateCommand();
    //            sqlQuery = "SELECT * FROM Card";
    //            dbcmd.CommandText = sqlQuery;
    //            reader = dbcmd.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                int Num = reader.GetInt32(0);
    //                string Name = reader.GetString(1);
    //                double Latitude = reader.GetDouble(2);
    //                double Longitude = reader.GetDouble(3);
    //                string quad = reader.GetString(4);
    //                places.Add(new GpsData(Num, Name, Latitude, Longitude, quad));
    //            }
    //            reader.Close();
    //            reader = null;
    //            dbcmd.Dispose();
    //            dbcmd = null;
    //            dbconn.Close();
    //            dbconn = null;
    //    }
    //}
}