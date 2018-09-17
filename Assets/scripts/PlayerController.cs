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
        private Rigidbody rb;
        public Vector3 characterMovement;
        public float fallMultiplier;
        public float lowJumpMultiplier = 2f;

        public float speed;
        public float jumpStrength;
        float distToGround = 0.5f;

        // Use this for initialization
        void Start()
        {
            
           
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            //Creates the canvas for UI
            _canvas = Instantiate<Canvas>(canvas);
            VelTextObject = Instantiate<Text>(velText, _canvas.transform);
            PosTextObject = Instantiate<Text>(posText, _canvas.transform);
            HighPointTextObject = Instantiate<Text>(highPointText, _canvas.transform);

            //fallMultiplier = 3.0f;
            //speed = 2.2f;
            //jumpStrength = 10f;
        }

        void FixedUpdate() // Update
        {

            
            characterMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0); //Get horizonal input

            
            if (isLeftWalled() && characterMovement.x < 0) //Stop player movement if they are againt a wall
            {
                characterMovement.x = 0;
            }
            if (isRightWalled() && characterMovement.x > 0) //Stop player movement if they are againt a wall
            {
                characterMovement.x = 0;
            }

            //Checks if the player is grounded then jumps when space is pressed
            if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded())
            {
                rb.velocity = Vector3.up * jumpStrength;
                //rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            //Applies force to the player
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            characterMovement.x = characterMovement.x * speed;
            characterMovement.z = characterMovement.z * speed;

            //Add the force to the player
            rb.AddForce(characterMovement, ForceMode.Impulse);

            //Update the UI text
            VelTextObject.text = ("Velocity " + rb.velocity.ToString());
            PosTextObject.text = ("Position " + gameObject.transform.position.ToString());
            HighPointTextObject.text = ("" + isRightWalled());

        }

        bool isGrounded()
        {
            
            if ((Physics.Raycast(new Vector3(transform.position.x + 0.49f, transform.position.y, transform.position.z), -Vector3.up, distToGround + 0.1f))
                || (Physics.Raycast(new Vector3(transform.position.x - 0.49f, transform.position.y, transform.position.z), -Vector3.up, distToGround + 0.1f)))
            {

                return true;
            }
            return false;
        }

        bool isLeftWalled()
        {
            RaycastHit hit1;
            RaycastHit hit2;
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.49f, transform.position.z), -Vector3.right, out hit1, distToGround + 0.01f);
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + -0.49f, transform.position.z), -Vector3.right, out hit2, distToGround + 0.01f);
            if (hit1.collider != null || hit2.collider != null)
            {
                //Debug.Log("Found");
                if (hit1.collider.tag == "green" && hit2.collider.tag == "green")
                {

                    return false;
                }
                return true;
            }
            return false;
        }

        bool isRightWalled()
        {
            RaycastHit hit1;
            RaycastHit hit2;
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.49f, transform.position.z), -Vector3.left, out hit1, distToGround + 0.01f);
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + -0.49f, transform.position.z), -Vector3.left, out hit2, distToGround + 0.01f);
            if (hit1.collider != null || hit2.collider != null)
            {
                Debug.Log(hit1.collider.tag);
                if (hit1.collider.tag == "green" && hit2.collider.tag == "green")
                {

                    return false;
                }
                return true;
            }
            return false;
        }
    }

    
}
