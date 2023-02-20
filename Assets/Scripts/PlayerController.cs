using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;
    public int health = 5;
    public Text healthText;
    private int originalHealth;
    private int originalScore;
    public float speed = 10.0f; //(acceleration)
    public float maxSpeed = 20.0f;
    public float deceleration = 35.0f;
    private Vector3 velocity = Vector3.zero;
    public Text winLoseText;
    public Image winLoseBG;

    private void Start()
    {
        originalHealth = health;
        originalScore = score;
    }

    private void Update()
    {
        if (health <= 0) //player loss check, shows loss screen, resets player and scene
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseBG.gameObject.SetActive(true); // enable WinLoseBG element
            StartCoroutine(LoadScene(3.0f)); //delay
            health = originalHealth;
            score = originalScore;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        SceneManager.LoadScene("Menu");
        }
        //movement
        if (direction.magnitude > 0)
        {
            velocity += direction * speed * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            velocity -= velocity.normalized * deceleration * Time.deltaTime;
            if (velocity.magnitude < 0.2f)
            {
                velocity = Vector3.zero;
            }
        }

        transform.position += velocity * Time.deltaTime;
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
