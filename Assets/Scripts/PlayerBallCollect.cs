using UnityEngine;
using System.Collections;

public class PlayerBallCollect : MonoBehaviour {

    public bool hasBall = false;
    public float throwStrength = 10f;

    private float maxCharge = 2.5f;
    private float currentCharge = 0;

    private bool charging = false;

    private Color normalColor = new Color(0, 0, 0);
    private Color chargedColor = new Color(.33f, .33f, 0);

    public float startThrowStrength = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        //Let user go if escape is pushed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Make GUI visible

            //Time.timeScale = 0;
        }

        //Lock mouse.
        if (Input.GetMouseButtonDown(0))
        {
            //Allow user to rejoin if left.
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //Handle ball actions
        if (hasBall)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasBall = false;
                GameObject ball = this.gameObject.transform.Find("Ball").gameObject;
                ball.transform.parent = this.transform.Find("MainCamera").transform;
                ball.transform.localPosition = new Vector3(0, 0, 2);

                charging = true;

                StartCoroutine("ChargeAndThrow", ball);
            }

            //Handle dribble
            if (Input.GetMouseButtonDown(1))
            {

            }
        }
    }

    //Grab the ball if collides with it.
    void OnCollisionEnter(Collision col)
    {
        //If we collided with the ball, then this player now owns the ball.
        //      if (col.gameObject.name == "Ball")

        Ball ball = col.collider.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Debug.Log("Ball received");
            //Make this player now the parent of the ball.
            col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            col.gameObject.transform.parent = this.transform;
            
            //This stops it from moving stupidly. Makes it not handle collisions.
            //Also allows the ball to properly follow the player.
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //Puts ball in front of player.
            col.gameObject.transform.localPosition = new Vector3(0,0,2);
            hasBall = true;
        }
    }

    private IEnumerator ChargeAndThrow(GameObject ball)
    {
        while (charging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);

            Color chargeColor = Color.Lerp(normalColor, chargedColor, currentCharge/maxCharge);

            ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", chargeColor);

            Debug.Log(currentCharge);

            yield return null;

            if (Input.GetMouseButtonUp(0))
            {
                charging = false;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.transform.parent = null;
//                Debug.Log((ball.transform.position - transform.position).normalized * throwStrength * (currentCharge / maxCharge + startThrowStrength));
                ball.GetComponent<Rigidbody>().AddForce((ball.transform.position - transform.position).normalized * throwStrength * (currentCharge / maxCharge + startThrowStrength), ForceMode.VelocityChange);
                currentCharge = 0;
                ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", normalColor);

            }
        }
    }
}
