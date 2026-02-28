using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerNamePanel : MonoBehaviour
{
    [SerializeField] private SO_PlayersDatas playersDatas;
    [SerializeField] private TMP_InputField playerInputField;

    public void LoadDatasInPanel()
    {
        playerInputField.text = playersDatas.Name;
    }

    public void SaveDataInSO()
    {
        playersDatas.Name = playerInputField.text;
    }
    
}
