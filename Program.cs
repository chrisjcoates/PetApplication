﻿using System.ComponentModel.Design;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Pet
{
    public int Id { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public string Condition { get; set; }
    public string Personality { get; set; }
    public string Nickname { get; set; }

    public Pet(int id, string species, int age, string condition, string personality, string nickname)
    {
        Id = id;
        Species = species;
        Age = age;
        Condition = condition;
        Personality = personality;
        Nickname = nickname ?? "";
    }

    public static void PrintAttributes()
    {
        Console.WriteLine();
        Console.Write($"Id{new string(' ', 10 - "Id".Length)}");
        Console.Write($"Species{new string(' ', 15 - "Species".Length)}");
        Console.Write($"Age{new string(' ', 10 - "Age".Length)}");
        Console.Write($"Condition{new string(' ', 15 - "Condition".Length)}");
        Console.Write($"Personality{new string(' ', 15 - "Personality".Length)}");
        Console.Write($"Nickname{new string(' ', 15 - "Nickname".Length)}");
    }

    public void ListPet()
    {
        int idLength = Id.ToString().Length;
        int speciesLength = Species.ToString().Length;
        int ageLength = Age.ToString().Length;
        int conditionLength = Condition.ToString().Length;
        int personalityLength = Personality.ToString().Length;
        int nicknameLength = Nickname.ToString().Length;

        Console.Write($"{Id}{new string(' ', 10 - idLength)}");
        Console.Write($"{Species}{new string(' ', 15 - speciesLength)}");
        Console.Write($"{Age}{new string(' ', 10 - ageLength)}");
        Console.Write($"{Condition}{new string(' ', 15 - conditionLength)}");
        Console.Write($"{Personality}{new string(' ', 15 - personalityLength)}");
        Console.Write($"{Nickname}{new string(' ', 15 - nicknameLength)}");
        Console.WriteLine();
    }
}

class Program
{

    static void Main(string[] args)
    {

        List<Pet> ourAnimals = new List<Pet>
        {
            new(1, "Cat", 3, "Healthy", "Playfull", "Whiskers"),
            new(2, "Dog", 5, "Injured", "Loyal", "Buddy"),
            new(3, "Dog", 2, "Healthy", "Timid", "Floppy"),
            new(4, "Parrot", 4, "Recovered", "Talkative", "Chirpy"),
            new(5, "Hamster", 1, "Healthy", "Curious", "")
        };

        string[] menuItems = [
            "List all of our current pet information.",
            "Create new pet record.",
            "Edit a pet's record.",
            "Search for pet by 'Type' and a specified characteristic",
            "Enter 'q' to quit."
            ];

        bool runApp = true;

        // Application loop
        Console.Clear();
        do
        {
            Console.WriteLine("Welcome to the Pet Appilcation!\n");
            for (int i = 0; i < menuItems.Length - 1; i++)
            {
                Console.WriteLine($"{i + 1} - {menuItems[i]}");
            }
            Console.WriteLine(menuItems[^1]);
            Console.Write($"\nPlease select a number from 1 - {menuItems.Length}: ");
            string? userInput = Console.ReadLine();

            if (userInput != null)
            {
                if (userInput.ToLower() == "q")
                {
                    runApp = false;
                    Console.Clear();
                    continue;
                }
            }

            bool parseInput = int.TryParse(userInput, out int parsedUserInput);
            if (parseInput)
            {
                switch (parsedUserInput)
                {
                    case 1:
                        ListAllPets(ourAnimals);
                        break;
                    case 2:
                        CreatePetRecord(ourAnimals);
                        break;
                    case 3:
                        EditPetRecord(ourAnimals);
                        break;
                    case 4:
                        SearchPetCharacteristic(ourAnimals);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPlease enter a valid integer.\n");
            }


        } while (runApp);
    }

    static void ListAllPets(List<Pet> petList)
    {
        Console.Clear();
        Console.WriteLine("All Pets");
        Pet.PrintAttributes();
        Console.WriteLine("\n");
        foreach (Pet pet in petList)
        {
            pet.ListPet();
        }
        Console.WriteLine();
    }
    static void CreatePetRecord(List<Pet> petList)
    {
        Console.Clear();

        int id = 1;
        foreach (Pet pet in petList)
        {
            id++;
        }
        string species = "";
        int age = 0;
        string condition = "";
        string personality = "";
        string nickname = "";

        bool creatingNew = true;
        // Ask user for valid pet Type
        do
        {
            bool typeIsValid = false;
            do
            {
                Console.Write("Enter a pet species: ");
                string? petSpecies = Console.ReadLine();

                if (petSpecies == null || petSpecies == "")
                {
                    continue;
                }
                else
                {
                    species = petSpecies;
                    typeIsValid = true;
                }


            } while (!typeIsValid);

            // Ask user for valid pet age
            bool ageIsValid = false;
            do
            {
                Console.Write("Enter pets age: ");
                string? petAge = Console.ReadLine();
                bool isAgeInt = int.TryParse(petAge, out int petAgeInt);

                if (petAge == null || isAgeInt == false)
                {
                    continue;
                }
                else
                {
                    age += petAgeInt;
                    ageIsValid = true;
                }

            } while (!ageIsValid);

            // Ask user for valid pet condition
            bool conditionIsValid = false;
            do
            {
                Console.Write("Enter pets condition: ");
                string? petCondition = Console.ReadLine();

                Console.WriteLine(petCondition);

                if (petCondition == null || petCondition == "")
                {
                    continue;
                }
                else
                {
                    condition = petCondition;
                    conditionIsValid = true;
                }

            } while (!conditionIsValid);

            // Ask user for valid pet personality
            bool personalityIsValid = false;
            do
            {
                Console.Write("Enter pets personality: ");
                string? petPersonality = Console.ReadLine();

                Console.WriteLine(petPersonality);

                if (petPersonality == null || petPersonality == "")
                {
                    continue;
                }
                else
                {
                    personality = petPersonality;
                    personalityIsValid = true;
                }

            } while (!personalityIsValid);

            // Ask user for  pet nickname
            Console.Write("Enter pets nickname: ");
            string? petNickname = Console.ReadLine();

            if (petNickname != null)
            {
                nickname = petNickname;
            }

            creatingNew = false;

        } while (creatingNew);

        petList.Add(new Pet(id, species, age, condition, personality, nickname));

        Console.Clear();

        Console.WriteLine("\nNew pet added to database.\n");
    }
    static void EditPetRecord(List<Pet> petList)
    {
        Console.Clear();

        int petId = 0;

        ListAllPets(petList);
        bool idIsValid = false;
        do
        {
            Console.Write("Enter ID of pet to edit: ");
            string? selectedPet = Console.ReadLine();
            bool isInputInt = int.TryParse(selectedPet, out int parsedSelectedPet);
            if (selectedPet != null && isInputInt)
            {
                petId = parsedSelectedPet;
                idIsValid = true;
            }
            else
            {
                continue;
            }

        } while (!idIsValid);

        bool idFound = false;
        foreach (Pet pet in petList)
        {
            if (pet.Id == petId)
            {
                idFound = true;
                string species = "";
                int age = 0;
                string condition = "";
                string personality = "";
                string nickname = "";

                bool editing = true;
                // Ask user for valid pet Type
                do
                {
                    bool typeIsValid = false;
                    do
                    {
                        Console.Write("Enter a pet species: ");
                        string? petType = Console.ReadLine();

                        Console.WriteLine(petType);

                        if (petType == null || petType == "")
                        {
                            continue;
                        }
                        else
                        {
                            species = petType;
                            typeIsValid = true;
                        }


                    } while (!typeIsValid);

                    // Ask user for valid pet age
                    bool ageIsValid = false;
                    do
                    {
                        Console.Write("Enter pets age: ");
                        string? petAge = Console.ReadLine();
                        bool isAgeInt = int.TryParse(petAge, out int petAgeInt);

                        if (petAge == null || isAgeInt == false)
                        {
                            continue;
                        }
                        else
                        {
                            age = petAgeInt;
                            ageIsValid = true;
                        }

                    } while (!ageIsValid);

                    // Ask user for valid pet condition
                    bool conditionIsValid = false;
                    do
                    {
                        Console.Write("Enter pets condition: ");
                        string? petCondition = Console.ReadLine();

                        Console.WriteLine(petCondition);

                        if (petCondition == null || petCondition == "")
                        {
                            continue;
                        }
                        else
                        {
                            condition = petCondition;
                            conditionIsValid = true;
                        }

                    } while (!conditionIsValid);

                    // Ask user for valid pet personality
                    bool personalityIsValid = false;
                    do
                    {
                        Console.Write("Enter pets personality: ");
                        string? petPersonality = Console.ReadLine();

                        Console.WriteLine(petPersonality);

                        if (petPersonality == null || petPersonality == "")
                        {
                            continue;
                        }
                        else
                        {
                            personality = petPersonality;
                            personalityIsValid = true;
                        }

                    } while (!personalityIsValid);

                    // Ask user for  pet nickname
                    Console.Write("Enter pets nickname: ");
                    string? petNickname = Console.ReadLine();

                    if (petNickname != null)
                    {
                        nickname = petNickname;
                    }

                    editing = false;
                } while (editing);

                pet.Species = species;
                pet.Age = age;
                pet.Condition = condition;
                pet.Personality = personality;
                pet.Nickname = nickname;

                break;
            }
        }
        if (!idFound)
        {
            Console.WriteLine("No pet with ID found in database.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\nPet record sucessfuly edited.\n");
        }

    }

    //TODO update method to be able to search for characteristic in a sentance
    //TODO update so multiple characteristics can be added to the search
    //TODO add donation amount to pets, handle default amount
    static void SearchPetCharacteristic(List<Pet> petList)
    {
        Console.Clear();

        bool searchIsValid = false;
        do
        {
            Console.Write("Enter a species to search ('Dog'/'Cat'): ");
            string? speciesInput = Console.ReadLine();

            if (speciesInput == null) continue;

            bool speciesFound = false;
            foreach (Pet pet in petList)
            {
                if (pet.Species.ToLower() == speciesInput.ToLower())
                {
                    Console.WriteLine($"\nOne or more pets found for species '{speciesInput}'\n");
                    speciesFound = true;
                    break;
                }
            }
            if (speciesFound)
            {
                bool characteristicValid = false;
                do
                {
                    Console.Write("Enter a characteristic to search ('Playful'/'Curious'): ");

                    string? characteristic = Console.ReadLine();

                    if (characteristic == null) continue;

                    List<Pet> petsFound = new List<Pet> { };
                    foreach (Pet pet in petList)
                    {
                        if (pet.Personality.ToLower() == characteristic.ToLower())
                        {
                            petsFound.Add(pet);
                        }
                    }

                    int count = 0;
                    foreach (Pet pet in petsFound)
                    {
                        count++;
                    }

                    if (count > 0)
                    {
                        Pet.PrintAttributes();
                        Console.WriteLine("\n");
                        foreach (Pet pet in petsFound)
                        {
                            pet.ListPet();
                        }
                        Console.WriteLine();
                        characteristicValid = true; ;
                        searchIsValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"No pets with characteristic {characteristic} found.");
                        break;
                    }


                } while (!characteristicValid);


            }
            else
            {
                Console.WriteLine($"\nNo pets found with species '{speciesInput}'\n");
                break;
            }


        } while (!searchIsValid);
    }
}
