using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam; // reference the camera
    public Transform followTarget; // as camera moves, will follow player across screen

    // starting position for the parallax game object
    Vector2 startingPosition;

    // start Z value of parallax game object
    float startingZ;

    // distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPosition; // => means it updates itself every frame; don't need to put it in the update method

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // if object is in front of target, use near clip plane. if behind the target, use farClipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // the further the object from player, the faster the ParallaxEffect object will move. Drag its Z value closer to the target to make it move slower.
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // starting position gives you a vector 3; will remove the z axis in following line
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // when frames update, want to MOVE the position of parallax object based on how FAR the camera has moved from the starting position
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // once you have new position, want to set it on the transform
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ); // only changing the x and y values, not the Z value
        
    }
}
