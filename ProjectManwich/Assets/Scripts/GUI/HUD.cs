using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    public Texture heartFull;
    public Texture heartEmpty;

    private Transform m_player1;
    private Transform m_player2;
    private Transform m_player3;
    private Transform m_player4;

    private Player p1;
    private Player p2;
    private Player p3;
    private Player p4;
    private List<Player> m_players;

    // hearts
    private GUITexture p1Heart1;
    private GUITexture p1Heart2;
    private GUITexture p1Heart3;

    private GUITexture p2Heart1;
    private GUITexture p2Heart2;
    private GUITexture p2Heart3;

    private GUITexture p3Heart1;
    private GUITexture p3Heart2;
    private GUITexture p3Heart3;

    private GUITexture p4Heart1;
    private GUITexture p4Heart2;
    private GUITexture p4Heart3;

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

        m_players = new List<Player>();

        p1 = PlayerManager.GetPlayer(0);
        if (p1 != null) {
            player1HUDMoneyValue = p1.m_money;
            m_players.Add(p1);

            Transform p1Hearts = m_player1.FindChild("PlayerHearts");
            p1Heart1 = p1Hearts.FindChild("Heart1").GetComponent<GUITexture>();
            p1Heart2 = p1Hearts.FindChild("Heart2").GetComponent<GUITexture>();
            p1Heart3 = p1Hearts.FindChild("Heart3").GetComponent<GUITexture>();
        }

        p2 = PlayerManager.GetPlayer(1);
        if (p2 != null) {
            player2HUDMoneyValue = p2.m_money;
            m_players.Add(p2);

            Transform p2Hearts = m_player2.FindChild("PlayerHearts");
            p2Heart1 = p2Hearts.FindChild("Heart1").GetComponent<GUITexture>();
            p2Heart2 = p2Hearts.FindChild("Heart2").GetComponent<GUITexture>();
            p2Heart3 = p2Hearts.FindChild("Heart3").GetComponent<GUITexture>();
        } else {
            EnablePlayerHUD(2, false);
        }

        p3 = PlayerManager.GetPlayer(2);
        if (p3 != null) {
            player3HUDMoneyValue = p3.m_money;
            m_players.Add(p3);

            Transform p3Hearts = m_player3.FindChild("PlayerHearts");
            p3Heart1 = p3Hearts.FindChild("Heart1").GetComponent<GUITexture>();
            p3Heart2 = p3Hearts.FindChild("Heart2").GetComponent<GUITexture>();
            p3Heart3 = p3Hearts.FindChild("Heart3").GetComponent<GUITexture>();
        } else {
            EnablePlayerHUD(3, false);
        }

        p4 = PlayerManager.GetPlayer(3);
        if (p4 != null) {
            player4HUDMoneyValue = p4.m_money;
            m_players.Add(p4);

            Transform p4Hearts = m_player4.FindChild("PlayerHearts");
            p4Heart1 = p4Hearts.FindChild("Heart1").GetComponent<GUITexture>();
            p4Heart2 = p4Hearts.FindChild("Heart2").GetComponent<GUITexture>();
            p4Heart3 = p4Hearts.FindChild("Heart3").GetComponent<GUITexture>();
        } else {
            EnablePlayerHUD(4, false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Player p in m_players) {
            UpdatePlayerMoney(p);
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

    private void SetPlayerHeart1State(int playerNumber, bool full)
    {
        Texture heartTexture = null;
        if (full) {
            heartTexture = heartFull;
        } else {
            heartTexture = heartEmpty;
        }

        switch (playerNumber) {
            case 1:

                p1Heart1.texture = heartTexture;
                break;

            case 2:
                p2Heart1.texture = heartTexture;
                break;

            case 3:
                p3Heart1.texture = heartTexture;
                break;

            case 4:
                p4Heart1.texture = heartTexture;
                break;
        }
    }

    private void SetPlayerHeart2State(int playerNumber, bool full)
    {
        Texture heartTexture = null;
        if (full) {
            heartTexture = heartFull;
        } else {
            heartTexture = heartEmpty;
        }

        switch (playerNumber) {
            case 1:

                p1Heart2.texture = heartTexture;
                break;

            case 2:
                p2Heart2.texture = heartTexture;
                break;

            case 3:
                p3Heart2.texture = heartTexture;
                break;

            case 4:
                p4Heart2.texture = heartTexture;
                break;
        }
    }

    private void SetPlayerHeart3State(int playerNumber, bool full)
    {
        Texture heartTexture = null;
        if (full) {
            heartTexture = heartFull;
        } else {
            heartTexture = heartEmpty;
        }

        switch (playerNumber) {
            case 1:

                p1Heart3.texture = heartTexture; 
                break;

            case 2:
                p2Heart3.texture = heartTexture;
                break;

            case 3:
                p3Heart3.texture = heartTexture;
                break;

            case 4:
                p4Heart3.texture = heartTexture;
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

    public void UpdatePlayerMoney(Player p)
    {
        int playerNumber = p.m_playerIndex+ 1;
        if (p != null) {
            int currentHUDValue = GetCurrentPlayerHUDMoneyValue(playerNumber);
            if (currentHUDValue < p.m_money) {
                currentHUDValue++;
            } else {
                if (currentHUDValue > p.m_money) {
                    currentHUDValue--;
                }
            }

            ChangePlayerMoneyString(playerNumber, currentHUDValue);
            SetCurrentPlayerHUDMoneyValue(playerNumber, currentHUDValue);
        }
    }

    public void UpdatePlayerHealth(Player p)
    {
        int playerNumber = p.m_playerIndex + 1;

        switch (p.m_health) {
            case 3:
                SetPlayerHeart1State(playerNumber, true);
                SetPlayerHeart2State(playerNumber, true);
                SetPlayerHeart3State(playerNumber, true);
                break;

            case 2:
                SetPlayerHeart1State(playerNumber, false);
                SetPlayerHeart2State(playerNumber, true);
                SetPlayerHeart3State(playerNumber, true);
                break;

            case 1:
                SetPlayerHeart1State(playerNumber, false);
                SetPlayerHeart2State(playerNumber, false);
                SetPlayerHeart3State(playerNumber, true);
                break;

            case 0: // fall-through
            default:
                SetPlayerHeart1State(playerNumber, false);
                SetPlayerHeart2State(playerNumber, false);
                SetPlayerHeart3State(playerNumber, false);
                break;
        }
    }
}
