using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMovement : MonoBehaviour
{

    //Used to check key presses for dash.
    private KeyCode moveKey;
    private KeyCode oldMoveKey;

    private float timeSinceKeyPress;
    public float dashForce = 50;

    public float dashCharge = 100;
    public float dashChargeRate = 1f;
    private float dashChargeTimer;
    public float doubleTapTime = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Handle movement tech.
        moveKey = GetMovementKey();

        //If the user pushed an interesting key, then check it for the dash or quick descent.
        if (moveKey != KeyCode.Z)
        {
            if (!(GetComponent<RigidbodyFirstPersonController>().Grounded) && moveKey == KeyCode.Space)
            {
                this.GetComponent<Rigidbody>().AddRelativeForce(0, -dashForce, 0, ForceMode.VelocityChange);
            }

            if (Time.time - timeSinceKeyPress < doubleTapTime && moveKey == oldMoveKey)
            {
                //Left Dash
                if (moveKey == KeyCode.A)
                {
                    this.GetComponent<Rigidbody>().AddRelativeForce(-dashForce, 0, 0, ForceMode.VelocityChange);
                }

                //Back Dash
                else if (moveKey == KeyCode.S)
                {
                    this.GetComponent<Rigidbody>().AddRelativeForce(0, 0, -dashForce, ForceMode.VelocityChange);

                }

                //Right Dash
                else if (moveKey == KeyCode.D)
                {
                    this.GetComponent<Rigidbody>().AddRelativeForce(dashForce, 0, 0, ForceMode.VelocityChange);

                }

                //Front dash
                else if (moveKey == KeyCode.W)
                {
                    this.GetComponent<Rigidbody>().AddRelativeForce(0, 0, dashForce, ForceMode.VelocityChange);

                }
            }

            timeSinceKeyPress = Time.time;
            oldMoveKey = moveKey;
        }
    }

    /* This is used to get which movement key is pressed.*/
    KeyCode GetMovementKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            return KeyCode.A;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return KeyCode.D;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            return KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return KeyCode.Space;
        }

        return KeyCode.Z;
    }

}