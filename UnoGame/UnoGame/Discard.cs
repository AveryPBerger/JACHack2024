using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Discard
{
    private List<Card> discardPile;

    public Discard()
    {
        discardPile = new();
    }

    public Card GetTopCard()
    {
        return discardPile[discardPile.Count - 1];
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < discardPile.Count; i++)
        {
            s += (i + 1) + discardPile[i].ToString() + "\n";
        }

        return s;
    }

    public void AddCard(Card card)
    {
        discardPile.Add(card);
    }
}
