using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData {
    public bool firstSaveFlag = false;
    public bool FirstSaveFlag
    {
        set { this.firstSaveFlag = value; }
        get { return this.firstSaveFlag; }
    }
}

public class DataSave : MonoBehaviour
{
    /*
    [System.Serializable]
    public class SaveData {
        private bool firstSaveFlag;
        public bool FirstSaveFlag
        {
            set { this.firstSaveFlag = value; }
            get { return this.firstSaveFlag; }
        }
    }*/

    public SaveData saveData = new SaveData();

    public void SaveOut(){
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(saveData);

        writer = new StreamWriter(Application.dataPath + "/save.json",false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public void LoadData(){
        string datastr = "";
        StreamReader reader;
        try
        {
        reader = new StreamReader(Application.dataPath + "/save.json");
        }
        catch
        {
            return;
        }
        datastr = reader.ReadToEnd();
        reader.Close();

        saveData = JsonUtility.FromJson<SaveData>(datastr);
        Debug.Log("データーをロードしました");
    }
}
