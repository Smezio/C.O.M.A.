using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public string[] sentences;
    public float typewriterTime;

    private TextMeshProUGUI textDisplayed;
    private int index = 0;

    private void Start()
    {
        textDisplayed = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplayed.text == sentences[index])
        {
            if (Input.GetKeyDown(KeyCode.Space))
                NextSentence();
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplayed.text += letter;
            yield return new WaitForSeconds(typewriterTime);
        }
    }

    public void NextSentence()
    {
        textDisplayed.text = "";
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(Type());
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public string[] GetSentences()
    {
        return sentences;
    }

    public int GetIndex()
    {
        return index;
    }

    public TextMeshProUGUI GetTextDisplayed()
    {
        return textDisplayed;
    }
}
