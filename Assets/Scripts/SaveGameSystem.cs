using System;
using UnityEngine;

public class SaveGameSystem : MonoBehaviour

{
    [SerializeField] private SO_PlayersDatas playerData;

    public void LoadSaveGame()
    {
        playerData.LoadDatas();
    }

    public void SaveGame()
    {
        playerData.SaveDatas();
    }
   
}
