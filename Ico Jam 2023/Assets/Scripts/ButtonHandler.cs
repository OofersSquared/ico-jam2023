using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Transform movableObject; // Drag your MovableObject here
    public Transform targetPosition; // Position where the object should move to
    public float moveSpeed = 1.0f; // Speed of movement
    public SpriteRenderer spriteRenderer; // Drag the SpriteRenderer of your object here
    public Sprite newSprite; // Assign the new sprite in the inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box"))
        {
            StartCoroutine(MoveObject());
        }
    }

    private IEnumerator MoveObject()
    {
        spriteRenderer.sprite = newSprite; // Change the sprite when the movement starts

        float journeyLength = Vector3.Distance(movableObject.position, targetPosition.position);
        float startTime = Time.time;

        while (Vector3.Distance(movableObject.position, targetPosition.position) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            movableObject.position = Vector3.Lerp(movableObject.position, targetPosition.position, fractionOfJourney);
            yield return null;
        }


        /*
         *                  INSERT IF YOU WANT PLATFORM TO MOVE BACK GRADUALLY WHEN YOU PRESS BUTTON
         *
         
        startTime = Time.time;
        journeyLength = Vector3.Distance(targetPosition.position, _initialPosition);

        while (Vector3.Distance(movableObject.position, _initialPosition) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            movableObject.position = Vector3.Lerp(targetPosition.position, _initialPosition, fractionOfJourney);
            yield return null;
        }

        spriteRenderer.sprite = _originalSprite; // Change the sprite back when the movement is finished
    }
}

         * 
         * 
         */
    }

}
