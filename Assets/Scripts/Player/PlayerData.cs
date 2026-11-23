using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour
{

    
    #region INPUT ACTIONS - MOVEMENT
        [Header("Input Settings")]
        public InputActionReference moveAction;
        public InputActionReference crouchAction;  
        public InputActionReference runAction;
        public InputActionReference jumpAction;
        #endregion
    
    #region INPUT ACTIONS - BOOK
        [SerializeField] private InputActionReference openBookAction;
        [SerializeField] private InputActionReference interactAction;
        // [SerializeField] private BookManager book;
    #endregion
    
    #region VARIABLES/REFERENCES - MOVEMENT
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        [SerializeField] private Animator animator;
        [SerializeField] private EAreaType areaType;
        private Rigidbody2D _rb;
        private Vector2 _moveVector;
        private bool _isMoving;
        private bool _isWalking;
        private bool _isRunning;
        private bool _isCrouching;
        private bool _isSliding;
        
        [Header("Movement Settings")]
        public float walkSpeed = 5f;
        public float jumpForce = 5f;
        [Header("Multipliers")]
        public float runMultiplier = 1.5f;
        public float crouchMultiplier = 0.5f;

        //player data
        private PlayerData _data;
    
        //=====EXTERNAL MOVEMENT=====//
    
        //slide
        public float slideForce = 1.2f;
        public float slideMultiplier = 1.2f;
        [Space(10)] public float slideMaxSpeed = 10f;
        [HideInInspector] public bool isSliding;

        //external force
        [HideInInspector] public Vector2 externalDirection = Vector2.zero;
        [HideInInspector] public float externalSpeed = 0f;
    #endregion
    
    
    [HideInInspector] public EDirection facingDirection;
    [HideInInspector] public bool _isSpeaking = false; 

    #region ACTIONS

        
        public event Action<bool> OnToggleBook;
    #endregion

    #region ANIMATIONS

        private static readonly int AnimDirection = Animator.StringToHash("Direction"); //value for topDown

        private static readonly int AnimIsWalking = Animator.StringToHash("IsWalking");
        private static readonly int AnimIsRunning = Animator.StringToHash("IsRunning");
        private static readonly int AnimIsJumping = Animator.StringToHash("IsJumping");
        private static readonly int AnimIsCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int AnimIsSliding = Animator.StringToHash("IsSliding");
        private static readonly int AnimIsSpeaking = Animator.StringToHash("IsSpeaking");

    #endregion
    
    private bool _isBookOpen;


    #region SETUP
    void Start()
    {
        _rb             = GetComponent<Rigidbody2D>();
        groundCheck     = GetComponentInChildren<GroundCheck>();
        facingDirection = EDirection.Right; //can only go left or right
    }
    #endregion

    private void Update()
    {
        if (openBookAction.action.triggered) { OnToggleBook?.Invoke(!_isBookOpen); _isBookOpen = !_isBookOpen; }
        //SIDE SCROLLER
        // if (_data.isSpeaking) { FreezeMovement(); }
        // else
        // {
        //     if (jumpAction.action.WasPressedThisFrame() && groundCheck.isGrounded)  { Jump(); }
        //     if (crouchAction.action.IsPressed() && groundCheck.isGrounded)          { Crouch(); }
        //     _moveVector = moveAction.action.ReadValue<Vector2>();
        //     _isMoving = _moveVector != Vector2.zero;
        //     
        //     //=====MOVEMENT STATES=====//
        //     if (_isMoving) { StopAllCoroutines(); _isWalking = true; } else { StartCoroutine(WaitBeforeStopWalking()); }
        //     _isRunning = runAction.action.IsPressed();
        //     _isCrouching = crouchAction.action.IsPressed();
        // }
        //
        // //=====ANIMATIONS=====//
        //
        // _animator.SetBool(AnimIsSpeaking, _data.isSpeaking);
        // _animator.SetBool(AnimIsWalking, _isWalking);
        // _animator.SetBool(AnimIsRunning, _isRunning);
        // _animator.SetBool(AnimIsCrouching, _isCrouching);
        // _animator.SetBool(AnimIsSliding, isSliding);
        // _animator.SetBool(AnimIsJumping, !groundCheck.isGrounded);
        // if (!_isMoving) { _animator.StopPlayback(); }
        // else
        // {
        //     if (Mathf.Abs(_moveVector.x) > 0.01f) { _spriteRenderer.flipX = _moveVector.x < 0; }
        //     _data.facingDirection = _moveVector.ToDirectionSideScroll(); 
        // }
        
        //TOP DOWN
        // if (_data.isSpeaking) { FreezeMovement(); }
        // else
        // {
        //     if (crouchAction.action.IsPressed() && groundCheck.isGrounded) { Crouch(); }
        //
        //     _moveVector = moveAction.action.ReadValue<Vector2>();
        //     _isMoving = _moveVector != Vector2.zero;
        //
        //
        //     //=====MOVEMENT STATES=====//
        //     if (_isMoving) { StopAllCoroutines(); _isWalking = true; } else { StartCoroutine(WaitBeforeStopWalking()); }
        //     _isRunning = runAction.action.IsPressed();
        //     _isCrouching = crouchAction.action.IsPressed();
        // }
        //
        // //=====ANIMATIONS=====//
        //
        // _animator.SetBool(AnimIsSpeaking, _data.isSpeaking);
        // _animator.SetBool(AnimIsWalking, _isWalking);
        // _animator.SetBool(AnimIsRunning, _isRunning);
        // _animator.SetBool(AnimIsCrouching, _isCrouching);
        // _animator.SetBool(AnimIsSliding, isSliding);
        // if (!_isMoving) { _animator.StopPlayback(); }
        // else
        // {
        //     if (Mathf.Abs(_moveVector.x) > 0.01f) { _spriteRenderer.flipX = _moveVector.x < 0; }
        //     _data.facingDirection = _moveVector.ToDirectionTopDown(); 
        //     _animator.SetInteger(AnimDirection, (int) _data.facingDirection);
        // }
    }


    private void FixedUpdate()
{

    #region MOVEMENT

    if (_isSpeaking || _isBookOpen)
    {
        _rb.linearVelocity = Vector2.zero;
        return;
    }

    float speed = MovementHelpers.GetSpeed(_isWalking, _isRunning, _isCrouching, walkSpeed, runMultiplier,
        crouchMultiplier);

    if (areaType == EAreaType.SideScroller)
    {
        float lastDir = 0;
        if (Mathf.Abs(_moveVector.x) > 0.01f)
        {
            lastDir = Mathf.Sign(_moveVector.x);
        }


        if (isSliding)
        {
            if (_moveVector != Vector2.zero)
            {
                _rb.AddForce(_moveVector * (speed * (slideForce * slideMultiplier)));
            } //accelerate slide
            else
            {
                _rb.AddForce(new Vector2(lastDir * speed * slideForce, 0f));
            } //decelerate slide

            if (Mathf.Abs(_rb.linearVelocity.x) > slideMaxSpeed)
            {
                _rb.linearVelocity = new Vector2(Mathf.Sign(_rb.linearVelocity.x) * slideMaxSpeed,
                    _rb.linearVelocity.y);
            } //max slide speed

            return;
        }

        _rb.linearVelocity = new Vector2(_moveVector.x * speed, _rb.linearVelocity.y) +
                             (externalDirection * externalSpeed);
    }

    if (areaType == EAreaType.TopDown)
    {
        if (isSliding)
        {
            if (_moveVector != Vector2.zero)
            {
                _rb.AddForce(_moveVector * (speed * slideMultiplier));
            } //accelerate slide
            else
            {
                _rb.AddForce(-_rb.linearVelocity * slideMultiplier);
            } //decelerate slide

            if (_rb.linearVelocity.magnitude > slideMaxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * slideMaxSpeed;
            } //max slide speed

            return;
        }

        _rb.linearVelocity = (_moveVector * speed) + (externalDirection * externalSpeed); //normal speed

    }
    #endregion

}

    
#region ACTIONS

private void Jump()
{
    switch (areaType)
    {
        case EAreaType.TopDown: break;
        case EAreaType.SideScroller: 
            groundCheck.isGrounded = true;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            break;
        default: break;
    }
}
private void Crouch() //crouch action, change collider size
{
    switch (areaType)
    {
        case EAreaType.TopDown:      break;
        case EAreaType.SideScroller: break;
        default: break;
    }
}
#endregion
    


#region HELPERS

    private IEnumerator WaitBeforeStopWalking()
    {
        if (_isMoving) { yield break; }
        yield return new WaitForSeconds(0.07f);
        _isWalking = false;
    }

    private void FreezeMovement() //for example, when talking
    {
        _moveVector = Vector2.zero;
        _isMoving = false;
        _isWalking = false;
        _isRunning = false;
        _isCrouching = false;
            
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f; 
        externalDirection = Vector2.zero;
        externalSpeed = 0f;
    }

#endregion
}

//====================OLD CODE - SIDE SCROLLER====================//

//if it's used like this, it will **ALWAYS** stop moving when sliding
// { _rb.AddForce(new Vector2(-_rb.linearVelocity.x * slideMultiplier, 0f)); } 