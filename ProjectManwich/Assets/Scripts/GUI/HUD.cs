using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    private Transform m_player1;
    private Transform m_player2;
    private Transform m_player3;
    private Transform m_player4;

    private int player1HUDMoneyValue = 0;
    private int player2HUDMoneyValue = 0;
    private int player3HUDMoneyValue = 0;
    private int player4HUDMoneyValue = 0;

	void Start ()
    {
        m_player1 = transform.FindChild("Player1");
        m_player2 = transform.FindChild("Player2");
        m_player3 = transform.FindChild("Player3");
        m_player4 = transform.FindChild("Player4");

        Player p1 = PlayerManager.GetPlayer(0);
        if (p1 != null) {
            player1HUDMoneyValue = p1.m_money;
        }

        Player p2 = PlayerManager.GetPlayer(1);
        if (p2 != null) {
            player2HUDMoneyValue = p2.m_money;
        } else {
            EnablePlayerHUD(2, false);
        }

        Player p3 = PlayerManager.GetPlayer(2);
        if (p3 != null) {
            player3HUDMoneyValue = p3.m_money;
        } else {
            EnablePlayerHUD(3, false);
        }

        Player p4 = PlayerManager.GetPlayer(3);
        if (p4 != null) {
            player4HUDMoneyValue = p4.m_money;
        } else {
            EnablePlayerHUD(4, false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Player[] players = PlayerManager.GetPlayers();
        for (int index = 0; index < players.Length; index++) {
            Player p = players[index];
            if (p != null) {
                UpdatePlayerMoney(index);
            }
        }
	}

    private Transform GetPlayerHUDTransform(int playerNumber)
    {
        Transform t = null;
        switch (playerNumber) {
            case 1:
                t = m_player1;
                break;

            case 2:
                t = m_player2;
                break;

            case 3:
                t = m_player3;
                break;

            case 4:
                t = m_player4;
                break;
        }

        return t;
    }

    private int GetCurrentPlayerHUDMoneyValue(int playerNumber)
    {
        switch (playerNumber) {
            default:
            case 1:
                return player1HUDMoneyValue;

            case 2:
                return player2HUDMoneyValue;

            case 3:
                return player3HUDMoneyValue;

            case 4:
                return player4HUDMoneyValue;
        }
    }


    private void SetCurrentPlayerHUDMoneyValue(int playerNumber, int value)
    {
        switch (playerNumber) {
            case 1:
                player1HUDMoneyValue = value;
                break;

            case 2:
                player2HUDMoneyValue = value;
                break;

            case 3:
                player3HUDMoneyValue = value;
                break;

            case 4:
                player4HUDMoneyValue = value;
                break;
        }
    }

    public void EnablePlayerHUD(int playerNumber, bool enable)
    {
        Transform playerHUDTransform = GetPlayerHUDTransform(playerNumber);
        playerHUDTransform.gameObject.SetActive(enable);
    }

    public void ChangePlayerJobString(int playerNumber, string jobName)
    {
        Transform playerHUDTransform = GetPlayerHUDTransform(playerNumber);
        playerHUDTransform.FindChild("PlayerJob").GetComponent<GUIText>().text = "Job: " + jobName;
    }

    public void ChangePlayerMoneyString(int playerNumber, int moneyValue)
    {
        Transform playerHUDTransform = GetPlayerHUDTransform(playerNumber);
        playerHUDTransform.FindChild("PlayerMoney").GetComponent<GUIText>().text = "$$$: " + moneyValue.ToString();
    }

    public void UpdatePlayerMoney(int playerNumber)
    {
        Player p = PlayerManager.GetPlayer(playerNumber);
        if (p != null) {
            int currentHUDValue = GetCurrentPlayerHUDMoneyValue(playerNumber);
            if (currentHUDValue < p.m_money) {
                currentHUDValue++;
            } else {
                if (currentHUDValue > p.m_money) {
                    currentHUDValue--;
                }
            }

            ChangePlayerMoneyString(playerNumber+1, currentHUDValue);
            SetCurrentPlayerHUDMoneyValue(playerNumber+1, currentHUDValue);
        }
    }
}
