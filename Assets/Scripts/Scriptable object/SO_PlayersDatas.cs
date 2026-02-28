using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayersDatas", menuName = "Scriptable Objects/SO_PlayersDatas")]
public class SO_PlayersDatas : ScriptableObject
{
    public string Name;
    public int Score;
    public int Level;
    
    private SaveController saveSystem;

    public void LoadDatas()
    {
        CheckSaveSystem();
        //utiliser la fonction de savesystem pour load les data
        //cette fonction renvoi des playersdatas
        PlayerDatas datas = saveSystem.Load();
        //donc je dois les affecter aux variable de mon scriptable object
        Name = datas.Name;
        Score =  datas.Score;
        Level = datas.Level;
    }

    public void SaveDatas()
    {
        CheckSaveSystem();
        //pour utiliser la fonction save de savesystem j'ai besoin de playerdatas
        //je dois créer un playerdatas à partir de mon SO
        PlayerDatas datas = new PlayerDatas();
        datas.Name = Name;
        datas.Score = Score;
        datas.Level = Level;
        //j'envois ça à la fonction save de savesystem
        saveSystem.Save(datas);
    }

    private void CheckSaveSystem()
    {
        //vérifie si SaveSystem est vide
        if (saveSystem == null)
        {
            saveSystem = new SaveController();
        }
    
    }
}
