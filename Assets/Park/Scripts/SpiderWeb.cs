using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    float m_dieTime;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        m_dieTime += Time.deltaTime;

        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerScript = collision.GetComponent<PlayerScript>();

            playerScript.SetMoveSpeed(1.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerScript = collision.GetComponent<PlayerScript>();

            playerScript.SetMoveSpeed(1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerScript = collision.GetComponent<PlayerScript>();

            playerScript.SetMoveSpeed(3.0f);
        }
    }

    void Initialize()
    {
        m_dieTime = 0.0f;
    }

    void Die()
    {
        if (m_dieTime >= 10.0f)
        {
            Destroy(gameObject);
        }
    }
}
