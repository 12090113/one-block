using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    PlayerController PC;
    [SerializeField]
    float kirbyiness;
    [SerializeField]
    int length;
    [SerializeField]
    LineRenderer lr;
    Vector3[] positions;
    bool clear = false;
    void Start()
    {
        PC = GetComponentInParent<PlayerController>();
        positions = new Vector3[length];
        lr.positionCount = length;
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(1) && Time.timeScale != 0)
        {
            Clear();
        }
    }

    private void FixedUpdate()
    {
        if (clear)
        {
            Clear();
            clear = false;
        }
    }

    public void Throwing(Vector2 force, float mass, Vector2 vel)
    {
        transform.position = PC.heldBlock.transform.position;
        positions = new Vector3[length];
        lr.positionCount = length;
        Vector3 initialVelocity = force / mass + vel;
        for (int i = 1; i < positions.Length; i++)
        {
            float time = i * kirbyiness;
            Vector3 pos = new Vector3(initialVelocity.x * time, .5f * Physics2D.gravity.y * Mathf.Pow(time, 2) + initialVelocity.y * time, 0);
            positions[i] = pos;
        }
        lr.SetPositions(positions);
    }

    public void ColorLerp(Color start, Color end, float duration)
    {
        lr.startColor = Color.Lerp(start, end, duration);
        lr.endColor = lr.startColor;
    }

    public void Clear(bool paused = false)
    {
        if (!paused)
            lr.positionCount = 0;
        else
            clear = true;
    }
}
