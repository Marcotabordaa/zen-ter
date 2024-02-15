using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpt2 : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private void Update()
    {
        // Solo permitir el movimiento si el personaje no está ya en movimiento
        if (!isMoving)
        {
            // Obtener las entradas de movimiento del jugador
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Si el jugador está moviéndose
            if (horizontalInput != 0 || verticalInput != 0)
            {
                Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f);
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

}

