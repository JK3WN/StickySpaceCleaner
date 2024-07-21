using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public bool isPlaying = true;
    public Vector3 NW, NE, SW, SE;
    public float growSpeed = 0.1f;

    public Camera mainCamera;
    public GameObject[] spawners;

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
        }
    }
}
