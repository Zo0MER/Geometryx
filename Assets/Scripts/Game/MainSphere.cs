using UnityEngine;
using System.Collections;

public class MainSphere : MonoBehaviour
{
    public float lowerLevel = 0;
    Vector3 initalPos;
    Vector3 beginPos;
    private LevelState levelState;

    public delegate void IsCanMoveUpdate();

    private bool isCanMove = false;
    public bool IsCanMove
    {
        get { return isCanMove; }
        set
        {
            isCanMove = value;
            if (levelState.PauseUpdated != null)
            {
                levelState.PauseUpdated(!isCanMove);
            }
        }
    }
    public GameObject portal;
    // Use this for initialization
    void Start()
    {
        levelState = FindObjectOfType<LevelState>();
        initalPos = transform.position;
        beginPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCanMove)
        {
            transform.position = initalPos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsCanMove = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector3(100.0f, 0.0f, 0.0f));

        }
        if ((transform.position.y < lowerLevel))
        {
            initalPos = beginPos;
            IsCanMove = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            initalPos = beginPos;
            IsCanMove = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vector = Input.mousePosition;
            vector = Camera.main.ScreenToWorldPoint(vector);
            vector.z = 0;
            if ((Mathf.Abs(vector.x - transform.position.x) <= GetComponent<Collider2D>().bounds.size.x) &&
                (Mathf.Abs(vector.y - transform.position.y) <= GetComponent<Collider2D>().bounds.size.y))
            {
                IsCanMove = true;
                GetComponent<Rigidbody2D>().AddForce(new Vector3(100.0f, 0.0f, 0.0f));
            }
        }
    }
}
