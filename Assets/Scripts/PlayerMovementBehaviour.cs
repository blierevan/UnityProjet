using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public Rigidbody2D Rigidbody;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;

    public Transform RaycastOriginDown;
    public Transform RaycastOriginDownLeft;
    public Transform RaycastOriginDownRight;

    public Transform RaycastOriginLeft;
    public Transform RaycastOriginLeftUp;
    public Transform RaycastOriginLeftDown;

    public Transform RaycastOriginRight;
    public Transform RaycastOriginRightUp;
    public Transform RaycastOriginRightDown;

    public LayerMask GroundMask;
    public float RaycastDistance;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(RaycastOriginDown.position, Vector2.down, RaycastDistance, GroundMask);
        RaycastHit2D hitL = Physics2D.Raycast(RaycastOriginDownLeft.position, Vector2.down, RaycastDistance, GroundMask);
        RaycastHit2D hitR = Physics2D.Raycast(RaycastOriginDownRight.position, Vector2.down, RaycastDistance, GroundMask);
        _isGrounded = hit.collider != null || hitL.collider != null || hitR.collider != null;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            SpriteRenderer.flipX = false;
            RaycastHit2D hit1 = Physics2D.Raycast(RaycastOriginRight.position, Vector2.right, RaycastDistance, GroundMask);
            RaycastHit2D hit2 = Physics2D.Raycast(RaycastOriginRightUp.position, Vector2.right, RaycastDistance, GroundMask);
            RaycastHit2D hit3 = Physics2D.Raycast(RaycastOriginRightDown.position, Vector2.right, RaycastDistance, GroundMask);
            if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
                Rigidbody.velocity = new Vector2(Speed, Rigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            SpriteRenderer.flipX = true;
            RaycastHit2D hit1 = Physics2D.Raycast(RaycastOriginLeft.position, Vector2.left, RaycastDistance, GroundMask);
            RaycastHit2D hit2 = Physics2D.Raycast(RaycastOriginLeftUp.position, Vector2.left, RaycastDistance, GroundMask);
            RaycastHit2D hit3 = Physics2D.Raycast(RaycastOriginLeftDown.position, Vector2.left, RaycastDistance, GroundMask);
            if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
                Rigidbody.velocity = new Vector2(-Speed, Rigidbody.velocity.y);
        }
        else
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Rigidbody.AddForce(Vector2.up * JumpForce);
            Animator.SetTrigger("Jump");
        }

        Animator.SetBool("IsGrounded", _isGrounded);
        Animator.SetFloat("velocityX", Mathf.Abs(Rigidbody.velocity.x));
        Animator.SetFloat("velocityY", Rigidbody.velocity.y);
    }
}
