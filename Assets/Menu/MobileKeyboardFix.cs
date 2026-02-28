using UnityEngine;
using TMPro;

public class MobileKeyboardFix : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;

    private void Start()
    {
        if (inputField == null)
            return;

        inputField.keyboardType = TouchScreenKeyboardType.NumberPad;
        inputField.shouldHideMobileInput = false;
        inputField.shouldHideSoftKeyboard = false;
    }

    /// <summary>Active l'InputField et force l'ouverture du clavier numérique sur mobile.</summary>
    public void ActivateInput(string value)
    {
        if (inputField == null)
            return;

        inputField.DeactivateInputField();
        inputField.ActivateInputField();
    }
}