using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private TMP_Text ballCountText = null;
    [SerializeField] private List<GameObject> balls = new List<GameObject>();

    [SerializeField] private float moveSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalLimit;
    private float horizontal;
    private int gateNumber;
    private int targetCount;

    void Update()
    {
        HorizontalBallMove();
        ForwardBallMove();
        UpdateBallCountText();
    }

    private void HorizontalBallMove()
    {
        float newX;

        if (Input.GetMouseButton(0))
        {
            horizontal = Input.GetAxisRaw("Mouse X");
        }
        else
        {
            horizontal = 0;
        }
        newX = transform.position.x + horizontal * horizontalSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, -horizontalLimit, horizontalLimit);

        transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
    }

    private void ForwardBallMove()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void UpdateBallCountText()
    {
        ballCountText.text = balls.Count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BallStack"))
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count - 1].transform.localPosition.z - 1f);
            balls.Add(other.gameObject);
        }
        if (other.gameObject.CompareTag("Gate"))
        {
            gateNumber = other.gameObject.GetComponent<GateController>().GetGateNumber();
            targetCount = balls.Count + gateNumber;

            if (gateNumber > 0)
            {
                IncreaseBallCount();
            }
            else if(gateNumber < 0)
            {
                DecreaseBallCount();
            }
        }
    } 
    private void IncreaseBallCount()
    {
        for(int i = 0; i < gateNumber; i++)
        {
            GameObject newBall = Instantiate(ballPrefab);
            newBall.transform.SetParent(transform);
            newBall.GetComponent<SphereCollider>().enabled = false;
            newBall.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count - 1].transform.localPosition.z - 1f);
            balls.Add(newBall);
        }
    }
    private void DecreaseBallCount()
    {
        for(int i = balls.Count - 1; i >= targetCount; i--)
        {
            balls[i].SetActive(false);
            balls.RemoveAt(i);
        }
    }
}
