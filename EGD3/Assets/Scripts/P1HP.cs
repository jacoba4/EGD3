using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P1HP : MonoBehaviour
{   
    public GameObject p1;
    int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = p1.GetComponent<Player>().hp;
        GetComponent<RectTransform>().sizeDelta = new Vector2(hp,100);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Lerp(-300,-150, (-350 + hp)/100),230,0);
    }
}
