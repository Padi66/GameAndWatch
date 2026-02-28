using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] private Transform[] m_transform;
    [SerializeField] private InputPlayerManagerCustom m_inputManager;
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;

    private int m_currentindex = 2;
    private int m_mouveSpeed = 1;

    // Variables swipe
    [SerializeField] private float swipeThreshold = 50f;
    private Vector2 startTouchPosition;
    private bool isSwiping = false;

    private void OnEnable()
    {
        m_inputManager.OnMoveLeft += MoveToPrevPosition;
        m_inputManager.OnMoveRight += MoveToNextPosition;
    }

    private void OnDisable()
    {
        m_inputManager.OnMoveLeft -= MoveToPrevPosition;
        m_inputManager.OnMoveRight -= MoveToNextPosition;
    }

    private void Start()
    {
        m_currentindex = 2;
        UpdatePosition();
    }

    private void Update()
    {
        HandleSwipe();
    }

    // Détection du swipe mobile
    private void HandleSwipe()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Moved:
                if (!isSwiping) return;

                float deltaX = touch.position.x - startTouchPosition.x;

                if (Mathf.Abs(deltaX) > swipeThreshold)
                {
                    if (deltaX > 0)
                        MoveToNextPosition();
                    else
                        MoveToPrevPosition();

                    isSwiping = false;
                }
                break;

            case TouchPhase.Ended:
                isSwiping = false;
                break;
        }
    }

    public void MoveToNextPosition()
    {
        m_currentindex += m_mouveSpeed;
        m_currentindex = Mathf.Clamp(m_currentindex, 0, m_transform.Length - 1);
        UpdatePosition();
        _audioEventDispatcher?.PlayAudio(AudioType.PlayerMovement);
    }

    public void MoveToPrevPosition()
    {
        m_currentindex -= m_mouveSpeed;
        m_currentindex = Mathf.Clamp(m_currentindex, 0, m_transform.Length - 1);
        UpdatePosition();
        _audioEventDispatcher?.PlayAudio(AudioType.PlayerMovement);
    }

    public void MoveToDirection(int direction)
    {
        m_currentindex = m_currentindex + m_mouveSpeed * direction;
        m_currentindex = Mathf.Clamp(m_currentindex, 0, m_transform.Length - 1);
        UpdatePosition();
        _audioEventDispatcher?.PlayAudio(AudioType.PlayerMovement);
    }

    public void UpdatePosition()
    {
        transform.position = m_transform[m_currentindex].position;
    }
}
