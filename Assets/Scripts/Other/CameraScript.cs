using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Camera cam;

    public Transform playerPosition;
    public float smoothSpeed;
    public float threshold;
    public float k_x;
    public float k_y;

    private bool centered;

    public bool shake;
    public float duration;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            centered = !centered;
        }
        */
    }

    void FixedUpdate()
    {
        if (playerPosition != null)
        {
            
            if (centered)
            {
                Vector3 finalPosition = (new Vector3(playerPosition.position.x, playerPosition.position.y, -10));
                transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed);
            }
            else
            {
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 targetPos = new Vector3((mousePos - playerPosition.position).x * k_x, (mousePos - playerPosition.position).y * k_y, -10);

                //targetPos.x = Mathf.Clamp(targetPos.x, -xThreshold * k_x, xThreshold * k_x);
                //targetPos.y = Mathf.Clamp(targetPos.y, -yThreshold * k_y, yThreshold * k_y);
                targetPos = Vector3.ClampMagnitude(targetPos, threshold);

                transform.position = Vector3.Lerp(transform.position, playerPosition.position + targetPos, smoothSpeed);

            }
        }
    }
}
