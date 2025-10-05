using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;          // Maksymalna prędkość
    public float acceleration = 10f;      // Jak szybko przyspiesza
    public float deceleration = 10f;      // Jak szybko zwalnia

    [Header("Building Interaction")]
    public Building collidedBuilding;     // Aktualny budynek w triggerze

    private Rigidbody2D rb;
    private Vector2 inputVector;
    private Vector2 currentVelocity;
    private GameplayInputActions inputact;

    private void Awake()
    {
        inputact = new GameplayInputActions();

        // Wejście do budynku
        inputact.Wandering.Enterbuilding.performed += _ =>
        {
            if (collidedBuilding == null) return;

            inputact.Wandering.Disable();
            inputact.InBuilding.Enable();
            collidedBuilding.Enter();
        };

        // Wyjście z budynku
        inputact.InBuilding.ExitBuilding.performed += _ =>
        {
            if (collidedBuilding == null) return;

            inputact.Wandering.Enable();
            inputact.InBuilding.Disable();
            collidedBuilding.Leave();
        };
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputact.Wandering.Enable();
    }

    private void Update()
    {
        // Pobranie inputu z Input System
        inputVector = inputact.Wandering.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        // Obrót gracza w kierunku ruchu
        if (inputVector.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        // Płynne przyspieszanie i zwalnianie
        Vector2 targetVelocity = inputVector * moveSpeed;

        if (inputVector.magnitude > 0)
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        else
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);
    }

    // Trigger 2D dla wejścia w budynek
    private void OnTriggerEnter2D(Collider2D other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            collidedBuilding = building;
        }
    }

    // Trigger 2D dla wyjścia z budynku
    private void OnTriggerExit2D(Collider2D other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null && collidedBuilding == building)
        {
            collidedBuilding = null;
        }
    }
}
