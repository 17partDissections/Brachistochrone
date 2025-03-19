using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Q17pD.Brachistochrone
{
    public class MasterSave
    {
        private string _savePath = Application.dataPath + "/Brachistochrone/GameSceneSave.json";
        public SaveData SaveData {  get; private set; } = new SaveData();

        public void SaveAllData() { string jsonString = JsonUtility.ToJson(SaveData); File.WriteAllText(_savePath, jsonString); }
        public void LoadAllData()
        {
            if(File.Exists(_savePath)) {
                string jsonString = File.ReadAllText(_savePath);
                SaveData = JsonUtility.FromJson<SaveData>(jsonString);
            }
        }
    }
    [Serializable] public class SaveData
    {
        public Vector3 playerPosition = new Vector3(500, 1, 503);
        public RoomSaveData[] RoomSaveData;


        //public PlayerPos PlayerPos = new PlayerPos(500,1,503);
    }
    //[Serializable] public struct PlayerPos 
    //{

    //    public float PlayerPosX, PlayerPosY, PlayerPosZ;
    //    public PlayerPos(float x, float y, float z)
    //    {
    //        PlayerPosX = x;
    //        PlayerPosY = y;
    //        PlayerPosZ = z;
    //    }
    //}
    [Serializable] public struct RoomSaveData
    {
        public string RoomType;
        public Vector3 RoomPosition;
        public RoomSaveData(string roomType, Vector3 roomPos)
        {
            RoomType = roomType;
            RoomPosition = roomPos;
        }

    }

}
