using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{

    public Transform player;            // Reference to the player's transform
    public float followDistance = 5f;  // Distance at which the enemy starts following the player
    public float moveSpeed = 3f;       // Speed at which the enemy moves

    private bool isFollowing = false;  // Flag to track whether the enemy is following

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within the followDistance
        if (distanceToPlayer < followDistance)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }

        // If the enemy is following, move towards the player
        if (isFollowing)
        {
            // Calculate the direction to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy in the direction of the player
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }
}
