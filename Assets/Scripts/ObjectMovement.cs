using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private TimeManager _timeManager;

    public void Init(GameObject newObject)
    {
        FallingObject fallingObject = newObject.GetComponent<FallingObject>();
        if (fallingObject != null)
        {
            fallingObject.Init(_transforms, _timeManager);
        }
    }
}