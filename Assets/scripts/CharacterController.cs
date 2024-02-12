using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private float inputMovement;
    private bool isLookingRight = true;
    private bool isOnFloor = false;
    public LayerMask surfaceLayer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        ProcessingMovement();
        ProcessingJump();
        isOnFloor = CheckingFloor();
    }

    private void ProcessingMovement()
    {
        inputMovement = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(inputMovement * speed, rigidBody.velocity.y);
        CharacterOrientation(inputMovement);
    }

    private void CharacterOrientation(float inputMovement)
    {
        if ((isLookingRight && inputMovement < 0) || (!isLookingRight && inputMovement > 0))
        {
            isLookingRight = !isLookingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}

    private void ProcessingJump()
    {
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }


        private bool CheckingFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center, //Origen de la caja
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), //Tamaño
            0f, //Ángulo
            Vector2.down, //Direccion hacia la que va la caja
            0.2f, //Distancia a la que aparece la caja
            surfaceLayer //Layer mask
        );
        return raycastHit.collider != null; //Devuelvo un valor siempre que no sea nulo
    }
}