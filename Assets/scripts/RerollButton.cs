using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollButton : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Rerolling == true)
        {
            image.color = new Color(1.0f,1.0f,1.0f,0.7f);
        }
        else
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
