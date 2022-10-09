using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameStart;
    public static bool wonGame;
    public InputAction startAction;
    public InputAction resetAction;
    public static GameObject henry;
    public static GameObject gameOverPanel;
    public static bool isDead;
    public TMP_Text tutorialText;
    public TMP_Text winText;
    public ParticleSystem winParticles;
    // Start is called before the first frame update
    void Awake()
    {
        gameStart = false;
        wonGame = false;
        henry = GameObject.Find("henry");
        gameOverPanel = GameObject.Find("Game");
        isDead = false;
        tutorialText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);

        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

    private void OnEnable()
    {
        startAction.Enable();
        startAction.performed += StartGame;

        resetAction.Enable();
        resetAction.performed += ResetGame;
    }

    private void OnDisable()
    {
        startAction.Disable();
        startAction.performed -= StartGame;

        resetAction.Disable();
        resetAction.performed -= ResetGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame(InputAction.CallbackContext context)
    {

        if (gameStart)
        {
            return;
        }

        gameStart = true;
        tutorialText.gameObject.SetActive(false);
        StartCoroutine(GetComponent<WordManager>().NextLevel());

    }

    void ResetGame(InputAction.CallbackContext context)
    {
        if (isDead)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (wonGame)
        {
            SceneManager.LoadScene(0);
            return;
        }

    }

    public static IEnumerator KillHenry()
    {
        gameStart = false;
        isDead = true;
        henry.GetComponent<Henry>().Die();
        Debug.Log("Henry's dead!");
        yield return new WaitForSeconds(3f);
        henry.GetComponent<Henry>().dedPanel.SetActive(true);

    }

    public IEnumerator Win()
    {
        while (true)
        {
            winText.gameObject.SetActive(true);
            Instantiate(winParticles, transform.position, Quaternion.Euler(-90, 0, 0));
            yield return new WaitForFixedUpdate();
        }
    }


}
