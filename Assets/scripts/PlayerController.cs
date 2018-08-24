namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;

    public class PlayerController : MonoBehaviour
    {
        public CharacterController controller;
        public Text VelText;
        private float speed = 0.2f;
        private float gravity = -0.1f;
        private int jumpTime;
        private IEnumerator coroutine;
        public Vector3 characterMovement;
        // Use this for initialization
        void Start()
        {
            coroutine = Countdown(0.1f);
            StartCoroutine(coroutine);
            
        }

        // Update is called once per frame
        void Update()
        {

            //Character Movement
            characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), characterMovement.y, 0);
            //Debug.Log(characterMovement);
            //Normalize the movement when no input
            if (characterMovement != Vector3.zero)
            {
               // characterMovement.Normalize();
            }

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                //jumpTime = 5;

                characterMovement.y = 1.0f;
            }

            /*
            if (jumpTime > 0)
            {
                characterMovement.y = 1.5f;
            }
            applyGravity();
            */
            VelText.text = (characterMovement.ToString());
            controller.Move(characterMovement * speed);
            
        }

        private IEnumerator Countdown(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                if (controller.isGrounded == false)
                {
                    characterMovement.y += gravity;
                    Debug.Log(characterMovement.y);
                }
                
                
                
               
                
                /*
               if (jumpTime > 0)
               {
                   jumpTime--;
                   Debug.Log(jumpTime);
               }
               */
            }

        }

        void applyGravity()
        {

            characterMovement.y += gravity;
            Debug.Log(jumpTime);
        }
    }

    
}
