namespace Zadanie;

using System;
public class OverfillException : Exception
{
    public OverfillException(string wiadomosc) : base(wiadomosc) { }
}