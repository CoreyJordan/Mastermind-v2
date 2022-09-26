using System;

class Program 
{
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
    string userStrGuess = Console.ReadLine(); 
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
            if (userStrGuess.Length == comboLength)
            {
              catchRouter = 2;
            }
            else
            {
              Console.WriteLine ("Guess must be {0} digits!", comboLength);
              userStrGuess = Console.ReadLine();
            }
            break;
          case 2:    // If all integers, pass to next case, else punt to case 1
            for (int i = 0; i < userStrGuess.Length; i++)
            {
              bool isDigit = Char.IsDigit(userStrGuess, i);
              if (!isDigit)
              {
                Console.WriteLine ("Guess must be all integers!");
                userStrGuess = Console.ReadLine();
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
            int userGuessCheck = Convert.ToInt32(userStrGuess);
            int userDigit1 = userGuessCheck / 1000;
            int userDigit2 = userGuessCheck / 100 % 10;
            int userDigit3 = userGuessCheck / 10 % 10;
            int userDigit4 = userGuessCheck % 10;

            if (userDigit1 < comboRangeLow || userDigit1 > comboRangeHigh ||
               userDigit2 < comboRangeLow || userDigit2 > comboRangeHigh ||
               userDigit3 < comboRangeLow || userDigit3 > comboRangeHigh ||
               userDigit4 < comboRangeLow || userDigit4 > comboRangeHigh)
            {
              Console.WriteLine ("Numbers must be in range {0} to {1}!",
                                 comboRangeLow, comboRangeHigh);
              userStrGuess = Console.ReadLine();
              catchRouter = 1;
            }
            else
            {
              guessPass = true;  // Passed all checks, dump out of switch
            }
            break;
        }
      }

      // Convert guess to int
      // Tried to 'globalize' userGuess but cleaner to just re-convert here, post check-pass
      int userGuess = Convert.ToInt32(userStrGuess);
      
      // Check the game state
      if (userGuess == combination)
      {
        gameOver = true;
        Console.WriteLine ("!!!CONGRATS!!! You figured it out in {0} guesses.", (turnCount + 1));
      }
      else if (turnCount == 9)
      {
        Console.WriteLine ("Better luck next time.");
        Console.WriteLine ("We were looking for " +combination);
        gameOver = true;
      }
      else
      {
        // Parse userGuess into 4 numbers
        int userFirst = userGuess / 1000;
        int userSecond = userGuess / 100 % 10;
        int userThird = userGuess / 10 % 10;
        int userFourth = userGuess % 10;
      
        // Compare the guess with the combination and tick indicators, tick 1 turn
        int guessCorrect = 0;
        int guessWrongSpot = 0;
        turnCount ++;

        // Evaluate first digit
        if (userFirst == digitFirst)
        {
          guessCorrect ++;
        }
        else if (userFirst == digitSecond ||
               userFirst == digitThird ||
               userFirst == digitFourth)
        {
            guessWrongSpot ++;
        }

        // Evaluate second digit
        if (userSecond == digitSecond) 
        {
          guessCorrect ++;
        }
        else if (userSecond == digitFirst ||
               userSecond == digitThird ||
               userSecond == digitFourth)
        {
          guessWrongSpot ++;
        }

        // Evaluate third digit
        if (userThird == digitThird)
        {
          guessCorrect ++;
        }
        else if (userThird == digitFirst ||
               userThird == digitSecond ||
               userThird == digitFourth)
        {
          guessWrongSpot ++;
        }

        // Evaluate fourth digit
        if (userFourth == digitFourth)
        {
          guessCorrect ++;
        }
        else if (userFourth == digitFirst ||
               userFourth == digitSecond ||
               userFourth == digitThird)
        {
          guessWrongSpot ++;
        }

        // Tell the user about their guess
        Console.WriteLine("     Try #{0} Correct: {1}, Move: {2}, Wrong: {3}, Try again: ",
                        turnCount, guessCorrect, guessWrongSpot, (4 - guessCorrect - guessWrongSpot));

        // Get a new guess
        userStrGuess = (Console.ReadLine());
      }
    } 
  }
}