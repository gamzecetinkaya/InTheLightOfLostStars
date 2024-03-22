using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_box;
    [SerializeField] string[] lines;
    [SerializeField] float text_speed;
    [SerializeField] GameObject Dialog_Panel;
    private int index;
    public bool IsItOver = false;
   
    void Start()
    {
        text_box.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (text_box.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text_box.text = lines[index];
            }

        }

    }
    

        void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()

    {
        foreach (char c in lines[index].ToCharArray())
        {
            text_box.text += c;
            yield return new WaitForSeconds(text_speed);

        }

        
    }

    
   public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text_box.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            IsItOver = true; //****** true idi
            gameObject.SetActive(false);
        }
    }

  


}
