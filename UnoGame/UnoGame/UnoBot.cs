using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UnoBot
{
    private List<Card> _hand;
    private bool _isBot;

    public UnoBot()
    {
        _hand = new();
        _isBot = false;
    }

    public List<Card> BotHand
    {
        get { return _hand; }
    }

    public void AddCard(Card card)
    {
        _hand.Add(card);
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

        foreach (Card card in _hand)
        {
            str += card.ToString() + "\n";
        }
        return str;
    }
}