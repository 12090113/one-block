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
    float x = 1.01f;
    //(point - (Vector2)transform.position).normalized)
    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponentInParent<PlayerController>();
        positions = new Vector3[length];
        lr.positionCount = length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            lr.positionCount = 0;
        }
    }

    public void Throwing(Vector2 force, float mass)
    {
        transform.position = PC.heldBlock.transform.position;
        positions = new Vector3[length];
        lr.positionCount = length;
        Vector3 pos = Vector3.zero;
        Vector3 vel = force / mass;
        positions[0] = pos;
        for (int i = 1; i < positions.Length; i++)
        {
            float time = i * kirbyiness;
            //Vector3 vel = new Vector3(initialVelocity.x * time, .5f * -9.8f * Mathf.Pow(time, 2) + initialVelocity.y * time, 0);
            vel += Vector3.down * 9.8f * time;
            Debug.Log(vel + " " + time);
            pos = pos + vel * time;
            positions[i] = pos;
        }
        lr.SetPositions(positions);
    }
}
