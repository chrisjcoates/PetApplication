using System.ComponentModel.Design;
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
            new(3, "Rabbit", 2, "Healthy", "Timid", "Floppy"),
            new(4, "Parrot", 4, "Recovered", "Talkative", "Chirpy"),
            new(5, "Hamster", 1, "Healthy", "Curious", "")
        };

        string[] menuItems = [
            "List all of our current pet information.",
            "Create new pet record.",
            "Edit an animal's record.",
            "Enter 'q' to quit."
            ];

        bool runApp = true;

        // Application loop
        Console.Clear();
        do
        {
            Console.WriteLine("Welcome to the Pet Appilcation!\n");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {menuItems[i]}");
            }
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

        int id = 0;
        foreach (Pet pet in petList)
        {
            id++;
        }
        string type = "";
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
                Console.Write("Enter a pet Type: ");
                string? petType = Console.ReadLine();

                Console.WriteLine(petType);

                if (petType == null || petType == "")
                {
                    continue;
                }
                else
                {
                    type = petType;
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

        petList.Add(new Pet(id, type, age, condition, personality, nickname));

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
                string type = "";
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
                        Console.Write("Enter a pet Type: ");
                        string? petType = Console.ReadLine();

                        Console.WriteLine(petType);

                        if (petType == null || petType == "")
                        {
                            continue;
                        }
                        else
                        {
                            type = petType;
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
}