using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    public static Random random = new();
    public static void Main()
    {
        Console.Write($"Welcome! How many players will be participating today? (2-4): ");
        GameManager gm = new(Validation(2, 4));
        gm.PlayerStart();

        while (!gm.IsGameOver())
        {
            for (int i = 0; i < gm.playerLength(); i++)
            {
                if (!gm.IsGameOver())
                {
                    Console.Clear();

                    
                    //Individual player turns go here.
                    if (i == 0)
                    {
                        Console.WriteLine("------------------");
                        Console.WriteLine("Player " + gm.getPlayer(i).PlayerId + "'s turn");
                        Console.WriteLine("Current Card: " + gm.TopCard);
                        Console.ResetColor();
                        Console.WriteLine("------------------");
                        PlayerTurn(i, gm);
                    }
                    else
                    {
                        ComputerCardSelection(i, gm);
                        Console.WriteLine("------------------");
                        Console.WriteLine("Player " + gm.getPlayer(i).PlayerId + "'s turn");
                        Console.WriteLine("Current Card: " + gm.TopCard);
                        Console.ResetColor();
                        Console.WriteLine("------------------");
                    }

                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The Game Is Over!");
                    Console.ReadLine();
                }
            }
        }
    }


    public static void PlayerTurn(int index, GameManager gm)
    {
        Console.WriteLine("Your hand is: ");
        gm.ShowHand(index);
        Console.WriteLine();
        SelectCardsToPlay(gm, index);
    }

    public static void ComputerCardSelection(int index, GameManager gm)
    {
        List<Card> cardlist = gm.GetHand(index);
        Card computerCard = null;
        int cardIndex = 0;
        for (int card = 0; card < cardlist.Count; card++)
        {
            if (gm.IsLegal(cardlist[card]))
            {
                computerCard = cardlist[card];
                cardIndex = card;
            }
        }

        if (computerCard == null)
        {
            gm.PlayerDraw(gm.players[index]);
            Console.WriteLine($"The Computer Has No Available Moves Therefore Drew A Card!");
        }
        else
        {
            if (cardlist[cardIndex].Value == -1)
            {
                int value = random.Next(0, 5);
                gm.TopCard.Color = (Colors) value;
            }
            gm.RunCard(cardlist[cardIndex], index);
            gm.playCard(index, cardIndex);
            Console.WriteLine($"The Computer Played A {computerCard}");
        }
        Console.ReadLine();

    }
    public static void SelectCardsToPlay(GameManager gm, int index)
    {
        //bool stacking = false;
        Console.ResetColor();
        Console.WriteLine("-------------------------------");
        Console.WriteLine($"What card(s) do you want to discard? ");
        Console.WriteLine();

        FindValidPlays(gm, index);
        int selectCard = CardValidation(gm, index) - 1;
        if (selectCard == gm.players[index].HandLength() + 1)
        {
            gm.PlayerDraw(gm.players[index]);
        }
        else
        {
            gm.RunCard(gm.GetHand(index)[selectCard], index);
            gm.playCard(index, selectCard);
        }



        if (gm.IsStackable(index))
        {
            Console.ResetColor();
            Console.WriteLine("\n--------------------------");
            Console.WriteLine("You can stack a card here.");
            SelectCardsToPlay(gm, index);
        }
    }

    public static void FindValidPlays(GameManager gm, int index)
    {
        Console.ResetColor();
        Console.WriteLine("Your Valid Plays Are:");
        List<Card> cardlist = gm.GetHand(index);
        for (int i = 0; i < cardlist.Count; i++)
        {
            if (gm.IsLegal(cardlist[i]))
            {
                Console.WriteLine($"{i + 1}. {cardlist[i]}");
            }
        }
        Console.ResetColor();
        Console.WriteLine($"{cardlist.Count + 1}. Draw a card");
        Console.WriteLine("----------------------------");
        Console.Write("Card Choice: ");

    }

    public static int CardValidation(GameManager gm, int index)
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num) || num > gm.players[index].HandLength() || num < 0 || !gm.IsLegal(gm.GetHand(index)[num - 1]))
        {
            if (num == gm.players[0].HandLength())
            {
                return num;
            }
            Console.ResetColor();
            Console.WriteLine("Please Enter a valid option");
            FindValidPlays(gm, index);
        }
        return num;
    }

    public static int Validation(int min, int max)
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num) || num < min || num > max)
        {
            Console.ResetColor();
            Console.Write("Please enter a valid number (2-4): ");
        }
        return num;
    }
}
