using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public bool isPlaying = true;
    public Vector3 NW, NE, SW, SE;
    public float growSpeed = 0.1f;
    public float spawnRate = 2.0f;
    public float debrisTorque = 10f;
    public float debrisForce = 3f;

    public Camera mainCamera;
    public GameObject[] spawners, missAreas, debrisList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            mainCamera.orthographicSize += growSpeed * Time.deltaTime;
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

            missAreas[0].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, mainCamera.nearClipPlane)) + new Vector3(0, 3, 0), Quaternion.identity);
            missAreas[0].transform.localScale = new Vector3(4.8f * mainCamera.orthographicSize, 0.1f);
            missAreas[1].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCamera.nearClipPlane)) + new Vector3(3, 0, 0), Quaternion.identity);
            missAreas[1].transform.localScale = new Vector3(0.1f, 3.2f * mainCamera.orthographicSize);
            missAreas[2].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane)) + new Vector3(0, -3, 0), Quaternion.identity);
            missAreas[2].transform.localScale = new Vector3(4.8f * mainCamera.orthographicSize, 0.1f);
            missAreas[3].transform.SetPositionAndRotation(mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane)) + new Vector3(-3, 0, 0), Quaternion.identity);
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
}
