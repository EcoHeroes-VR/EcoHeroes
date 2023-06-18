using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts.Modules.Menu.Start
{
    public class StartGameScript : MonoBehaviour
    {

        public InputActionReference newGameButton = null;
        public InputActionReference randomButton = null;
        public TMP_InputField playerName;
        public TextMeshProUGUI difficultyLabel;
		public TMP_Dropdown playerNameMenu;
        

        private string[] randonUserNames = new string[]
        {
            "Fuchs", "Hirsch", "Igel", "Eule", "Eichhörnchen", "Hase", "Reh", "Maulwurf", "Maus", "Hamster",
            "Fledermaus",
            "Biber", "Dachs", "Marder", "Bär", "Wolf", "Luchs", "Hund", "Katze", "Pferd", "Schaf", "Ziege", "Huhn",
            "Ente",
            "Gans", "Pinguin", "Flamingo", "Adler", "Falke", "Taube", "Spatz", "Amsel", "Krähe", "Specht", "Storch",
            "Pelikan", "Krokodil", "Schildkröte", "Krabbe", "Hummer", "Aal", "Lachs", "Hai", "Delphin", "Seehund",
            "Känguru", "Koala", "Nashorn", "Giraffe", "Schlange", "Eidechse", "Gekko", "Kaninchen", "Hund", "Goldfisch",
            "Wellensittich", "Kolibri", "Schmetterling", "Biene", "Libelle", "Marienkäfer", "Krake", "Seestern",
            "Frosch",
            "Salamander", "Echse", "Chamäleon", "Tintenfisch", "Fasan", "Sperling", "Mauersegler", "Fischotter",
            "Iltis",
            "Delfin", "Papagei", "Känguru", "Büffel", "Löwe", "Tiger", "Leopard", "Puma", "Gepard", "Waschbär",
            "Marderhund", "Gazelle", "Pferd"
        };


        /// <summary>
        /// Description:    This method is called automatically by Unity when the game object
        ///                 this script is attached to is first created in the scene.\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void Start()
        {
            if (playerNameMenu != null)
            {
                refreshUsernames();
            }

            //Debug.Log("enabled?");
            //Debug.Log(teleportationProvider.enabled);
            //teleportationProvider.enabled = true;

            // If the backButton variable is not null, add a listener to the button
            // that calls the StartGameScene function when the button is clicked.
            /*if (newGameButton != null)
            {
                newGameButton.onClick.AddListener(StartGameScene);
            }

            if (randomButton != null)
            {
                randomButton.onClick.AddListener(GenerateNewName);
            }*/

            // If the difficultyLabel variable is not null, set the text of the label
            // based on the value of the "difficultyLevel" player preference.
            if (difficultyLabel != null)
            {
                // Get the value of the "difficultyLevel" player preference, or 0 if it
                // does not exist yet.
                var difficultyLevel = PlayerPrefs.GetInt("difficultyLevel", 0);

                // If the difficulty level is 0, set the text of the label to indicate
                // that the game will be played with easy difficulty.
                if (difficultyLevel == 0)
                {
                    difficultyLabel.text =
                        "Der aktuelle Schwierigkeitsgrad des Spiels ist Normal.";
                }
                // Otherwise, set the text of the label to indicate that the game will
                // be played with challenging difficulty.
                else
                {
                    difficultyLabel.text =
                        "Der aktuelle Schwierigkeitsgrad des Spiels ist Anspruchsvoll.";
                }

                int randomIndex = Random.Range(0, randonUserNames.Length);
                playerName.text = randonUserNames[randomIndex] + "#" + Random.Range(0, 500);
            }
        }

        // <summary>
        /// Description:    Generates 3 unique names with random numbers appended to them.
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            string[]
        /// </summary>
        /// <returns>An array of 3 unique names with random numbers appended to them.</returns>
        string[] generateUniqueNames()
        {
            // Create a HashSet to keep track of the unique numbers
            HashSet<int> uniqueNumbers = new HashSet<int>();

            // Loop until we have 3 unique numbers
            while (uniqueNumbers.Count < 3)
            {
                // Generate a random number between 0 and randonUserNames.Length
                int randomNumber = Random.Range(0, randonUserNames.Length);

                // Add the number to the HashSet
                uniqueNumbers.Add(randomNumber);
            }

            // Convert the HashSet to an array and print the result
            int[] nums = new int[3];
            uniqueNumbers.CopyTo(nums);

            return new string[]
            {
                randonUserNames[nums[0]] + "#" + Random.Range(0, 500),
                randonUserNames[nums[1]] + "#" + Random.Range(0, 500),
                randonUserNames[nums[2]] + "#" + Random.Range(0, 500)
            };
        }

        /// <summary>
        /// Description:    Refreshes the list of player names in the dropdown menu.
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void refreshUsernames()
        {
            string[] generatedNames = generateUniqueNames();

            foreach (string name in generatedNames)
            {
                playerNameMenu.options.Add(new TMP_Dropdown.OptionData(name));
            }

            // Refresh the dropdown to show the new option
            playerNameMenu.RefreshShownValue();
        }


        /// <summary>
        /// Description:    Loads the game scene (hubworld) when the back button is clicked.\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void StartGameScene()
        {
            // Load the game scene
            Debug.Log("UNLOCK PLAYER HERE");
        }

        /// <summary>
        /// Description:    Updates the output text to display the player's name.\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        public void UpdateOutputText()
        {
            // Log the player's name
            Debug.Log(playerName.text);
        }

        /// <summary>
        /// Description:    Generates a new player name by selecting a random username from the list of usernames
        ///                 and appending a randomly generated number to it.\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        public void GenerateNewName()
        {
            playerNameMenu.ClearOptions();
            refreshUsernames();
        }

        /// <summary>
        /// Description:    Update is called once per frame\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void Update()
        {

        }

    }
}