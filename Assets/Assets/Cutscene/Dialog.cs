using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public string[] sentences;
    public float typewriterTime;

    private bool gameOutro;
    public Transform choiceMenu;

    private TextMeshProUGUI textDisplayed;
    private int index = 0;

    private void Start()
    {
        textDisplayed = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Type());

        gameOutro = false;
    }

    private void Update()
    {
        if (choiceMenu.gameObject.activeSelf)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (choiceMenu.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle == FontStyles.Underline)
                {
                    choiceMenu.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                    choiceMenu.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
                }
                else
                {
                    choiceMenu.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
                    choiceMenu.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && gameOutro)
            {
                if (choiceMenu.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle == FontStyles.Underline)
                    SceneManager.LoadScene(2);
                else
                    Application.Quit();
            }
        }

        if (textDisplayed.text == sentences[index] && !choiceMenu.gameObject.activeSelf)
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
            if (SceneManager.GetActiveScene().name.Equals("IntroScene"))
                SceneManager.LoadScene(2);
            else
            {
                choiceMenu.gameObject.SetActive(true);
                gameOutro = true;
            }
                
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
