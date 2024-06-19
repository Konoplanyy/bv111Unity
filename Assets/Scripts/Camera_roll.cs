using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_roll : MonoBehaviour
{
    public Transform _targetTransform;
    public Vector3 _offset;
    public float _CamSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        var nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offset, Time.fixedDeltaTime * _CamSpeed);
        transform.position = nextPosition;
    }
}
