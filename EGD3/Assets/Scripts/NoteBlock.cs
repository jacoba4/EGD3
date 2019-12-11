using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlock : MonoBehaviour
{
    public float blocktime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BlockTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BlockTime()
    {
        yield return new WaitForSeconds(blocktime);
        Destroy(gameObject);
    }
}
