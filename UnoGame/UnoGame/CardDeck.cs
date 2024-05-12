using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class CardDeck
{
    public const int MaxDeckSize = 108;
    public const int Offset = 1;

    private Card[] _deck;
    private int _deckIndex;
    public static Random rnd = new Random();

    public Card[] Deck
    {
        get { return _deck; }
    }

    public CardDeck()
    {
        _deck = new Card[MaxDeckSize];
        _deckIndex = 0;
        FillDeck();
        ShuffleDeck();
    }

    public void FillDeck()
    {
        for (Colors cardColor = Colors.Red; cardColor <= Colors.Yellow; cardColor++)
        {
            for (int i = 1; i <= 12; i++)
            {
                CreateCards(2, i, cardColor);
            }

        }

        for (Colors cardColor = Colors.Red; cardColor <= Colors.Yellow; cardColor++)
        {
            for (int i = -2; i <= 0; i++)
            {
                if (i < 0)
                {
                    CreateCards(1, i, Colors.Wild);
                }
                else
                {
                    CreateCards(1, i, cardColor);
                }
            }
        }
    }

    // Shuffles the deck.

    public void ShuffleDeck()
    {

        for (int numOfTimes = 0; numOfTimes < 10; numOfTimes++)
        {
            for (int currentIndex = 0; currentIndex < MaxDeckSize; currentIndex++)
            {
                int randomIndex = rnd.Next(currentIndex, MaxDeckSize);
                Card tempCard = _deck[currentIndex];
                _deck[currentIndex] = _deck[randomIndex];
                _deck[randomIndex] = tempCard;
            }
        }
    }

    public bool IsDeckEmpty()
    {
        if (_deck.Length <= 0)
        {
            return true;
        }
        return false;
    }

    public Card DrawFromDeck()
    {
        _deckIndex--;
        return _deck[_deckIndex];
    }


    public void CreateCards(int count, int value, Colors color)
    {
        for (int i = 0; i < count; i++)
        {
            _deck[_deckIndex] = new Card(color, value);
            _deckIndex++;
        }
    }

}

