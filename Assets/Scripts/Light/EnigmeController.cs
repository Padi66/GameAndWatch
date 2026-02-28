using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class EnigmeController : MonoBehaviour
{
    [SerializeField] private EnigmeDatas enigmeDatas;
    [SerializeField] private Light2D[] lights;
    [SerializeField] private Light2D winLight;

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = enigmeDatas.LampsActivated[i];
        }

        winLight.enabled = enigmeDatas.IsAllActivated();
    }
}
