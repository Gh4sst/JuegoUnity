using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidBody;
    private float inputMovement;
    private BoxCollider2D boxCollider;
    public bool isLookingRight = true, isOnFloor = false;
    public LayerMask surfaceLayer;
    public float jumpspeed;
    private int jumpsLeft = 2; // Contador de saltos restantes
    Animator animator;
    public bool isRunning;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        ProcessingMovement();
        ProcessingJump();
        isOnFloor = CheckingFloor();

    }
    bool CheckingFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
                                        boxCollider.bounds.center, //Origen de la caja
                                        new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), //Tamaño
                                        0f, //Ángulo
                                        Vector2.down, //Direccion hacia la que va la caja
                                        0.2f, //Distancia a la que aparece la caja
                                        surfaceLayer//Layer mask
                                        );
        return raycastHit.collider != null; //Devuelvo un valor siempre que no sea nulo
    }

    void ProcessingJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                if (isOnFloor)
                {
                    // Salto desde el suelo
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpspeed);
                }
                else
                {
                    // Doble salto en el aire
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpspeed);
                }
                jumpsLeft--;
            }
        }
    }

    void ProcessingMovement()
    {
        //Movement logic
        inputMovement = Input.GetAxis("Horizontal");
        isRunning = inputMovement != 0 ? true : false;
        animator.SetBool("isRunning", inputMovement != 0);
        rigidBody.velocity = new Vector2(inputMovement * speed, rigidBody.velocity.y);
        CharacterOrientation(inputMovement);
    }

    void CharacterOrientation(float inputMovement)
    {
        if ((isLookingRight && inputMovement < 0) || (!isLookingRight && inputMovement > 0))
        {
            isLookingRight = !isLookingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Restablecer saltos si colisiona con el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            jumpsLeft = 2;
        }
    }
}