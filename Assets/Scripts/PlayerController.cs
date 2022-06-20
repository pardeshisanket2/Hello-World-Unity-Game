using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;

    public GameObject gameWonPanel;
    public GameObject gameLostPanel;
    public GameObject gamePausePanel;
    public Button continueButton;
    public float speed;
    public float maxSpeed;
    public float acceleration;

    private bool isGameOver;
    private bool isGamePaused = false;


    void Start()
    {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(ContinueOnClick);

    }

    void Update()
    {
        if (isGameOver == true)
        {
            return;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            gamePausePanel.SetActive(true);
            isGamePaused = true;
            rigidbody2d.velocity = new Vector2(0f, 0f);
        }

        if (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
        }


        if (Input.GetAxis("Horizontal") > 0)  //it is positive
        {
            rigidbody2d.velocity = new Vector2(speed, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0) // it is negative
        {
            rigidbody2d.velocity = new Vector2(-speed, 0f);
        }

        else if (Input.GetAxis("Vertical") > 0)  //it is positive
        {
            rigidbody2d.velocity = new Vector2(0f, speed);
        }
        else if (Input.GetAxis("Vertical") < 0) // it is negative
        {
            rigidbody2d.velocity = new Vector2(0f, -speed);
        }
        // else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        else if (Input.GetButtonDown("Jump"))
        {
            //stop
            rigidbody2d.velocity = new Vector2(0f, 0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            Debug.Log("Level Completed!!!");
            gameWonPanel.SetActive(true);
            isGameOver = true;

        }

        else if (other.tag == "Enemy")
        {
            Debug.Log("Game Over!!!");
            gameLostPanel.SetActive(true);
            isGameOver = true;

        }
    }

    void ContinueOnClick()
    {
        gamePausePanel.SetActive(false);
        isGamePaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //In your game, theres is only one level so SceneManager.GetActiveScene().buildIndex = 0th level
    }




}
