using UnityEngine;
using System.Collections;

public class Player
{
    public int m_playerIndex;
    public PlayerClass m_class;

    public int m_health;
    public int m_money;

    public Player(int index)
    {
        m_playerIndex = index;
        m_class = new Hobo();
        m_money = 500;
        m_health = 3;
    }

    public void Update()
    {
        
    }

}
