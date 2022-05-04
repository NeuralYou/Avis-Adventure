using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontal, vertical;
    public bool canRun = true;
    public bool hasStopped = false;
    Vector2 previousPosition;
    List<Vector2> prevPositions;
    Vector2 vectorFromBrain;

    [SerializeField] float speedModifier;
    Rigidbody2D rb;

    public void InitSelf()
    {
        prevPositions = new List<Vector2>();
        rb = GetComponent<Rigidbody2D>();
        previousPosition = transform.localPosition;
    }

    
    void Update()
    {
        ApplyMovement(vectorFromBrain);
    }



    public void ApplyMovement(Vector2 i_Input)
	{
        //Vector2 movement = i_Input * speedModifier;
        Vector2 movement = GetInput() * speedModifier;
		rb.AddForce(movement);

	}


    public Vector2 GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }
}