using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;

    public int CurrentBalance { get { return currentBalance; } }



    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if(currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void UpdateDisplay()
    {
        displayBalance.text = $"Gold {currentBalance.ToString()}" ;
    }
}
