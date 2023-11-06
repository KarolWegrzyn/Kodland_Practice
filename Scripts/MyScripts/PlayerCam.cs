using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX; //czulosc x
    public float sensY; //czulosc y

    public Transform orientation; //orientacja

    float xRotation; //rotacja kamery w osi x
    float yRotation; //rotacja kamery w osi y

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora
        Cursor.visible = false; //ukrycie kursora
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; //inicjalizacja zmiennej do przechowywania ruchu myszki w osi X 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY; //inicjalizacja zmiennej do przechowywania ruchu myszki w osi Y

        yRotation += mouseX; // uwzglednienie ruchu myszki w rotacji kamery
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 50f); // zablokowanie "fiko³ka kamery"

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
