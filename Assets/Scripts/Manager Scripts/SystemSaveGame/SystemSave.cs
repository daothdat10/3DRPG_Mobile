﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SystemSave
{ 
    
    public static void Save(GameData data)
    {
        //tao một phiên bản trình dịch nhị phân
        BinaryFormatter formatter = new BinaryFormatter();

        //luồng tep de luu du lieu
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        
        formatter.Serialize(fs, data);

        fs.Close();


    }

    public static GameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameData emptyData = new GameData();
            Save(emptyData);
            
            return emptyData;
        }


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);

        GameData data = formatter.Deserialize(fs) as GameData;
        fs.Close();

        return data;
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/data.qnd"; 
    }

    

}
