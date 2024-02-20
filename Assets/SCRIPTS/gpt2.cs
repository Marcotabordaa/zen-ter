using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpt2 : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    public LayerMask solidObjectsLayer;

    private Animator animator;
    private Vector2 moveInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
        // Solo permitir el movimiento si el personaje no está ya en movimiento
        if (!isMoving)
        {
            // Obtener las entradas de movimiento del jugador
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(horizontalInput, verticalInput).normalized;
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);

            // Si el jugador está moviéndose
            if (horizontalInput != 0 || verticalInput != 0)
            {
                Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f);

                if(IsWalkable(inputDirection))
                   StartCoroutine(Move(inputDirection));
            }
        }
    }

    // Coroutine para el movimiento suave del personaje
    IEnumerator Move(Vector3 inputDirection)
    {
        // Indicar que el personaje está en movimiento
        isMoving = true;

        // Calcular la posición objetivo hacia la que se va a mover el personaje
        Vector3 targetPos = transform.position + inputDirection;

        // Mientras el personaje no ha alcanzado la posición objetivo
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // Mover suavemente el personaje hacia la posición objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Asegurar que el personaje llegue exactamente a la posición objetivo
        transform.position = targetPos;

        // Indicar que el personaje ha terminado de moverse
        isMoving = false;
    }

    private bool IsWalkable(Vector3 inputDirection)
    {
        // Convertir la posición actual del personaje a un Vector2
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        // Convertir inputDirection a un Vector2
        Vector2 inputDir2D = new Vector2(inputDirection.x, inputDirection.y);

        // Realizar un OverlapCircle para verificar si hay algún objeto sólido en la dirección del input
        if (Physics2D.OverlapCircle(currentPosition + inputDir2D, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

}

