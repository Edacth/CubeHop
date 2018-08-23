namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;

    public class PlayerController : MonoBehaviour
    {
        public CharacterController controller;
        private float speed = 0.1f;
        private float gravity = -1.5f;
        private int jumpTime;
        private IEnumerator coroutine;
        public Vector3 characterMovement;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            //Character Movement
            characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            //Debug.Log(characterMovement);
            //Normalize the movement when no input
            if (characterMovement != Vector3.zero)
            {
                characterMovement.Normalize();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpTime = 1;
            }
            if (jumpTime > 0)
            {
                coroutine = Countdown(0.5f);
                StartCoroutine(coroutine);
                characterMovement.y = 3.0f;


            }
            applyGravity();
            controller.Move(characterMovement * speed);
        }

        private IEnumerator Countdown(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            jumpTime--;
            Debug.Log(jumpTime);

        }

        void applyGravity()
        {
            characterMovement.y += gravity;
        }
    }

    
}
