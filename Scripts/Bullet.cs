using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 3;
    Vector3 direction;

    private void Start()
    {
        this.GetComponent<Collider>().enabled = true;

        Vector3 dirCam = Camera.main.transform.forward;
        transform.forward = dirCam;
    }
    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    void FixedUpdate()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        speed += 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Destroy(gameObject);   
        }
    }
}
