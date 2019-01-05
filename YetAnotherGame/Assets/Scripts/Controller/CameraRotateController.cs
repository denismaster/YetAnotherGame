using UnityEngine;

public class CameraRotateController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    private float moveVertical;
    private float rotateHorizontal;

    // Update is called once per frame
    void Update()
    {
        // get axis angle
        moveVertical = Input.GetAxis("Mouse Y") * speed;
        rotateHorizontal = Input.GetAxis("Mouse X") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame
        moveVertical *= Time.deltaTime;
        rotateHorizontal *= Time.deltaTime;

        //// Move translation along the object's z-axis
        //transform.Translate(0, 0, moveVertical);

        transform.Rotate(moveVertical, rotateHorizontal, 0);
    }
}
