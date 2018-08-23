using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 0.1f;
    private float gravity = -0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Character Movement
        Vector3 characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        //Debug.Log(characterMovement);
            //Normalize the movement when no input
        if (characterMovement != Vector3.zero)
        {
            characterMovement.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterMovement.y = 1.0f;
            
        }
        

    }
}
