using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayerFeet : MonoBehaviour
{
    public float angleMin;
    public float angleMax;
    public float rotationSpeed;
    private float rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        //rotationSpeed = 2f;
        rotationDirection = rotationSpeed;
        rotationSpeed = Mathf.Abs(rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        Quaternion rotMin = Quaternion.Euler(new Vector3(0, 0, angleMin));
        Quaternion rotMax = Quaternion.Euler(new Vector3(0, 0, angleMax));

        Quaternion rotation = transform.rotation;
        /*if(rotationSpeed < 0)
            Debug.Log(string.Format("z: {0}, min: {1} , max: {2}", rotation.z, rotMin.z, rotMax.z));*/

        if (rotation.z <= rotMin.z)
        {
            rotationDirection = rotationSpeed;
        }

        if (rotation.z >= rotMax.z)
        {
            rotationDirection = -rotationSpeed;
        }
        
        transform.Rotate(new Vector3(0, 0, rotationDirection));
    }

}
