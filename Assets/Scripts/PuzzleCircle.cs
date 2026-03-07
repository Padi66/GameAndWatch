using UnityEngine;
using UnityEngine.UI;

public class PuzzleCircle : MonoBehaviour
{
    [Header("Boutons du puzzle (8 boutons)")]
    public Button[] buttons;

    [Header("Lumières du puzzle (8 images)")]
    public Image[] lights;

    [Header("Couleur quand la lumière est allumée")]
    public Color activeColor = Color.yellow;

    [Header("Couleur quand la lumière est éteinte")]
    public Color inactiveColor = Color.gray;

    private int[] buttonToLight;   
    private int currentTarget = -1; // -1 = pas encore commencé
    private int lightsActivated = 0;

    private void Start()
    {
        GenerateRandomMapping();
        ResetLights();
        AssignButtonEvents();
    }

    private void GenerateRandomMapping()
    {
        buttonToLight = new int[8];

        for (int i = 0; i < 8; i++)
            buttonToLight[i] = i;

        for (int i = 0; i < 8; i++)
        {
            int rand = Random.Range(i, 8);
            int temp = buttonToLight[i];
            buttonToLight[i] = buttonToLight[rand];
            buttonToLight[rand] = temp;
        }

        Debug.Log("Mapping aléatoire : " + string.Join(", ", buttonToLight));
    }

    private void ResetLights()
    {
        foreach (var light in lights)
            light.color = inactiveColor;

        currentTarget = -1;
        lightsActivated = 0;
    }

    private void AssignButtonEvents()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
        }
    }

    private void OnButtonPressed(int buttonIndex)
    {
        int lightIndex = buttonToLight[buttonIndex];

        Debug.Log($"Bouton {buttonIndex} → Lumière {lightIndex} | Cible = {currentTarget}");

        // PREMIER CLIC : on choisit le point de départ
        if (currentTarget == -1)
        {
            currentTarget = lightIndex;
            lights[currentTarget].color = activeColor;
            lightsActivated = 1;
            Debug.Log("Départ choisi : " + currentTarget);
            return;
        }

        // Calcul de la prochaine lumière attendue (boucle)
        int nextTarget = (currentTarget + 1) % 8;

        // Vérifie si c'est la bonne lumière
        if (lightIndex == nextTarget)
        {
            currentTarget = nextTarget;
            lights[currentTarget].color = activeColor;
            lightsActivated++;

            if (lightsActivated >= 8)
            {
                Debug.Log("Puzzle terminé !");
            }
        }
        else
        {
            Debug.Log("Mauvais bouton → RESET !");
            ResetLights();
        }
    }
}
