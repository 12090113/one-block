using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private float speed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float scrollSpeed = 0.1f;
    private Camera cam;
    private float scroll;
    void Start()
    {
        cam = GetComponent<Camera>();
        scroll = cam.orthographicSize;
        player = FindObjectOfType<PlayerController>().transform;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, speed);
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0f)
        {
            scroll += -cam.orthographicSize * Input.mouseScrollDelta.y * scrollSpeed;
            scroll = Mathf.Clamp(scroll, 3, 20);
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, scroll, 0.1f);
    }
}