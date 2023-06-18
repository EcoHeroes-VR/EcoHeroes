using UnityEngine;
using System.Collections.Generic;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description: Custom Scriptable Object to easily access possible score points depending on the type of ingredient GameObject\n
    /// Author:     Theresa Mayer\n
    /// </summary>
    [CreateAssetMenu(menuName = "Ingredient Data")]
    public class IngredientData : ScriptableObject
    {
        public string ingredientType;
        public int points;
    }
}

