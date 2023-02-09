using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private string content;
    private Text lb_content;
    // Start is called before the first frame update
    void Awake()
    {
        this.lb_content = this.transform.Find("lb_content").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string name){
        this.content = name;
        this.lb_content.text = name;
    }
}
