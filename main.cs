using System;

class Program 
{
  private static int intUserGuess, userDigit1, userDigit2, userDigit3, userDigit4;
  private static string userGuess;
  
  public static void Main (string[] args) 
  {
        // "Global" variables
    int comboRangeLow = 1;    // Lower bound of possible combo digits, default: Easy
    int comboRangeHigh = 6;   // Upper bound of possible combo digits, default: Easy
    int comboLength = 4;      // Length of combination, default: Easy
    
    Console.WriteLine ("I will create a non-repeating random 4 digit number\n" +
                      "using only the numbers {0} through {1}.\n\n" +
                      "Input your own guess and I will tell you how close you are:\n" +
                      "I will tell you how many digits you have guessed correctly\n" +
                      "and whether or not they are in the right location.\n\n" +
                      "BUT be careful, I won't tell you which ones are which.\n\n" +
                      "You have ten tries. Good luck.", comboRangeLow, comboRangeHigh);
  
    // Generate 4 random digits
    Random rnd = new Random();
    int digitFirst = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    int digitSecond = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    int digitThird = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    int digitFourth = rnd.Next(comboRangeLow, (comboRangeHigh + 1));

    // Ensure the number is non repeating
    // Check second digit against first
    while (digitSecond == digitFirst) 
    {
      digitSecond = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    }
    // Check third digit against first 2
    while (digitThird == digitFirst || digitThird == digitSecond) 
    {
      digitThird = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    }
    // Check fourth digit against first 3
    while (digitFourth == digitFirst ||
           digitFourth == digitSecond ||
           digitFourth == digitThird) 
    {
      digitFourth = rnd.Next(comboRangeLow, (comboRangeHigh + 1));
    }

    // Create combination, parse the random numbers into 1 4 digit integer
    int combination = int.Parse(digitFirst.ToString() +
                                digitSecond.ToString() +
                                digitThird.ToString() +
                                digitFourth.ToString());


    
    // ***Main Program***
    int turnCount = 0;
    bool gameOver = false;
    
    // Get 4 digit number from user
    Console.WriteLine ("Enter your first guess: ");
    Program.userGuess = Console.ReadLine(); 
    while (!gameOver)
    {
      // Catch block for user input, verify digit count, numerical, range
      bool guessPass = false;
      int catchRouter = 1;
      while (!guessPass)
      {
        switch (catchRouter)
        {
          case 1:     // If same length as combination, pass to next case
            if (Program.userGuess.Length == comboLength)
            {
              catchRouter = 2;
            }
            else
            {
              Console.WriteLine ("Guess must be {0} digits!", comboLength);
              Program.userGuess = Console.ReadLine();
            }
            break;
          case 2:    // If all integers, pass to next case, else punt to case 1
            for (int i = 0; i < Program.userGuess.Length; i++)
            {
              bool isDigit = Char.IsDigit(Program.userGuess, i);
              if (!isDigit)
              {
                Console.WriteLine ("Guess must be all integers!");
                Program.userGuess = Console.ReadLine();
                catchRouter = 1;
                break;
              }
              catchRouter = 3;
            }
            break;
          case 3:    // If all numbers in range, pass, else punt to case 1
            
            // TODO Going to be tricky to allow difficulty variance here
            // TODO Maybe Nested Ifs (difficulty > j)????
            // TODO ??? Create an array and loop through??? (Learn arrays)
            
            // Safe to convert guess to int
            Program.intUserGuess = Convert.ToInt32(Program.userGuess);
            Program.userDigit1 = Program.intUserGuess / 1000;
            Program.userDigit2 = Program.intUserGuess / 100 % 10;
            Program.userDigit3 = Program.intUserGuess / 10 % 10;
            Program.userDigit4 = Program.intUserGuess % 10;

            if (Program.userDigit1 < comboRangeLow || Program.userDigit1 > comboRangeHigh ||
               Program.userDigit2 < comboRangeLow || Program.userDigit2 > comboRangeHigh ||
               Program.userDigit3 < comboRangeLow || Program.userDigit3 > comboRangeHigh ||
               Program.userDigit4 < comboRangeLow || Program.userDigit4 > comboRangeHigh)
            {
              Console.WriteLine ("Numbers must be in range {0} to {1}!",
                                 comboRangeLow, comboRangeHigh);
              Program.userGuess = Console.ReadLine();
              catchRouter = 1;
            }
            else
            {
              guessPass = true;  // Passed all checks, dump out of switch
            }
            break;
        }
      }
      
      // Check the game state
      if (Program.intUserGuess == combination)
      {
        gameOver = true;
        Console.WriteLine ("!!!CONGRATS!!! You figured it out in {0} guesses.", (turnCount + 1));
        Console.ReadLine();
      }
      else if (turnCount == 9)
      {
        Console.WriteLine ("Better luck next time.");
        Console.WriteLine ("We were looking for " +combination);
        gameOver = true;
        Console.ReadLine();
      }
      else
      {      
        // Compare the guess with the combination and tick indicators, tick 1 turn
        int guessCorrect = 0;
        int guessWrongSpot = 0;
        turnCount ++;

        // Evaluate first digit
        if (Program.userDigit1 == digitFirst)
        {
          guessCorrect ++;
        }
        else if (Program.userDigit1 == digitSecond ||
               Program.userDigit1 == digitThird ||
               Program.userDigit1 == digitFourth)
        {
            guessWrongSpot ++;
        }

        // Evaluate second digit
        if (Program.userDigit2 == digitSecond) 
        {
          guessCorrect ++;
        }
        else if (Program.userDigit2 == digitFirst ||
               Program.userDigit2 == digitThird ||
               Program.userDigit2 == digitFourth)
        {
          guessWrongSpot ++;
        }

        // Evaluate third digit
        if (Program.userDigit3 == digitThird)
        {
          guessCorrect ++;
        }
        else if (Program.userDigit3 == digitFirst ||
               Program.userDigit3 == digitSecond ||
               Program.userDigit3 == digitFourth)
        {
          guessWrongSpot ++;
        }

        // Evaluate fourth digit
        if (Program.userDigit4 == digitFourth)
        {
          guessCorrect ++;
        }
        else if (Program.userDigit4 == digitFirst ||
               Program.userDigit4 == digitSecond ||
               Program.userDigit4 == digitThird)
        {
          guessWrongSpot ++;
        }

        // Tell the user about their guess
        Console.WriteLine("     Try #{0} Correct: {1}, Move: {2}, Wrong: {3}, Try again: ",
                        turnCount, guessCorrect, guessWrongSpot, (4 - guessCorrect - guessWrongSpot));

        // Get a new guess
        Program.userGuess = (Console.ReadLine());
      }
    } 
  }
}