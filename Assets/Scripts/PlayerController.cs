using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject heldBlock;
    public float health = 100;
    public bool throwing = false;
    [SerializeField]
    float speed = 2f, jumpForce = 10f, maxVelocityX = 10, ySpeedLimit = 10, blockpower = 10, minpower = 10, maxpower = 30, damageTimer, damageInterval = 0.2f;
    [SerializeField]
    TileController tc;
    AimLine AL;
    ReloadScene rs;
    public LayerMask groundLayer;
    public Rigidbody2D rb, blockrb;
    public FixedJoint2D joint;
    public SpriteRenderer sR;
    // Start is called before the first frame update
    void Start()
    {
        rs = FindObjectOfType<ReloadScene>();
        rb = GetComponent<Rigidbody2D>();
        tc = FindObjectOfType<TileController>();
        AL = GetComponentInChildren<AimLine>();
        joint = GetComponent<FixedJoint2D>();
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(damageTimer >= 0)
        {
            if(damageTimer >= damageInterval)
            {
                sR.color = Color.white;
                damageTimer = 0;
            }
            damageTimer += Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector2.right * speed;
        }
        else if (IsGrounded() && rb.velocity.y < 0.1f && rb.velocity.y > -0.1f)
        {
            rb.velocity *= .75f;
        }
        if (rb.velocity.x > maxVelocityX)
        {
            rb.velocity = new Vector2 (maxVelocityX, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxVelocityX)
        {
            rb.velocity = new Vector2(-maxVelocityX, rb.velocity.y);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            if (Input.GetMouseButtonUp(1) && blockrb != null)
            {
                blockpower = minpower;
                throwing = false;
                AL.Clear(true);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && rb.velocity.y <= ySpeedLimit)
        {
            rb.velocity += Vector2.up * jumpForce;
        }
        if (Input.GetMouseButton(1) && blockrb != null)
        {
            AL.ColorLerp(Color.white, Color.red, blockpower/maxpower);
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (point - (Vector2)heldBlock.transform.position).normalized;
            AL.Throwing(direction * blockpower, blockrb.mass, rb.velocity);
            if (blockpower < maxpower)
                blockpower += Time.deltaTime * 1000;
            throwing = true;
        }
        if(Input.GetMouseButtonUp(1) && blockrb != null)
        {
            joint.enabled = false;
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (point - (Vector2)heldBlock.transform.position).normalized;
            blockrb.AddForce(blockpower * direction, ForceMode2D.Impulse);
            heldBlock.layer = 0;
            heldBlock = null;
            blockrb = null;
            tc.currentBlock = null;
            blockpower = minpower;
            throwing = false;
        }
        if (health <= 0)
        {
            rs.End(false);
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down * .6f, .5f, groundLayer))
            return true;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + Vector3.down * .6f, .5f);
    }

    public void Damage(float damage)
    {
        health -= damage;
        damageTimer = 0.01f;
        sR.color = Color.red;
    }

    public IEnumerator PickupBlock(GameObject block, bool setMass)
    {
        heldBlock = block;
        heldBlock.transform.rotation = Quaternion.identity;
        heldBlock.transform.position = transform.position + Vector3.up * 1.5f;
        heldBlock.layer = 8;
        blockrb = heldBlock.GetComponent<Rigidbody2D>();
        if (setMass)
            blockrb.mass = tc.mass[tc.currentBlock];
        blockrb.velocity = rb.velocity;
        blockrb.angularVelocity = 0;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        joint.enabled = true;
        joint.connectedBody = blockrb;
    }
}
