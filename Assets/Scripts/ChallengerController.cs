using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengerController : MonoBehaviour, Interactable
{
    public void interact()
    {
        Debug.Log("Challenger");
        SceneManager.LoadScene("Turnbase");
    }
}
