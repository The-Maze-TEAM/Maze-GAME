using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Install the package, then include this namespace for Gyro
// For WebGL on iOS, go to Project Settings > Input System Package
// Enable Motion Usage
// Make sure to create a settings asset when prompted.
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
	public float friction;
	public Text scoreText;
	public Text healthText;
	public GameObject endGameContainer;
	private int score = 0;
	private int health = 5;
	private Rigidbody rb;
	// This function pointer will allow us to conditionally determine how to move,
	// Based on input devices available on the current playform. Bitchin'.
	public delegate void InputFuncPtr();
	InputFuncPtr movefunction = null;

	void Start()
	{
		// https://docs.unity3d.com/ScriptReference/RuntimePlatform.html
		// Checks for accelerometer support, or an editor being used. Remove the other two options to get your input back.
		if (SystemInfo.supportsAccelerometer == true)
		{
			// Any sensors used must be manually enabled
			InputSystem.EnableDevice(Accelerometer.current);
			movefunction = new InputFuncPtr(moveMobile);
		}
		else
			movefunction = new InputFuncPtr(moveKbd);
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		movefunction();
	}

	void moveKbd()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (direction != Vector3.zero)
			rb.AddForce(direction * speed);
		else
			rb.velocity *= Mathf.Clamp01(1f - friction * Time.fixedDeltaTime);
	}

	void moveMobile()
	{
		Vector3 tilt, force;
		float currentTiltMagnitude, currentSpeed, tiltAcceleration, previousTiltMagnitude = 0, speedModFactor = 2.5f;
		// Read and adjust the device angle
		tilt = Accelerometer.current.acceleration.ReadValue();
		tilt = Quaternion.Euler(90, 0, 0) * tilt; // Rotate the tilt vector to match the game's orientation
		// tilt.Normalize();


		// Track movement between frames
		currentTiltMagnitude = tilt.magnitude;
		tiltAcceleration = (currentTiltMagnitude - previousTiltMagnitude) / Time.deltaTime;
		previousTiltMagnitude = currentTiltMagnitude;

		// Apply the tilt vector as a force in the X and Z directions, while keeping the player grounded in the Y direction
		force = new Vector3(tilt.x, 0, tilt.y - 0.707f);

		if (force.magnitude > 0.08)
		{
			currentSpeed = speed + tiltAcceleration * speedModFactor;
			rb.AddForce(force * currentSpeed);
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);
		}
		else
			rb.velocity *= Mathf.Clamp01(1f - friction * Time.fixedDeltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            //Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            //Debug.Log("Health: " + health);
        }
        else if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
        else if (other.CompareTag("Walls"))
        {
            Debug.Log("Wall hit");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Walls"))
        {
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }
     IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
