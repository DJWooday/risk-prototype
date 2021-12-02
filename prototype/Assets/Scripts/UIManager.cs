using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static string playerName;
    public void SetName(string n) {
        playerName = n;
        Debug.Log(playerName);
    }
    public void StartGame() {
        SceneManager.LoadScene(1);
    }
}
