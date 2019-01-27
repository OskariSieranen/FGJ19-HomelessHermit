using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyFeet : MonoBehaviour
{
    public float angleMin;
    public float angleMax;
    public float rotationSpeed = 2f;
    public float enabledState = 1;
    private float rotationDirection;
    private int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = rotationSpeed;
        rotationSpeed = Mathf.Abs(rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(state >= enabledState)
        {
            Move();
        }        
    }

    public void Enable(int newState)
    {
        state = newState;
    }

    private void Move()
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
