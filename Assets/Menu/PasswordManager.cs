using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    public GameObject passwordPanel;
    public TMP_InputField inputField;
    public GameObject errorText;

    private string expectedPassword;
    private string sceneToLoad;

    public void OpenPasswordPanel(string password, string scene)
    {
        expectedPassword = password;
        sceneToLoad = scene;

        passwordPanel.SetActive(true);
        errorText.SetActive(false);
        inputField.text = "";
    }

    public void ValidatePassword()
    {
        if (inputField.text == expectedPassword)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            errorText.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        passwordPanel.SetActive(false);
    }
}