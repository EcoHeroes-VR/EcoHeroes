using Assets._Game.Scripts.Menu.Manager;
using TMPro;
using UnityEngine;


namespace _Game.Scripts.Modules.Menu.NameGenerator
{
    /// <summary>
    /// Description:    Button Events for the name generator menu
    /// Author:         Marc Fischer
    /// </summary>
    public class NameGeneratorMenu : MonoBehaviour
    {
        private void Awake()
        {
            _name.text = GenerateNewName();
        }
        [SerializeField] private TextMeshProUGUI _name;

        /// <summary>
        /// Description:    Event for the "Accept" button in the name generator menu. Go back to the main menu. Will save the name (currently not implemented)
        /// Author:         Marc Fischer
        /// </summary>
        public void AcceptButton()
        {
            // safe name here
            MenuManager.OpenMenu(MenuType.MAIN_MENU, gameObject);
        }

        /// <summary>
        /// Description:    Generates a new random name and updates the displayed name.
        /// Author:         Marc Fischer
        /// </summary>
        public void GenerateNewNameButton()
        {
            UpdateName();
        }

        private readonly string[] _randonUserNames = new string[]
        {
            "Fuchs", "Hirsch", "Igel", "Eule", "Eichh�rnchen", "Hase", "Reh", "Maulwurf", "Maus", "Hamster",
            "Fledermaus",
            "Biber", "Dachs", "Marder", "B�r", "Wolf", "Luchs", "Hund", "Katze", "Pferd", "Schaf", "Ziege", "Huhn",
            "Ente",
            "Gans", "Pinguin", "Flamingo", "Adler", "Falke", "Taube", "Spatz", "Amsel", "Kr�he", "Specht", "Storch",
            "Pelikan", "Krokodil", "Schildkr�te", "Krabbe", "Hummer", "Aal", "Lachs", "Hai", "Delphin", "Seehund",
            "K�nguru", "Koala", "Nashorn", "Giraffe", "Schlange", "Eidechse", "Gekko", "Kaninchen", "Hund", "Goldfisch",
            "Wellensittich", "Kolibri", "Schmetterling", "Biene", "Libelle", "Marienk�fer", "Krake", "Seestern",
            "Frosch",
            "Salamander", "Echse", "Cham�leon", "Tintenfisch", "Fasan", "Sperling", "Mauersegler", "Fischotter",
            "Iltis",
            "Delfin", "Papagei", "K�nguru", "B�ffel", "L�we", "Tiger", "Leopard", "Puma", "Gepard", "Waschb�r",
            "Marderhund", "Gazelle", "Pferd"
        };

        /// <summary>
        /// Description:    Generates a new random name using the random username list.
        /// Author:         Marc Fischer
        /// </summary>
        /// <returns>A randomly generated name as a string.</returns>
        private string GenerateNewName()
        {
            return _randonUserNames[Random.Range(0, _randonUserNames.Length)] + "#" + Random.Range(0, 500);
        }

        /// <summary>
        /// Description:    Updates the displayed name with a newly generated random name.
        /// Author:         Marc Fischer
        /// </summary>
        private void UpdateName()
        {
            _name.text = GenerateNewName();
        }
    }
}