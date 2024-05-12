using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

public enum Colors
{
    Red,
    Blue,
    Green,
    Yellow,
    Wild

    //private const string _color;

    //private Colors(string color) {
    //  this._color = color;
    //}
}

//Class that represents a single card in game.
public class Card
{
    public Colors _color;
    public int _value;

    public Card()
    {
        _color = Colors.Red;
        _value = 0;
    }

    public Card(Colors color_, int num_)
    {
        _color = color_;
        _value = num_;
    }

    public Card(Card card)
    {
        _color = card.Color;
        _value = card.Value;
    }

    public static Colors CardColorValidation(string response)
    {
        Colors cardColor;

        while (!Colors.TryParse(response, out cardColor))
        {
            Console.Write("Invalid card color! Please enter a valid color");
            response = Console.ReadLine();
        }

        return cardColor;
    }

    public Colors Color
    { //Property for Color
        get { return _color; }
        set { _color = value; }
    }

    public int Value
    {    //Property for Value
        get { return _value; }
        set { _value = value; }
    }

    public static ConsoleColor GetConsoleColor(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                return ConsoleColor.Red;
            case Colors.Green:
                return ConsoleColor.Green;
            case Colors.Blue:
                return ConsoleColor.Blue;
            case Colors.Yellow:
                return ConsoleColor.Yellow;
            default:
                return ConsoleColor.White;
        }
    }

    public override string ToString()
    {
        Console.ResetColor();
        Console.ForegroundColor = GetConsoleColor(this.Color);

        if (Value == -1)
        {
            return "Change Color";
        }
        else if (Value == -2)
        {
            return "Draw Four";
        }
        else if (Value == 10)
        {
            return $"{_color} Skip";
        }
        else if (Value == 11)
        {
            return $"{_color} Draw 2";
        }
        else if (Value == 12)
        {
            return $"{_color} Reverse";
        }
        else
        {
            return $"{_color} {_value}";
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        Card card = obj as Card;

        if (card is null)
            return false;
        else
            return this.Color == card.Color && this.Value == card.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_color, _value);
    }
}


