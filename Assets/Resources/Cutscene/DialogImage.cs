using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogImage : MonoBehaviour
{
    public Sprite[] sprites;

    private int index;
    private Dialog dialog;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        image = GetComponent<Image>();
        dialog = GameObject.Find("DialogText").GetComponent<Dialog>();

        //image.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        //if (dialog.GetTextDisplayed().text == dialog.GetSentences()[dialog.GetIndex()])
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //        NextImage();
        //}

        int index = dialog.GetIndex() < sprites.Length - 1 ? dialog.GetIndex() : sprites.Length - 1;
        image.sprite = sprites[index];
    }

    //private void NextImage()
    //{
    //    if (index < sprites.Length - 1)
    //        index++;
    //}
}
