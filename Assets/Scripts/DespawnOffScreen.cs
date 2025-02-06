using UnityEngine;

public class DespawnOffScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1f)
        {
            Destroy(gameObject);
        }   
    }
}
