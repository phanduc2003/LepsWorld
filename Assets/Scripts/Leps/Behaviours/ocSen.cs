using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocSen : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float leftBound, rightBound;
    [SerializeField] bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = transform.localPosition.x;
        if(positionX >= rightBound)
        {
            isLeft= true;
        }
        else if (positionX <= leftBound)
        {
            isLeft = false;
        }
        transform.Translate(
            (isLeft ? Vector3.left : Vector3.right) * speed * Time.deltaTime);
        Vector2 scale = transform.localScale;
        scale.x *= (isLeft == scale.x < 0) ? -1 : 1;
        transform.localScale = scale;  
        
    }
}
