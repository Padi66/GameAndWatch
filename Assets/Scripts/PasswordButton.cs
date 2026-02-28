using UnityEngine;

public class PasswordButton : MonoBehaviour
{
    public PasswordManager manager;
    public string password;
    public string sceneName;

    public void OnClick()
    {
        manager.OpenPasswordPanel(password, sceneName);
    }
}