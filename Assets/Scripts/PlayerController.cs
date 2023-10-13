using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // to be able to use classes of textmesh
using UnityEngine.SceneManagement; //to be able to interact with TextMesh Pro elements.
using UnityEngine.UI; // to abe to interact with button



public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
    public float turnspeed;
    public float horizontalInput;
    public float forwardInput;
    public GameObject projectilePrefab;
    public ParticleSystem getScoreParticale;
    public float jumpForce = 10;
    public float gravityModifire;
    public bool isOnGround = true;
    public float bound = 5;
    public bool gameOver = false;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public bool hasPowerUp = false;
    public GameObject powerupIndicator;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restatrButton;
    public GameObject titleScreen;

    private Rigidbody playerRb;
    private AudioSource playerAudio;
    private Animator playerAnim;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
        
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifire;  // change the gravity
        titleScreen.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // make player animation normolize when jump
            playerAnim.SetTrigger("Jump_trig");
            // when player jump, jumpsound will play
            playerAudio.PlayOneShot(jumpSound, 0.20f);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (gameOver == false)
        {
            // we will movw the player forward and right
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Rotate(Vector3.up * turnspeed * horizontalInput * Time.deltaTime);
            powerupIndicator.transform.position = transform.position + new Vector3(0,2.5f,0);
        }
        if (transform.position.z > bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        // transform.rotation = new Vector3(how i can stop rotating)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }


    }
    private void OnTriggerEnter(Collider other) // when player collide whith powerup
    {
        UpdateScore(10);
        if (other.CompareTag("PowerUP"))
        {
            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true); // show that the player has power up whuth ballon
            Destroy(other.gameObject);

            StartCoroutine(PowerUpCountdownRotin()); // when get power up start to count
        } }
    IEnumerator PowerUpCountdownRotin()
    {
        yield return new WaitForSeconds(5); // has power up for 5 seconds
        powerupIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
    }
        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
       else if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
      {
            
            Debug.Log("Has Power Up"); // when player has power up
        }
        else if (collision.gameObject.CompareTag("Enemy") )
        {

            Debug.Log("Game Over");
            gameOver = true;
            GameOverText();
            // make animation of death

            playerAnim.SetBool("Death_b", true);
            playerAnim.GetInteger("DeathType_int");
            //Destroy(.gameObject.CompareTag("Enemy"))
            titleScreen.gameObject.SetActive(true);


        }
        
        else if (collision.gameObject.CompareTag("Chick"))
        {
            UpdateScore(5);
            // make shape of particle to get score
            getScoreParticale.Play();
          //  Debug.Log("YOu Save 5 Score");
            // when player collid whith chick crash sound will play
            playerAudio.PlayOneShot(crashSound, 0.20f);
            Destroy(collision.gameObject);
        }
        }
   public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOverText()
    {
        restatrButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }
    public void ReStartGame() // to be able to reload our scene
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    }


 

