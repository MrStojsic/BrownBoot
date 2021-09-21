using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=uPmorHLPwnk&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=65
// Watched from 13.0 to 13.4, havent done the spell tooptip as havnet implimented spells yet.
public interface IDescribable
{
    string LongDescription
    { get; }
    string ShortDescription
    { get; }

    /// <summary>
    /// Returns a custom description.
    /// </summary>
    /// <returns></returns>
    string GetShortDescription();
    string GetLongDescription();
}
