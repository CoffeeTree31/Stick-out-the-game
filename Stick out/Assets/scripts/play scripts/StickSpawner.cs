using System.Collections;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public float maxtime = 1;
    public GameObject stick;
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            Instantiate(stick, new Vector2(-5, Random.Range(-1.35f, 1.35f)), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }

    }

    
}
