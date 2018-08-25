namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;

    public class PlayerController : MonoBehaviour
    {
        //public CharacterController controller;

        //Delaring Text objects and junk
        private Text VelTextObject;
        private Text PosTextObject;
        private Text HighPointTextObject;
        public Text velText;
        public Text posText;
        public Text highPointText;
        private Canvas _canvas;
        public Canvas canvas;

        //Declare the rest of them
        float highestPoint = 0;
        private float speed = 0.7f;
        private float gravity = -4.5f;
        private IEnumerator coroutine;
        public Vector3 characterMovement;
        public GameObject player;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        public Rigidbody rigidbody;
        float distToGround = 0.5f;

        // Use this for initialization
        void Start()
        {
            //Starts the coroutine that increments gravity
            coroutine = Countdown(0.1f);
            //StartCoroutine(coroutine);

            //Creates the canvas for UI
            _canvas = Instantiate<Canvas>(canvas);
            VelTextObject = Instantiate<Text>(velText, _canvas.transform);
            PosTextObject = Instantiate<Text>(posText, _canvas.transform);
            HighPointTextObject = Instantiate<Text>(highPointText, _canvas.transform);

        }

        // Update is called once per frame
        void Update()
        {

            //Character Movement
            characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                //characterMovement.y = 0.25f;
                rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 22.25f, rigidbody.velocity.z);
            }

            if (Input.GetKeyDown(KeyCode.F) && isGrounded())
            {
                highestPoint = 0.0f;
            }

            if (player.transform.position.y > highestPoint)
            {
                highestPoint = player.transform.position.y;
            }

            if (characterMovement.y > -1.0f)
            {
                characterMovement.y += gravity * Time.deltaTime;
            }

            


            if (rigidbody.velocity.y < 0)
            {
                characterMovement += Vector3.up * gravity * (fallMultiplier - 1) * Time.deltaTime;
                
            }
            rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
            characterMovement.x = characterMovement.x * speed;
            characterMovement.z = characterMovement.z * speed;
            rigidbody.AddForce(characterMovement, ForceMode.Impulse);

            //Update the UI text
            VelTextObject.text = ("Velocity " + characterMovement.ToString());
            PosTextObject.text = ("Position " + player.transform.position.ToString());
            HighPointTextObject.text = ("Time " + Time.deltaTime);

        }

        private IEnumerator Countdown(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                if (isGrounded() == false)
                {
                    applyGravity();
                    Debug.Log(characterMovement.y);
                }
            }

        }

        void applyGravity()
        {
            if (characterMovement.y > -1.0f)
            {
                characterMovement.y += gravity;
            }
            
            
        }

        bool isGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }
    }

    
}
