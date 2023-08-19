using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMUMD : MonoBehaviour
{
    public float top, bottom;
    public bool isTop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var npcY = transform.position.y;

        if (npcY > top)
        {
            isTop = false;
        }
        if (npcY < bottom)
        {
            isTop = true;
        }
        if (isTop)
        {
            transform.Translate(new Vector3(0, Time.deltaTime * 2, 0));
        }
        else
        {
            transform.Translate(new Vector3(0, -Time.deltaTime * 2, 0));
        }
    }
}
