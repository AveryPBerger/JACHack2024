public class GameManager
{
    //Fields
    private CardDeck deck;
    private Discard discardPile;
    public Player[] players;

    private const int StartHandSize = 7;
    public Card _topCard;

    //Constructors
    public GameManager()
    {
        deck = new();

        discardPile = new();
        players = new Player[4];

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new();
            players[i].PlayerId = (i + 1);
        }
    }

    public GameManager(int playerCount)
    {
        deck = new();
        discardPile = new();
        players = new Player[playerCount];


        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new();
            players[i].PlayerId = (i + 1);
        }
    }

    //Getters
    public int playerLength()
    {
        return this.players.Length;
    }

    public Player getPlayer(int index)
    {
        return players[index];
    }

    //Cool Methods
    public void PlayerStart()
    {
        foreach (Player player in players)
        {
            for (int numOfCards = 0; numOfCards < StartHandSize; numOfCards++)
            {
                PlayerDraw(player);
            }
        }
        TopCard = deck.DrawFromDeck();



    }

    //public void PlayerDiscard(Card discarded)
    //{

    //}

    public void ShowHand(int index)
    {
        // Console.WriteLine(players[index]);
        int count = 1;
        foreach (Card card in players[index].Hand)
        {
            Console.ForegroundColor = Card.GetConsoleColor(card.Color);
            Console.WriteLine($"{count++}. {card}");
        }
    }

    public List<Card> GetHand(int index)
    {
        return players[index].Hand;
    }

    public void PlayerDraw(Player player)
    {
        Console.ResetColor();
        player.AddCard(deck.DrawFromDeck());
    }

    public void playCard(int player, int cardIndex)
    {
        Card discarded = players[player].DiscardCard(cardIndex);
        TopCard = discarded;
    }
    public bool IsLegal(Card card, bool stacking)
    {
        if (stacking)
        {
            if (card.Color == TopCard.Color || card.Value == TopCard.Value)
            {
                return true;
            }
        }
        else
        {

            if (card.Color == TopCard.Color || card.Value == TopCard.Value || card.Value < 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsLegal(Card card)    //Original
    {
        if (card.Color == TopCard.Color || card.Value == TopCard.Value || card.Value < 0)
        {
            return true;
        }

        return false;
    }
    public void RunCard(Card card, int currentPlayer, bool isStacked)
    {
        if (card.Value == -2) //Wild Draw 4
        {
            ChangeCardColor();
            TopCard = card;
            for (int i = 0; i < 4; i++)
            {
                PlayerDraw(players[nextPlayer(currentPlayer)]);
            }
        }
        else if (card.Value == 11)    //Draw 2
        {
            ChangeCardColor();
            TopCard = card;
            for (int i = 0; i < 2; i++) //draws two no?
            {
                PlayerDraw(players[nextPlayer(currentPlayer)]);
            }
        }
        else if (card.Value == -1) // wild normal
        {
            ChangeCardColor();
        }
        else if (card.Value == 12) // Reverse
        {
            players.Reverse();
        }
        else if (card.Value == 10) // Skip
        {
            players[currentPlayer] = players[nextPlayer(nextPlayer(currentPlayer))];
        }
    }

    public void RunCard(Card card, int currentPlayer)
    {
        if (card.Value == -2) //Wild Draw 4
        {
            TopCard = card;
            for (int i = 0; i < 4; i++)
            {
                PlayerDraw(players[nextPlayer(currentPlayer)]);
            }
        }
        else if (card.Value == 11)    //Draw 2
        {
            TopCard = card;
            for (int i = 0; i < 2; i++) //draws two no?
            {
                PlayerDraw(players[nextPlayer(currentPlayer)]);
            }
        }
        else if (card.Value == -1) // wild normal
        {
            ChangeCardColor();
        }
        else if (card.Value == 12) // Reverse
        {
            players.Reverse();
        }
        else if (card.Value == 10) // Skip
        {
            players[currentPlayer] = players[nextPlayer(nextPlayer(currentPlayer))];
        }
    }



    //-1: Color Change
    //-2: Draw Four
    //10: skip 
    //11: Draw 2
    //12: Reverse

    public bool IsStackable(int currentPlayer)
    {
        foreach (Card card in players[currentPlayer].Hand)
        {
            if (TopCard.Value == card.Value || TopCard.Color == card.Color)
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeCardColor()
    {
        Console.Write("What color would you like to change to?: ");
        TopCard.Color = Card.CardColorValidation(Console.ReadLine());
    }

    public CardDeck Deck
    {
        get { return deck; }
    }

    public Discard DiscardPile
    {
        get { return discardPile; }
    }

    public Player[] Players
    {
        get { return players; }
    }

    public Card TopCard
    {
        get { return _topCard; }
        set { _topCard = value; }
    }

    public bool IsGameOver()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (Player.IsHandEmpty(players[i]) || deck.IsDeckEmpty())
            {
                return true;
            }
        }
        return false;
    }

    private int nextPlayer(int currentPlayer)
    {
        if (currentPlayer < players.Length - 1)
        {
            return currentPlayer + 1;
        }
        else
        {
            return 0;
        }
    }


    //public void SortCards(int currentPlayer)
    //{
    //    players[currentPlayer].Hand.ToString().Sort();
    //}
} //END OF CLASS!

