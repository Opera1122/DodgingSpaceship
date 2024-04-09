using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    float originalSpeed;
    public GameObject flame;
    public GameObject speedUpFlame;
    public ParticleSystem explosion;
    public GameObject shield;
    IEnumerator boosterCoroutine;
    public bool isShieldArmed = false;

    private BoxCollider boxCollider;
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boosterCoroutine = Booster();
        originalSpeed = speed;
        playerRb = GetComponent<Rigidbody>();

        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float rightInput = Input.GetAxis("Horizontal");

        Move();
        MoveClamp();
        //playerRb.AddForce(mainCamera.transform.forward * speed * forwardInput);
        //playerRb.AddForce(mainCamera.transform.right * speed * rightInput);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
    }

    void DestroyEvent()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        GameManager.instance.End();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DestroyEvent();
        }

        ItemEvent(collision);
    }

    public void ItemEvent(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power Up"))
        {
            StopBooster();
            StopCoroutine(boosterCoroutine);
            boosterCoroutine = Booster();
            StartCoroutine(boosterCoroutine);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Shield Armed"))
        {
            ArmShield();
        }
    }

    IEnumerator Booster()
    {
        StartBooster();
        yield return new WaitForSeconds(10f);
        StopBooster();
    }

    public void ArmShield()
    {
        isShieldArmed = true;
        shield.SetActive(true);
        boxCollider.size = new Vector3(0.1f, 0.1f, 0.1f);
        
    }

    public void UnArmedShield()
    {
        isShieldArmed = false;
        shield.SetActive(false);
        boxCollider.size = new Vector3(1.5f, 3f, 2f);
    }

    void StartBooster()
    {
        speed += 20;
        speedUpFlame.SetActive(true);
        flame.SetActive(false);
    }

    void StopBooster()
    {
        speed = originalSpeed;
        speedUpFlame.SetActive(false);
        flame.SetActive(true);
    }

    void MoveClamp()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.0f) pos.x = 0.0f;
        if (pos.x > 0.9999f) pos.x = 0.9999f;
        if (pos.y < 0.01f) pos.y = 0.01f;
        if (pos.y > 1.0f) pos.y = 1.0f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        /*
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 player = transform.position;
        player.x = Mathf.Clamp(player.x, left.x + 0.8F, right.x - 0.8f);
        player.y = Mathf.Clamp(player.y, left.y + 1, right.y - 1);
        transform.position = player;*/
    }
}
