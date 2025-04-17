using System;
using UnityEngine;


public class Facula : MonoBehaviour
{
    private float speed;
    private Transform delTransform;
    private void Update()
    {
        this.transform.position = new Vector3(transform.position.x-speed * Time.deltaTime,transform.position.y,transform.position.z);
        if (this.transform.position.x < delTransform.position.x)
        {
            Destroy(this.gameObject);
        }
    }

    public void Init(float _speed,Transform _delTransform)
    {
        speed = _speed;
        this.delTransform = _delTransform;
    }
}
