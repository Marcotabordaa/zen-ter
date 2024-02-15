using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorpersonaje : MonoBehaviour

    //variables que se necesitan para el movimiento
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private void Update()
        // bloque de codigo para actualizar la posición a lo que obtenga de las teclas de movimiento
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }
    }
    //corrutina
    IEnumerator Move(Vector3 targetPos)
    {
        while ((targetPos-transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
}
}
