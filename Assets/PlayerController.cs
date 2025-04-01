using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        
        TouchHandler.Instance.OnTouch += Move;
    }

    private void Move(Vector3 direction) { transform.position = new Vector3(direction.x, transform.position.y, transform.position.z); }
}
