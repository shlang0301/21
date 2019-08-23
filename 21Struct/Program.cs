using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Struct
{

    public struct cd
    {
        public enum CardSuit : int
        {
            Clubs, Diamond, Hearts, Spades
        }

        public enum CardNumber : int
        {
            Six = 6, Seven, Eight, Nine, Ten, Jack = 2, Lady, King, Ace = 11
        }

        public static int MakeIdCard(CardSuit suit, CardNumber number)
        {
            int cardId = ((int)number) * 4 + ((int)suit);
            return cardId;
        }

        public static CardNumber makeCardNumber_fromCardId(int cardId)
        {
            return (CardNumber)(cardId / 4);
        }


        public static CardSuit MakeCardSuit_fromCardId(int cardNo)
        {
            return (CardSuit)(cardNo % 4);
        }

        public static int[] CreateAndShuffleDeck()
        {
            int[] massivId = new int[36];

            for (int i = 0; i < 36; i++)
            {
                if (i <= 11)
                {
                    massivId[i] = i + 8;
                }
                else
                {
                    massivId[i] = i + 12;
                }

            }

            Random random = new Random();

            for (int i = massivId.Length - 1; i >= 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = massivId[j];
                massivId[j] = massivId[i];
                massivId[i] = temp;

            }

            return massivId;

        }

    }

    public struct Player
    {
        public string playerName;
        public int scoreInGame;
        public int scoreWins;
        public int scoreAcesInGame;

        public static int GameMove(int[] array, ref int decrimValue)
        {

                decrimValue--;
                Console.Write("{0} {1} - {2} score", cd.makeCardNumber_fromCardId(array[decrimValue]), cd.MakeCardSuit_fromCardId(array[decrimValue]), Convert.ToInt32(cd.makeCardNumber_fromCardId(array[decrimValue])));
                Console.WriteLine();
                return array[decrimValue];
        }

        public static int GameMoveWithoutCW(int[] array, ref int decrimValue)
        {

            decrimValue--;
            return array[decrimValue];
        }
    }

    public struct Play
    {

        public Player player1;
        public Player player2;

        public static int DetermineWhoGoeFirst()
            {
            Random rand = new Random();
            int whoseFirstMove;
            whoseFirstMove = rand.Next(0, 2);
            Console.WriteLine("Player{0} is doing first move!", whoseFirstMove + 1);
            Console.WriteLine();
            return  whoseFirstMove;
    }
       
    }

   

    class Program
    {
        static void GoPlay()
        {
            Play p;

            p.player1 = new Player() { playerName = "Player1" };
            p.player2 = new Player() { playerName = "Player2" };            

            

            string flagContinueGame = "yes";

            int tempValueForLatestCW=0;

            p.player1.scoreWins = 0;
            p.player2.scoreWins = 0;

            while (flagContinueGame == "yes")
            {
                Console.Clear();

                int whoseFirstMove = Play.DetermineWhoGoeFirst();

                int[] array = cd.CreateAndShuffleDeck();

                int issuedCardPosition = 36;

                int issuedCardValue = 0;

                p.player1.scoreInGame = 0;
                p.player2.scoreInGame = 0;

                p.player1.scoreAcesInGame = 0;
                p.player2.scoreAcesInGame = 0;

                if (whoseFirstMove == 0)
                {
                    Console.WriteLine("First player took cards:");

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player1.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player1.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }

                    Console.WriteLine("Second player took cards:");

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player2.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player2.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }
                }
                else
                {
                    Console.WriteLine("Second player took cards:");

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player2.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }

                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player2.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }

                    Console.WriteLine("First player took cards:");
                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player1.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }
                    issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                    p.player1.scoreInGame += issuedCardValue;
                    if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }
                }

                Console.WriteLine();

                Console.WriteLine("Player1 - {0} score/ Player2 - {1} score", p.player1.scoreInGame, p.player2.scoreInGame);
                Console.WriteLine();

               

                for (; ; )
                {
                    
                    if (p.player1.scoreInGame == 21 || p.player1.scoreAcesInGame == 22 || (p.player2.scoreInGame > 21 && p.player1.scoreInGame < p.player2.scoreInGame))
                    {
                        Console.WriteLine("Player 1 won!");
                        p.player1.scoreWins++;
                        break;
                    }
                    else if (p.player2.scoreInGame == 21 || p.player2.scoreAcesInGame == 22 || (p.player1.scoreInGame > 21 && p.player2.scoreInGame < p.player1.scoreInGame))
                    {
                        Console.WriteLine("Player 2 won!");
                        p.player2.scoreWins++;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Do you want to add one card?");
                        if (whoseFirstMove == 0)
                        {
                            Console.WriteLine("Player1! Make your choice to add one card or not! 0 - now/any other numbers-yes ");
                            int player1ChoiceAddCard = Convert.ToInt32(Console.ReadLine());
                            if (player1ChoiceAddCard == 0)
                            {
                                Console.WriteLine("Player1 doesn't add any card!");
                            }
                            else
                            {
                                Console.WriteLine("Player1 took card:");
                                issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                                p.player1.scoreInGame += issuedCardValue;
                                if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }
                            }

                            Console.WriteLine("Player2 is doing his choise...  ");

                            Random rand = new Random();
                            int player2ChoiceAddCard = rand.Next(0, 2);

                            if (player2ChoiceAddCard == 0)
                            {
                                Console.WriteLine("Player2 doesn't add any card!");
                            }
                            else
                            {
                                Console.WriteLine("Player2 adds one card!");
                                Console.WriteLine("Player2 took card:");
                                issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                                p.player2.scoreInGame += issuedCardValue;
                                if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }
                            }
                            Console.WriteLine("Player1 - {0} score/ Player2 - {1} score", p.player1.scoreInGame, p.player2.scoreInGame);
                        }
                        else
                        {
                            Console.WriteLine("Player2 is doing his choise...");
                            Random rand = new Random();
                            int player2ChoiceAddCard = rand.Next(0, 2);

                            if (player2ChoiceAddCard == 0)
                            {
                                Console.WriteLine("Player2 doesn't add any card!");
                            }
                            else
                            {
                                Console.WriteLine("Player2 adds one card!");
                               issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMoveWithoutCW(array, ref issuedCardPosition)));
                               tempValueForLatestCW = array[issuedCardPosition];
                               p.player2.scoreInGame += issuedCardValue;
                               if (issuedCardValue == 11) { p.player2.scoreAcesInGame++; }
                            }

                            Console.WriteLine("Player1! Make your choice to add one card or not! 0 - now/any other numbers-yes  ");

                            int player1ChoiceAddCard = Convert.ToInt32(Console.ReadLine());

                            if (player1ChoiceAddCard == 0)
                            {
                                Console.WriteLine("Player1 doesn't add any card!");
                            }
                            else
                            {
                                Console.WriteLine("Player1 took card:");
                                issuedCardValue = Convert.ToInt32(cd.makeCardNumber_fromCardId(Player.GameMove(array, ref issuedCardPosition)));
                                p.player1.scoreInGame += issuedCardValue;
                                if (issuedCardValue == 11) { p.player1.scoreAcesInGame++; }
                            }                            
                            Console.Write("Player2 took card:{0} {1} - {2} score", cd.makeCardNumber_fromCardId(tempValueForLatestCW), cd.MakeCardSuit_fromCardId(tempValueForLatestCW), Convert.ToInt32(cd.makeCardNumber_fromCardId(tempValueForLatestCW)));
                            Console.WriteLine();
                            Console.WriteLine("Player1 - {0} score/ Player2 - {1} score", p.player1.scoreInGame, p.player2.scoreInGame);
                        }
                    }
                }

                Console.WriteLine("Do you want continue game? yes/no");

                flagContinueGame = Console.ReadLine();

            }

            Console.WriteLine("Games score:Player1 - {0}/ Player2 - {1}", p.player1.scoreWins, p.player2.scoreWins);

            Console.ReadLine();

     }        

        static void Main(string[] args)
        {

            GoPlay();       

        }
    }
}
