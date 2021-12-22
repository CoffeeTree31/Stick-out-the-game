using UnityEngine;
using System.Collections;

public class stick : MonoBehaviour
{
    private float rotation;
    private void Start()
    {
        rotation = Random.Range(-0.9f, 0.9f);
    }

    private void Update()
    {
        transform.position += Vector3.right * Random.Range(0.7f, 2.5f) * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0f, 0f, rotation);
        if (transform.position.x > 3.5f)
        {
            Destroy(gameObject);
        }
    }
}
