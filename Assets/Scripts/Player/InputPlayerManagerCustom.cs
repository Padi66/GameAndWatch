using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Action = System.Action; 
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class InputPlayerManagerCustom : MonoBehaviour
{
    public event Action OnMoveLeft;//eventdispatcher Left
    public event Action OnMoveRight;//eventdispatcher Right
    [SerializeField] private float _tapDuration = 0.1f;
    private float _tapTimer = 0.0f;
    private bool _isTouching = false;
    private float width = 0.0f;
    private float height = 0.0f;
    private Vector2 _endPosition;
    private Vector2 _startPosition;
    
    InputAction _tapAction;
    
    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
        
        _tapAction = InputSystem.actions.FindAction("Tap");
    }
    
    private void Update()
    {
        if (Touch.activeTouches.Count <= 0)
        {
            return;
        }
        var touch = _tapAction.ReadValue<Touch>();
        if (touch.phase == TouchPhase.Began)
        {
           Debug.Log("Start");
           _startPosition = touch.screenPosition;
        }else if (touch.phase == TouchPhase.Ended)
        {
           Debug.Log("End");
           _endPosition = touch.screenPosition;
           OnSwipe();
        }
        
           /* if (Input.touchCount > 0)
            {
                Touch firstTouch = Input.GetTouch(0);
                if (firstTouch.phase == TouchPhase.Began)
                { 
                    _isTouching = true;
                }
                else if (firstTouch.phase == TouchPhase.Ended)
                {
                    _isTouching = false;
                    if (_tapTimer <= _tapDuration)
                    {
                        Debug.LogWarning($"Tap ok !! Touch at {firstTouch.position}");
                        if (firstTouch.position.x < width / 2)
                        {
                            Debug.Log("Tap Left!!");
                        }
                        else
                        {
                            Debug.Log("Tap Right!!");
                        }
                    }
                    _tapTimer = 0.0f;
                }

                if (_isTouching)
                {
                    _tapTimer += Time.deltaTime;
                }
                
                
                
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveLeft();
                } 
            }*/

    }
    private void OnSwipe()
    { 
        Vector2 delta = _endPosition - _startPosition;
        delta = delta.normalized;

        float dot = Vector2.Dot(lhs: delta, rhs: Vector2.right);

        if (Mathf.Abs(dot) < 0.7f)
        {
            if (dot < 0.0f)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }
    }
    
    public void MoveLeft()
    {
        OnMoveLeft?.Invoke();//Call de l'eventdispatcher associé 
    }
    
    public void MoveRight()
    {
        OnMoveRight?.Invoke();//Call de l'eventdispatcher associé
    }
}
