using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscramblerProgram.Data;
using WordUnscramblerProgram.Workers;
//code clean up
//a string literal is having the string value, like in Console.WriteLine
//created constants class

namespace WordUnscramblerProgram
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();       
        
        //start with skelton
        static void Main(string[] args)
        {
            try
            {
                bool continueWordUnscrambler = true;
                do
                {
                    Console.WriteLine(Constants.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            Console.Write(Constants.EnterScrambledWordsViaFile);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case Constants.Manual:
                            Console.Write(Constants.EnterScrambledWordsManually);
                            ExecuteScrambledWordsInManualEntryScenario();
                            break;
                        default:
                            Console.WriteLine(Constants.EnterScrambledWordsOptionNotRecognized);
                            break;
                    }

                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                        continueDecision = Console.ReadLine() ?? string.Empty;

                    } while (
                        !continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                        !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscrambler = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscrambler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
        }

        private static void ExecuteScrambledWordsInManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(filename);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);
            }
            
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }
}
