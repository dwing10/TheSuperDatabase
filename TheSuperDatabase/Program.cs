using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheSuperDatabase
{
    // **************************************************
    //
    // Title: The Super Database!
    // Description: A database to track characters from the Marvel and DC universes
    // Application Type: Console
    // Author: Devin Wing
    // Dated Created: 11/17/2019
    // Last Modified: 12/04/2019
    //
    // **************************************************

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            //read seed data from file
            List<Character> characters = ReadFromDataFile();

            //List<Character> characters = InitializeCharacterList();

            DisplayWelcomeScreen();

            DisplayMainMenu(characters);

            DisplayClosingScreen();
        }

        #region menus

        /// <summary>
        /// application main menu
        /// </summary>
        static void DisplayMainMenu(List<Character> characters)
        {
            bool quitApplication = false;

            do
            {
                Console.WriteLine();
                DisplayScreenHeader("Main Menu");

                //get user's menu choice
                Console.WriteLine("\ta) View Database");
                Console.WriteLine();
                Console.WriteLine("\tb) Add A Character");
                Console.WriteLine();
                Console.WriteLine("\tc) Update A Character");
                Console.WriteLine();
                Console.WriteLine("\td) Delete A Character");
                Console.WriteLine();
                Console.WriteLine("\tq) Quit");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\tEnter Choice: ");
                string menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "a":
                        DisplayViewDatabase(characters);
                        break;
                    case "b":
                        DisplayAddChar(characters);
                        break;
                    case "c":
                        DisplayUpdateCharacter(characters);
                        break;
                    case "d":
                        DisplayDeleteCharacter(characters);
                        break;
                    case "q":
                        quitApplication = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a choice from the list above");
                        DisplayContinuePrompt();
                        Console.Clear();
                        break;
                }
            } while (!quitApplication);

            DisplayContinuePrompt();

        }

        /// <summary>
        /// view the database
        /// </summary>
        static void DisplayViewDatabase(List<Character> characters)
        {
            bool quitApplication = false;

            do
            {
                //get user's menu choice
                DisplayScreenHeader("View Database");
                Console.WriteLine();
                Console.WriteLine("\ta) View All");
                Console.WriteLine();
                Console.WriteLine("\tb) View Only DC Characters");
                Console.WriteLine();
                Console.WriteLine("\tc) View Only Marvel Characters");
                Console.WriteLine();
                Console.WriteLine("\td) View Heroes");
                Console.WriteLine();
                Console.WriteLine("\te) View Villains");
                Console.WriteLine();
                Console.WriteLine("\tf) Search By Name");
                Console.WriteLine();
                Console.WriteLine("\tq) Return To Main Menu");
                Console.WriteLine();
                string userResponse = Console.ReadLine().ToLower();
                Console.WriteLine();

                switch (userResponse)
                {
                    case "a":
                        Console.Clear();
                        DisplayAllCharacters(characters);
                        break;
                    case "b":
                        Console.Clear();
                        DisplayDC(characters);
                        break;
                    case "c":
                        Console.Clear();
                        DisplayMarvel(characters);
                        break;
                    case "d":
                        Console.Clear();
                        DisplayHeroes(characters);
                        break;
                    case "e":
                        Console.Clear();
                        DisplayVillains(characters);
                        break;
                    case "f":
                        DisplaySearch(characters);
                        break;
                    case "q":
                        //DisplayMainMenu(characters);
                        quitApplication = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a choice from the list above.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        break;
                }

            } while (!quitApplication);
        }

        #endregion

        #region add, delete, and update character methods

        /// <summary>
        /// add a character
        /// </summary>
        static void DisplayAddChar(List<Character> characters)
        {
            Character newChar = new Character();
            string response;

            DisplayScreenHeader("Add Character");

            // add character object property values

            //enter name
            bool validName = false;
            do
            {
                Console.WriteLine();
                Console.Write("\tName: ");
                response = Console.ReadLine().ToUpper();
                if (response != "")
                {
                    newChar.Name = response;
                    validName = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tYou must provide a name.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.Clear();
                    DisplayScreenHeader("Add Character");
                }

            } while (!validName);


            //enter real name/ alias
            Console.WriteLine();
            Console.Write("\tReal Name/ Alias: ");
            newChar.RealName = Console.ReadLine().ToUpper();

            //enter gender
            Console.WriteLine();
            Console.Write("\tGender: ");
            newChar.Gender = Console.ReadLine().ToUpper();

            //enter homeworld
            Console.WriteLine();
            Console.Write("\tHomeworld: ");
            newChar.Homeworld = Console.ReadLine().ToUpper();

            //enter alignment
            bool validAlignment = false;
            do
            {
                Console.WriteLine();
                Console.Write("\tAlignment (good, evil, neutral, or antihero): ");
                if (Enum.TryParse(Console.ReadLine().ToUpper(), out Character.Alignment alignment))
                {
                    newChar.Alignments = alignment;
                    validAlignment = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter either good, evil, neutral, or antihero.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.Clear();
                    DisplayScreenHeader("Add Character");
                }

            } while (!validAlignment);

            //enter comic universe
            bool validUniverse = false;
            do
            {
                Console.WriteLine();
                Console.Write("\tUniverse (valid Comic Universes are Marvel or DC): ");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out Character.Universe universe))
                {
                    newChar.ComicUniverse = universe;
                    validUniverse = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter either Marvel or DC.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.Clear();
                    DisplayScreenHeader("Add Character");
                }
            } while (!validUniverse);

            //echo properties and confirm character
            bool validResponse = false;
            do
            {
                Console.Clear();
                // echo new character properties
                Console.WriteLine();
                Console.WriteLine("\tNew Character's Properties");
                CharacterInfo(newChar);
                DisplayContinuePrompt();

                Console.WriteLine();
                Console.WriteLine($"\tAre you sure you want to add {newChar.Name} to the database?");
                Console.Write("\tEnter yes or no: ");
                switch (Console.ReadLine().ToLower())
                {
                    case "no":
                        Console.WriteLine();
                        Console.WriteLine($"\tOkay, {newChar.Name} will not be added to the database.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        validResponse = true;
                        break;
                    case "yes":
                        characters.Add(newChar);
                        WriteToDataFile(characters);
                        validResponse = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter either yes or no.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        break;
                }
            } while (!validResponse);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t{newChar.Name} was added to the database");
            Console.WriteLine();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// delete a character
        /// </summary>
        static void DisplayDeleteCharacter(List<Character> characters)
        {
            DisplayScreenHeader("Delete A Character");

            // get user character choice
            Console.WriteLine();
            Console.Write("\tEnter name: ");
            string charName = Console.ReadLine().ToUpper();

            // get character object
            Character selectedChar = null;
            foreach (Character character in characters)
            {
                if (character.Name == charName)
                {
                    selectedChar = character;
                    break;
                }
            }

            //confirm delete
            bool validAnswer = false;
            string response;
            do
            {
                // display character detail
                Console.WriteLine();
                Console.WriteLine("\t*********************");
                CharacterInfo(selectedChar);
                Console.WriteLine("\t*********************");

                Console.WriteLine();
                Console.Write($"\tAre you sure you want to delete {selectedChar.Name} (yes or no)? ");
                response = Console.ReadLine().ToLower();

                switch (response)
                {
                    case "yes":
                        // delete character
                        if (selectedChar != null)
                        {
                            characters.Remove(selectedChar);
                            Console.WriteLine();
                            Console.WriteLine($"\t{selectedChar.Name} was deleted");
                            WriteToDataFile(characters);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\t{charName} was not found");
                        }
                        validAnswer = true;
                        break;
                    case "no":
                        Console.WriteLine();
                        Console.WriteLine($"\tOkay, {selectedChar.Name} will not be deleted.");
                        validAnswer = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter either yes or no.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        DisplayScreenHeader("Delete A Character");
                        break;
                }
            } while (!validAnswer);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// update a character
        /// </summary>
        static void DisplayUpdateCharacter(List<Character> characters)
        {
            bool validResponse = false;
            Character selectedChar = null;

            do
            {
                DisplayScreenHeader("Update Character");

                // get user character choice
                Console.WriteLine();
                Console.Write("\tEnter name: ");
                string charName = Console.ReadLine().ToUpper();

                // get character object
                foreach (Character character in characters)
                {
                    if (character.Name == charName)
                    {
                        selectedChar = character;
                        validResponse = true;
                        break;
                    }
                }

                // feedback for wrong name choice
                if (!validResponse)
                {
                    Console.WriteLine("\tThat name was not found in the databse.");
                    DisplayContinuePrompt();
                }

            } while (!validResponse);

            Console.Clear();
            DisplayScreenHeader("Update Character");


            // update character
            //enter new name
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to update. Press enter to keep the current info.");
            Console.WriteLine();
            Console.WriteLine($"\tCurrent Name: {selectedChar.Name}");
            Console.Write("\tNew Name: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                selectedChar.Name = userResponse;
            }

            //enter new real name/ alias
            Console.WriteLine();
            Console.WriteLine($"\tCurrent Real Name/Alias: {selectedChar.RealName}");
            Console.Write("\tNew Real Name/ Alias ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                selectedChar.RealName = userResponse;
            }

            //enter new gender
            Console.WriteLine();
            Console.WriteLine($"\tCurrent Gender: {selectedChar.Gender}");
            Console.Write("\tNew Gender: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                selectedChar.Gender = userResponse;
            }

            //enter new homeworld
            Console.WriteLine();
            Console.WriteLine($"\tCurrent Homeworld: {selectedChar.Homeworld}");
            Console.Write("\tNew Homeworld: ");
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse != "")
            {
                selectedChar.Homeworld = userResponse;
            }

            //enter new alignment
            bool validAlignment = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Alignment: {selectedChar.Alignments}");
                Console.Write("\tNew Alignment (good, evil, neutral, or antihero): ");
                userResponse = Console.ReadLine().ToUpper();
                if (userResponse != "")
                {
                    if (Enum.TryParse(userResponse, out Character.Alignment align))
                    {
                        selectedChar.Alignments = align;
                        validAlignment = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter either good, evil, neutral, or antihero.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        DisplayScreenHeader("Update Character");
                    }
                }

                if (userResponse == "")
                {
                    validAlignment = true;
                }

            } while (!validAlignment);

            //enter new comic universe
            bool validUniverse = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Universe: {selectedChar.ComicUniverse}");
                Console.Write("\tNew Universe (Marvel or DC): ");
                userResponse = Console.ReadLine().ToUpper();
                if (userResponse != "")
                {
                    if (Enum.TryParse(userResponse, out Character.Universe universe))
                    {
                        selectedChar.ComicUniverse = universe;
                        validUniverse = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter either Marvel or DC.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        DisplayScreenHeader("Update Character");
                    }
                }

                if (userResponse == "")
                {
                    validUniverse = true;
                }

            } while (!validUniverse);

            Console.Clear();

            string saveAnswer;
            bool validSave = false;

            do
            {
                DisplayScreenHeader("Update Character");
                Console.WriteLine();
                CharacterInfo(selectedChar);
                Console.WriteLine();
                Console.Write("\tAre you sure you want to save these changes (yes or no)? ");
                saveAnswer = Console.ReadLine().ToLower();

                switch (saveAnswer)
                {
                    case "yes":
                        WriteToDataFile(characters);
                        Console.WriteLine();
                        Console.WriteLine($"\t{selectedChar.Name}'s new properties were successfully saved.");
                        validSave = true;
                        break;
                    case "no":
                        Console.WriteLine();
                        Console.WriteLine($"\t{selectedChar.Name}'s new properties will not be saved.");
                        Console.WriteLine();
                        validSave = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter yes or no.");
                        Console.WriteLine();
                        DisplayContinuePrompt();
                        Console.Clear();
                        DisplayScreenHeader("Update Character");
                        break;
                }

            } while (!validSave);

            DisplayContinuePrompt();
        }

        #endregion

        #region database views

        /// <summary>
        /// display all characters
        /// </summary>
        static void DisplayAllCharacters(List<Character> characters)
        {

            DisplayScreenHeader("All Characters");

            Console.WriteLine("****************************************");
            foreach (Character character in characters)
            {
                Console.WriteLine();
                CharacterInfo(character);
                Console.WriteLine();
                Console.WriteLine("****************************************");
            }

            DisplayMainMenuPrompt();

        }

        /// <summary>
        /// view only dc characters
        /// </summary>
        static void DisplayDC(List<Character> characters)
        {
            DisplayScreenHeader("DC Characters");
            Console.WriteLine();

            Console.WriteLine("****************************************");
            foreach (Character character in characters)
            {

                if (character.ComicUniverse == Character.Universe.DC)
                {
                    Console.WriteLine();
                    CharacterInfo(character);
                    Console.WriteLine();
                    Console.WriteLine("****************************************");
                }

            }

            Console.WriteLine();
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// view only Marvel characters
        /// </summary>
        static void DisplayMarvel(List<Character> characters)
        {
            DisplayScreenHeader("Marvel Characters");
            Console.WriteLine();

            Console.WriteLine("****************************************");
            foreach (Character character in characters)
            {

                if (character.ComicUniverse == Character.Universe.MARVEL)
                {
                    Console.WriteLine();
                    CharacterInfo(character);
                    Console.WriteLine();
                    Console.WriteLine("****************************************");
                }

            }

            Console.WriteLine();
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// view only heroes
        /// </summary>
        static void DisplayHeroes(List<Character> characters)
        {
            DisplayScreenHeader("Heroes");
            Console.WriteLine();

            Console.WriteLine("****************************************");
            foreach (Character character in characters)
            {

                if (character.Alignments == Character.Alignment.GOOD)
                {
                    Console.WriteLine();
                    CharacterInfo(character);
                    Console.WriteLine();
                    Console.WriteLine("****************************************");
                }

            }

            Console.WriteLine();
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// view only villains
        /// </summary>
        static void DisplayVillains(List<Character> characters)
        {
            DisplayScreenHeader("Villains");
            Console.WriteLine();

            Console.WriteLine("****************************************");
            foreach (Character character in characters)
            {

                if (character.Alignments == Character.Alignment.EVIL)
                {
                    Console.WriteLine();
                    CharacterInfo(character);
                    Console.WriteLine();
                    Console.WriteLine("****************************************");
                }

            }

            Console.WriteLine();
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// search by name
        /// </summary>
        static void DisplaySearch(List<Character> characters)
        {
            bool validResponse = false;
            Character selectedChar = null;
            do
            {
                Console.Clear();
                DisplayScreenHeader("Search By Name");

                // get user character choice
                Console.WriteLine();
                Console.Write("\tEnter name or alias: ");
                string charName = Console.ReadLine().ToUpper();

                // get character object

                foreach (Character character in characters)
                {
                    if (character.Name == charName || character.RealName == charName)
                    {
                        selectedChar = character;
                        validResponse = true;
                        break;
                    }
                }
                if (selectedChar == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("\tThat name does not match any records in our database.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                }

            } while (!validResponse);


            // display character detail
                Console.WriteLine();
                Console.WriteLine("\t*********************");
                CharacterInfo(selectedChar);
                Console.WriteLine("\t*********************");

            DisplayMainMenuPrompt();

        }

        /// <summary>
        /// character information
        /// </summary>
        static void CharacterInfo(Character character)
        {
            Console.WriteLine($"\tName: {character.Name}");
            Console.WriteLine($"\tAlias/ Former Name: {character.RealName}");
            Console.WriteLine($"\tGender: {character.Gender}");
            Console.WriteLine($"\tHomeworld: {character.Homeworld}");
            Console.WriteLine($"\tAlignment: {character.Alignments}");
            Console.WriteLine($"\tUniverse: {character.ComicUniverse}");
        }

        #endregion

        /*#region database initialization

        /// <summary>
        /// initialize the database
        /// </summary>
        static List<Character> InitializeCharacterList()
        {
            List<Character> characters = new List<Character>()
            {
                //DC characters
                new Character()
                {
                    Name = "Superman",
                    RealName = "Kal-El, Clark Joseph Kent",
                    Gender = "Male",
                    Homeworld = "Krypton",
                    Alignments = Character.Alignment.GOOD,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Wonder Woman",
                    RealName = "Dian Prince, Diana of Themyscira",
                    Gender = "Female",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.GOOD,
                    ComicUniverse = Character.Universe.DC

                },
                new Character()
                {
                    Name = "Batman",
                    RealName = "Bruce Wayne",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Green Lantern",
                    RealName = "Hal Jordan",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "The Flash",
                    RealName = "Barry Allen",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Cyborg",
                    RealName = "Vic Stone",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Aquaman",
                    RealName = "Arthur Curry",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Darkseid",
                    RealName = "Uxas",
                    Gender = "Male",
                    Homeworld = "Apokolips",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Doomsday",
                    RealName = "N/A",
                    Gender = "Male",
                    Homeworld = "Krypton",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Killer Frost",
                    RealName = "Caitlin Snow",
                    Gender = "Female",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.DC
                },
                new Character()
                {
                    Name = "Brainiac",
                    RealName = "N/A",
                    Gender = "Male",
                    Homeworld = "Colu",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.DC
                },

                //Marvel Characters
                new Character()
                {
                    Name = "Thor Odinson",
                    RealName = "N/A",
                    Gender = "Male",
                    Homeworld = "Asgard",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Iron Man",
                    RealName = "Tony Stark",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Ms. Marvel",
                    RealName = "Kamala Khan",
                    Gender = "Female",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Captain Marvel",
                    RealName = "Carol Danvers",
                    Gender = "Female",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "The Hulk",
                    RealName = "Bruce Banner",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.good,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Deadpool",
                    RealName = "Wade Wilson",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.anithero,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Punisher",
                    RealName = "Frank Castle",
                    Gender = "Male",
                    Homeworld = "Earth",
                    Alignments = Character.Alignment.anithero,
                    ComicUniverse = Character.Universe.Marvel
                },
                new Character()
                {
                    Name = "Thanos",
                    RealName = "N/A",
                    Gender = "Male",
                    Homeworld = "Titan",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.Marvel
                },
                 new Character()
                {
                    Name = "Galactus",
                    RealName = "N/A",
                    Gender = "Male",
                    Homeworld = "Universe before the big bang",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.Marvel
                },
                  new Character()
                {
                    Name = "Hela",
                    RealName = "N/A",
                    Gender = "Female",
                    Homeworld = "Asgard",
                    Alignments = Character.Alignment.evil,
                    ComicUniverse = Character.Universe.Marvel
                },
            };


            return characters;
        }

        #endregion */

        #region read and write to file

        /// <summary>
        /// read from file
        /// </summary>
        static List<Character> ReadFromDataFile()
        {
            List<Character> characters = new List<Character>();

            //read from datafile
            string[] characterStrings = File.ReadAllLines("Characters\\Characters.txt");

            foreach (string character in characterStrings)
            {
                //get character properties
                string[] characterProperties = character.Split('|');

                //create new character
                Character newChar = new Character();

                newChar.Name = characterProperties[0];

                newChar.RealName = characterProperties[1];

                newChar.Gender = characterProperties[2];

                newChar.Homeworld = characterProperties[3];

                Enum.TryParse(characterProperties[4], out Character.Alignment alignment);

                newChar.Alignments = alignment;

                Enum.TryParse(characterProperties[5], out Character.Universe universe);

                newChar.ComicUniverse = universe;

                //add character to the list
                characters.Add(newChar);

            }

            return characters;
        }

        /// <summary>
        /// write to data file
        /// </summary>
        static void WriteToDataFile(List<Character> characters)
        {
            string[] characterStrings = new string[characters.Count];

            //create the array
            for (int index = 0; index < characters.Count; index++)
            {
                string characterInfo =
                    characters[index].Name + "|" +
                    characters[index].RealName + "|" +
                    characters[index].Gender + "|" +
                    characters[index].Homeworld + "|" +
                    characters[index].Alignments + "|" +
                    characters[index].ComicUniverse;

                //add strings to array
                characterStrings[index] = characterInfo;
            }

            File.WriteAllLines("Characters\\Characters.txt", characterStrings);
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tWelcome To The Super Database!");
            Console.WriteLine();

            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tThank you for using The Super Database!");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("\tPress any key to exit");
            Console.ReadLine();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// return to main menu
        /// </summary>
        static void DisplayMainMenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to return to the main menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
