using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSelector : MonoBehaviour
{
    public GameObject shownImage;
    public GameObject indexObject;
    public int player;
    public int controller = -1;

    private HatIndex index;
    private int current;
    private Image img;
    private float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        index = indexObject.GetComponent<HatIndex>();
        img = shownImage.GetComponent<Image>();
        index.Set(player, index.objects[0]);
    }

    private void Update()
    {
        if(controller!=-1)
        {
            if (Input.GetAxis("jHorizontal"+(controller+1))>=0.5&&Time.time>nextTime)
            {
                NextSelection();
                nextTime = Time.time + 0.2f;
            }
            else if (Input.GetAxis("jHorizontal" + (controller+1)) <= -0.5 && Time.time > nextTime)
            {
                LastSelection();
                nextTime = Time.time + 0.2f;
            }
        }
    }

    public void LastSelection()
    {
        current--;
        if (current < 0)
        {
            current = index.images.Length - 1;
        }
        img.sprite = index.images[current];
        index.Set(player, index.objects[current]);
    }

    public void NextSelection()
    {
        current++;
        if (current > index.images.Length - 1)
        {
            current = 0;
        }
        img.sprite = index.images[current];
        index.Set(player, index.objects[current]); 
    }
}
