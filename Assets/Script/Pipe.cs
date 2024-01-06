using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    float speed;
    float distanceBetweenPipes;
    float numberPipes;

    float startPositionY;
    public Collider2D[] colliders;

    void Start()
    {
        GameManager.OnGameEnded += OnGameEnded;

        speed = GameManager.Instance.speedPipes;
        distanceBetweenPipes = GameManager.Instance.distanceBetweenPipes;
        numberPipes = GameManager.Instance.numberPipes;

        startPositionY = transform.position.y;
        transform.position = new Vector3(transform.position.x, startPositionY + Random.Range(-2, 2), transform.position.z);

    }

    private void OnDestroy()
    {
        GameManager.OnGameEnded -= OnGameEnded;
    }

    void OnGameEnded()
    {
        foreach ( var item in colliders)
        {
            item.enabled = false;
        }
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.InGame)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    public void UpdatePosition()
    {
        transform.position = new Vector3(transform.position.x + distanceBetweenPipes * numberPipes, startPositionY, transform.position.z);
        transform.position = new Vector3(transform.position.x, startPositionY + Random.Range(-3, 3), transform.position.z);
    }
}
