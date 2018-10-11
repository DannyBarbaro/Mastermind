using System;

namespace Mastermind_Barbaro
{
    class MainClass
    {
        //main function of the code
        public static void Main(string[] args)
        {
            //first prints the instructions of the game
            Console.WriteLine("Welcome to Mastermind!\nThe computer will generate a secret code which is a 4 digit number with each digit " +
                              "being between 1 and 6.\nThe user will play as the code breaker.\nThe code breaker will get the chance to " +
                              "figure out the code.\nAfter the user puts in their guess the system will respond with a series of +'s and -'s\n" +
                              "For each number in the guess that matches the number and position of a number in the secret code, the score includes one +\n" +
                              "For each number in the guess that matches the number but not the position of a number in the secret code, the score includes one -\n" +
                              "If the code breaker guesses correctly, you win!\nIf the code breaker runs out of tries, you lose\n\n\n");

            //asks the user for the number of guesses they want
            int numGuesses = numberOfGuesses();

            //generates a secret code
            int[] code = generateSecretCode();

            //keeps track of the total number of code breaker guesses
            int totalGuesses = 0;

            //tells if the game has been won or not 
            bool win = false;

            //loop to run the game while you have guesses left and haven't won
            while (totalGuesses < numGuesses && !win)
            {
                //gets the user guess
                int[] input = processGuess();
                //calculates the score for the guess
                string score = calcScore(input, code);
                //checks if code breaker wins. if not give the user the output
                if (score.Equals("+ + + + "))
                {
                    win = true;
                }
                else
                {
                    Console.WriteLine("output: " + score + "\n");
                }

                //increment the number of guesses the user has made
                totalGuesses++;
            }

            //prints the winning or losing statement
            if (win)
            {
                Console.WriteLine("\nYou Cracked the Code!");
            }
            else
            {
                Console.WriteLine("\nYou Lose :(");
            }
        }

        //method that prompts the user for a number of guesses and makes sure they
        public static int numberOfGuesses()
        {
            //prompt the user for guess input
            Console.WriteLine("How many guesses would you like?");
            int input = 0;

            //check that they give a valid input
            if (int.TryParse(Console.ReadLine(), out input) && input > 0)
            {
                if (input == 1)
                {
                    Console.WriteLine("You have " + input + " guess to crack the code. Good Luck!\n\n");
                }
                else
                {
                    Console.WriteLine("You have " + input + " guesses to crack the code. Good Luck!\n\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid input entered\n");
                numberOfGuesses();
            }
            return input;
        }

        //method to generate the secret code for the user to guess
        public static int[] generateSecretCode()
        {
            int[] code = new int[4];
            //provide a seed for the random method
            Random rnd = new Random(DateTime.Now.Day * 23 + DateTime.Now.Millisecond);
            //loop to create the random secret code
            for (int i = 0; i < code.Length; i++)
            {
                code[i] = rnd.Next(1, 7);
            }
            return code;
        }

        //method to take the input from the code breaker
        public static int[] processGuess()
        {
            int[] output = new int[4];
            //Prompt the user to enter a code
            Console.WriteLine("Input your code guess");
            char[] input = (Console.ReadLine()).ToCharArray();
            //checks that the input is the right length
            if (input.Length == output.Length)
            {
                //loop to put the values from the input into the array for the guess
                for (int i = 0; i < output.Length; i++)
                {
                    int val = (int)Char.GetNumericValue(input[i]);
                    if (val >= 1 && val <= 6)
                    {
                        output[i] = val;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input entered\n");
                        processGuess();
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input entered\n");
                processGuess();
            }
            return output;
        }

        //takes the input and the secret code and calculates the + and - score for the guess
        public static string calcScore(int[] input, int[] code)
        {
            string score = "";
            int[] codeCopy = new int[code.Length];
            int[] inputCopy = new int[input.Length];
            //loops through to find values that are same position and value (the +'s)
            for (int i = 0; i < code.Length; i++)
            {
                if (input[i] == code[i])
                {
                    score += "+ ";
                    inputCopy[i] = 0;
                    codeCopy[i] = 0;
                }
                else
                {
                    inputCopy[i] = input[i];
                    codeCopy[i] = code[i];
                }
            }
            //loop that finds values that are the same but wrong position (the -'s)
            for (int i = 0; i < inputCopy.Length; i++)
            {
                for (int j = 0; j < codeCopy.Length; j++)
                {
                    if (inputCopy[i] != 0 && inputCopy[i] == codeCopy[j])
                    {
                        score += "- ";
                        inputCopy[i] = 0;
                        codeCopy[j] = 0;
                    }
                }
            }
            return score;
        }
    }
}

