using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; //do predkosci gracza
    public Transform orientation; //do orientacji gracza

    public float groundDrag; //zmienna do obliczen zwiazanych z terenem

    public float jumpForce; //zmienna na sile skoku
    public float airMultiplier; //zmienna do obliczen predkosci ruchu podczas skoku

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight; //zmienna na wysokosc
    public LayerMask whatIsGround; // przechowuje teren
    bool grounded; //czy dotykamy zmiei

    float horizontalInput; //do wczytywania z klawiatury
    float verticalInput;   //do wczytywania z klawiatury

    Vector3 moveDirection; //kierunek ruchu

    Rigidbody rb; // referencja do rigidbody

    //public float rotationSpeed = 5.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //powiazanie zmiennej z komponentem
        rb.freezeRotation = true;  //zablokowanie rotacji rigidbody
    }

    private void Update()
    {
        //sprawdzenie gdzie jest ziemia metoda promieniowa
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //"wystrzelenie" promienia w dol
        

        MyInput(); // wywolanie metody
        SpeedControl();

        if (grounded) //sprawdza czy dotykamy ziemi
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //float Angle = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        //transform.Rotate(Vector3.up, Angle);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); //dostep
        verticalInput = Input.GetAxisRaw("Vertical");

        //float moveAngle = horizontalInput * rotationSpeed * Time.deltaTime;
        //orientation.Rotate(Vector3.up, moveAngle);

        //transform.rotation = Quaternion.LookRotation(moveDirection);
        //transform.Rotate(verticalInput,0);

        if (Input.GetKey(jumpKey) && grounded) //czy skaczemy
        {
            Jump(); //wywolanie skoku
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; // obliczenie ruchu zaleznego od orientacji tzn. poruszamy sie zawsze tam gdzie patrzymy
        

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force); //poruszenie ciala fiz podczas ruchu
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force); //poruszenie ciala fiz podczas skoku

       
        
    }

    private void SpeedControl() // ograniczenie predkosci
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //predkosc gracza

        if(flatVel.magnitude > moveSpeed) // ograniczenie predkosci jezeli jest wieksza niz docelowa
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // czy predkosc w osi y wynosi 0

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); // dodanie "impulsu" w osi Y w celu skoku
    }

}
