using System;
using System.Collections;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    
    private CapsuleCollider2D _capsuleCollider;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public Vector2 lastGroundedPosition;
    [SerializeField] private float _tickTime = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _deathLayer;
    private void OnEnable()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    { StartCoroutine(TickCoroutine()); }


    void Update()
    { isGrounded = Physics2D.OverlapCapsule(_capsuleCollider.bounds.center, _capsuleCollider.bounds.size, CapsuleDirection2D.Horizontal, 0, _groundLayer); }
    
    private IEnumerator TickCoroutine()
    { while (true) { yield return new WaitForSeconds(_tickTime); CheckOnTick(); } }
    
    
    private void CheckOnTick()
    { if (isGrounded) { lastGroundedPosition = transform.position; } }
}
