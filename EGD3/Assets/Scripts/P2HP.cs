using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P2HP : MonoBehaviour
{   
    public GameObject p2;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = p2.GetComponent<Player>().hp;
        GetComponent<RectTransform>().sizeDelta = new Vector2(hp,100);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Lerp(381, 107, (-350 + hp) / 100), 220.3f, 0);
    }
}
