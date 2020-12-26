using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Update is called once per frame
    public float speed;
    public float x;
    public float y;
    public float z;

    // calculate this from the world size too
    void Start()
    {
        speed = 6f;
        x = 12f;
        y = 12f;
        z = -12f;
        transform.position = new Vector3(x, y, z);
    }

    void Update()
    {
        // code for translating
        if (Input.GetKey(KeyCode.D))
        {
            x += (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            y += (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
        // Code for zooming in and out
        if (Input.GetKey(KeyCode.LeftShift))
        {
            z += (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            z -= (speed * Time.deltaTime);
            ActivateBoundedTransform(x, y, z);
        }
    }

    // Bounds the area the camera can move to. *** Calculate these from the world size
    private void ActivateBoundedTransform(float x, float y, float z)
    {
        if (x < -25f)
        {
            x = -25f;
        }
        if (x > 125f)
        {
            x = 125f;
        }
        if (z > -1f)
        {
            z = -1;
        }
        if (y < -25f)
        {
            y = -25f;
        }
        if (y > 125f)
        {
            y = 125f;
        }

        transform.position = new Vector3(x, y, z);
    }


    public void SetPostion(Vector3 location)
    {
        x = location.x;
        y = location.y;
        z = location.z;
    }

}

