using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instanse;
    public static int Points { get; private set; }
    public static bool gameOver = false;

    [SerializeField]
    private Cube cubePref;
    public TextMeshProUGUI pointsText;

    private Vector2 tapPosition;
    private bool isMobile;
    private Cube last;
    private GameObject cubesParent;
    private int count;

    private void Awake()
    {
        if (Instanse == null)
            Instanse = this;
    }

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
        StartGame();
    }
    void Update()
    {
        if(gameOver)
        {
            gameOver = false;
            pointsText.text = "GAME OVER";
            Time.timeScale = 0;
            StartCoroutine(RestartGame());
        }
        if (!last.isReleased)
        {

            if (!isMobile)
            {
                if (Input.GetMouseButton(0))
                {
                    tapPosition = Input.mousePosition;
                    Move(true);
                }
                else
                    Move(false);
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    tapPosition = Input.GetTouch(0).position;
                    Move(true);
                }
                else
                    Move(false);
            }
        }
    }

    private void Move(bool flag)
    {
        if (flag)
        {
            last.rb.MovePosition(new Vector3(tapPosition.x / 13 - 43, 50, 125));
            last.isPressed = true;
        }
        else
        {
            if (last.isPressed)
            {
                last.rb.useGravity = true;
                last.rb.AddForce(new Vector3(tapPosition.x / 13 - 33, 55, 8000), ForceMode.Impulse);
                last.isReleased = true;
                count++;
                if (count % 15 == 0)
                {
                    Debug.Log(0);
                    AdBanner.Instanse.ShowInterstitial();
                }
                StartCoroutine(GenerateNewCube());
            }
        }
    }

    public void StartGame()
    {
        SetPoints(0);
        cubesParent = new GameObject("Cubes");
        GenerateCube();
        gameOver = false;
        count = 0;
    }
    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }

    private void GenerateCube()
    {
        last = Instantiate(cubePref, new Vector3(0, 50, 125), transform.rotation);
        last.transform.SetParent(cubesParent.transform);
    }
    
    IEnumerator GenerateNewCube()
    {
        yield return new WaitForSeconds(0.5f);
        GenerateCube();
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        Destroy(GameObject.Find("Cubes"));
        yield return new WaitForSecondsRealtime(0.1f);
        StartGame();
    }
}
