using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public static bool isPlaying = true;
    public Vector3 NW, NE, SW, SE;
    public float growSpeed = 0.1f;
    public float spawnRate = 2.0f;
    public float debrisTorque = 10f;
    public float debrisForce = 3f;
    public float minSize = 0.5f;
    public float maxSize = 1.0f;

    private bool gameOverProcessed = false;

    public Camera mainCamera;
    public GameObject[] spawners, missAreas, debrisList;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.SetInt("CurrentMiss", 0);
        isPlaying = true;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if(PlayerPrefs.GetInt("CurrentMiss") >= 3 && !gameOverProcessed)
            {
                isPlaying = false;
                gameOverScreen.SetActive(true);

                if(PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("1st"))
                {
                    PlayerPrefs.SetInt("5th", PlayerPrefs.GetInt("4th"));
                    PlayerPrefs.SetInt("4th", PlayerPrefs.GetInt("3rd"));
                    PlayerPrefs.SetInt("3rd", PlayerPrefs.GetInt("2nd"));
                    PlayerPrefs.SetInt("2nd", PlayerPrefs.GetInt("1st"));
                    PlayerPrefs.SetInt("1st", PlayerPrefs.GetInt("CurrentScore"));
                }
                else if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("2nd"))
                {
                    PlayerPrefs.SetInt("5th", PlayerPrefs.GetInt("4th"));
                    PlayerPrefs.SetInt("4th", PlayerPrefs.GetInt("3rd"));
                    PlayerPrefs.SetInt("3rd", PlayerPrefs.GetInt("2nd"));
                    PlayerPrefs.SetInt("2nd", PlayerPrefs.GetInt("CurrentScore"));
                }
                else if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("3rd"))
                {
                    PlayerPrefs.SetInt("5th", PlayerPrefs.GetInt("4th"));
                    PlayerPrefs.SetInt("4th", PlayerPrefs.GetInt("3rd"));
                    PlayerPrefs.SetInt("3rd", PlayerPrefs.GetInt("CurrentScore"));
                }
                else if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("4th"))
                {
                    PlayerPrefs.SetInt("5th", PlayerPrefs.GetInt("4th"));
                    PlayerPrefs.SetInt("4th", PlayerPrefs.GetInt("CurrentScore"));
                }
                else if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("5th"))
                {
                    PlayerPrefs.SetInt("5th", PlayerPrefs.GetInt("CurrentScore"));
                }

                gameOverProcessed = true;
            }

            mainCamera.orthographicSize += growSpeed * Time.deltaTime;
            minSize = 0.1f * mainCamera.orthographicSize;
            maxSize = 0.2f * mainCamera.orthographicSize;

            NW = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
            NE = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            SW = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            SE = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
            spawners[0].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, mainCamera.nearClipPlane)) + new Vector3(0, 1, 0), Quaternion.identity);
            spawners[0].transform.localScale = new Vector3(3.2f * mainCamera.orthographicSize, 0.1f);
            spawners[1].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane)) + new Vector3(1, 0, 0), Quaternion.identity);
            spawners[1].transform.localScale = new Vector3(0.1f, 1.8f * mainCamera.orthographicSize);
            spawners[2].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane)) + new Vector3(0, -1, 0), Quaternion.identity);
            spawners[2].transform.localScale = new Vector3(3.2f * mainCamera.orthographicSize, 0.1f);
            spawners[3].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane)) + new Vector3(-1, 0, 0), Quaternion.identity);
            spawners[3].transform.localScale = new Vector3(0.1f, 1.8f * mainCamera.orthographicSize);

            missAreas[0].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, mainCamera.nearClipPlane)) + new Vector3(0, 0.6f * mainCamera.orthographicSize, 0), Quaternion.identity);
            missAreas[0].transform.localScale = new Vector3(4.8f * mainCamera.orthographicSize, 0.1f);
            missAreas[1].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane)) + new Vector3(0.6f * mainCamera.orthographicSize, 0, 0), Quaternion.identity);
            missAreas[1].transform.localScale = new Vector3(0.1f, 3.2f * mainCamera.orthographicSize);
            missAreas[2].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane)) + new Vector3(0, -0.6f * mainCamera.orthographicSize, 0), Quaternion.identity);
            missAreas[2].transform.localScale = new Vector3(4.8f * mainCamera.orthographicSize, 0.1f);
            missAreas[3].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane)) + new Vector3(-0.6f * mainCamera.orthographicSize, 0, 0), Quaternion.identity);
            missAreas[3].transform.localScale = new Vector3(0.1f, 3.2f * mainCamera.orthographicSize);
        }
    }

    IEnumerator Spawn()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(spawnRate);
            int debrisType = Random.Range(0, debrisList.Length);
            int spawnerType = Random.Range(0, spawners.Length);
            GameObject go = Instantiate(debrisList[debrisType]);
            go.transform.position = new Vector3(Random.Range(spawners[spawnerType].transform.position.x - spawners[spawnerType].transform.lossyScale.x / 2, spawners[spawnerType].transform.position.x + spawners[spawnerType].transform.lossyScale.x / 2), Random.Range(spawners[spawnerType].transform.position.y - spawners[spawnerType].transform.lossyScale.y / 2, spawners[spawnerType].transform.position.y + spawners[spawnerType].transform.lossyScale.y / 2));
            float size = Random.Range(minSize, maxSize);
            go.transform.localScale = new Vector3(size, size);
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.AddTorque(Random.Range(-debrisTorque, debrisTorque), ForceMode2D.Impulse);
            switch (spawnerType)
            {  
                case 0:
                    rb.AddForce(new Vector2(0, -debrisForce), ForceMode2D.Impulse);
                    break;
                case 1:
                    rb.AddForce(new Vector2(-debrisForce, 0), ForceMode2D.Impulse);
                    break;
                case 2:
                    rb.AddForce(new Vector2(0, debrisForce), ForceMode2D.Impulse);
                    break;
                default:
                    rb.AddForce(new Vector2(debrisForce, 0), ForceMode2D.Impulse);
                    break;
            }
        }
    }

    public void RestartPressed()
    {
        isPlaying = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitPressed()
    {
        SceneManager.LoadScene(0);
    }
}
