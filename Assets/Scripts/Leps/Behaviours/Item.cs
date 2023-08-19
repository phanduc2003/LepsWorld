using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = isRight? Vector3.right : Vector3.left;
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    public void SetSpeed(float _speed)
    {
       speed= _speed;
    }

    public void SetDirection (bool _isRight)
    {
        isRight= _isRight;
        Vector2 scale = transform.localScale;
        scale.x *= isRight? 1 : -1;
        transform.localScale = scale;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("npc"))
        {
            Destroy(collision.gameObject);
        }
    }
}
