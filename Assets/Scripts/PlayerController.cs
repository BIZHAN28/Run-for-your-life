using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float jumpForce = 5f;
    public CharacterController characterController;
    public Transform cameraHolder;

    public float cameraDistance = 5f;
    public float cameraHeight = 2f;

    public int maxHealth = 100;
    private int currentHealth;
    public Text healthText;

    private float verticalSpeed = 0f;
    private float verticalRotation = 0f;
    public float footStepDelay;
    private float nextFootstep = 0;

    private float gravity = 20f; 
    private bool isJumping = false;
	public GameObject gameOverPanel;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleCamera();
        UpdateHealthText();
        CheckFallingOff();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, horizontal, 0);

        float moveDirectionX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveDirectionZ = Input.GetAxis("Vertical") * moveSpeed;
        
        Vector3 move = transform.TransformDirection(new Vector3(moveDirectionX, 0, moveDirectionZ));

        if (characterController.isGrounded)
        {
            if (!isJumping)
            {
                verticalSpeed = 0f; 
            }

            if (Input.GetKey(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
                isJumping = true;
            }
        }
        else
        {

            verticalSpeed -= gravity * Time.deltaTime;
            isJumping = false; 
        }


        move.y = verticalSpeed;


        characterController.Move(move * Time.deltaTime);

        if ((move.x != 0 || move.z != 0) && characterController.isGrounded)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                nextFootstep += footStepDelay;
            }
        }
    }

    void HandleCamera()
    {
        float vertical = Input.GetAxis("Mouse Y") * lookSpeed;
        verticalRotation -= vertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -30f, 30f);

        Vector3 cameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
        cameraHolder.position = cameraPosition;

        cameraHolder.LookAt(transform.position + Vector3.up * cameraHeight);
        cameraHolder.rotation *= Quaternion.Euler(verticalRotation, 0, 0);
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth;
    }

    void CheckFallingOff()
    {
        if (transform.position.y < -10f)
        {
            GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
		Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
		gameOverPanel.SetActive(true);
		characterController.enabled = false;
    }
}