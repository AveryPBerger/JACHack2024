using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Player
{
    private List<Card> _hand;
    private bool _isBot;
    private int _playerID;

    public Player()
    {
        _hand = new();
        _isBot = false;
        _playerID = 0;
    }

    public Player(bool isBot)
    {
        _hand = new();
        _isBot = isBot;
        _playerID = 0;
    }

    //Getter / setters
    public int PlayerId
    {
        get { return _playerID; }
        set { _playerID = value; }
    }

    public List<Card> Hand
    {
        get { return _hand; }
        set { _hand = value; }
    }

    public void AddCard(Card card)
    {
        _hand.Add(card);
    }

    public int HandLength()
    {
        return Hand.Count;
    }

    public Card DiscardCard(int index)
    {
        Card card = _hand[index];
        _hand.RemoveAt(index);
        return card;
    }

    public static bool IsHandEmpty(Player player)
    {
        if (player.Hand.Count == 0)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        string str = "";
        int count = 1;
        foreach (Card card in _hand)
        {
            str += $"{count++}. ";
            str += card.ToString() + "\n";
        }

        return str;
    }
}