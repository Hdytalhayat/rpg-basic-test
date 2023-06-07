using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int letterPerSecond;

    public static DialogManager Instance { get; private set; }

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    int currentLine = 0;
    Dialog dialog;

    bool isTyping;

    private void Awake()
    {
        Instance = this;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach(var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }
}
