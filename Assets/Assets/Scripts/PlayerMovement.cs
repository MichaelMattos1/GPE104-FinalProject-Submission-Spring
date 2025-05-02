using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public float speed = 5f;
     public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float wallJumpCooldown { get; set; }
    private Vector2 movement;
    private Vector2 screenBounds;
    private float playerHalfWidth;
    private float xPoslastFrame;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        playerHalfWidth = spriteRenderer.bounds.extents.x;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        ClampMovement();
        FlipCharacterX();

        if (wallJumpCooldown > 0f)
        {
            wallJumpCooldown -= Time.deltaTime;
        }
    }
    private void HandleMovement()
    {

        if (wallJumpCooldown > 0f) return;

        float input = Input.GetAxis("Horizontal");
        movement.x = input * speed * Time.deltaTime;
        transform.Translate(movement);
        
    }

    private void ClampMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
    }

    private void FlipCharacterX()
    {
        float input = Input.GetAxis("Horizontal");
        if (input > 0 && (transform.position.x > xPoslastFrame))
        { 
            spriteRenderer.flipX = false;
        }
        else if (input < 0 && (transform.position.x < xPoslastFrame))
        {
            spriteRenderer.flipX = true;
        }
        xPoslastFrame = transform.position.x;
    }
}
