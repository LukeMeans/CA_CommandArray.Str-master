using FinchAPI;
using System;
using System.Collections.Generic;

namespace CommandArray
{
    // *************************************************************
    // add comment block here
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;

            int delayDuration = 0;
            int numberOfCommands = 0;
            int motorSpeed = 0;
            int LEDBrightness = 0;
            // FinchCommand[] commands = null;
            List<FinchCommand> commands = new List<FinchCommand>();

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) get finch robot commands ");
                Console.WriteLine("\t3) display finch robot commands ");
                Console.WriteLine("\t4) execute finch robot commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        DisplayGetFinchCommands(commands, numberOfCommands);
                        break;
                    case "3":
                        DisplayFinchCommands(commands);
                        break;
                    case "4":
                        DisplayExecuteCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayExecuteCommands(Finch myFinch, List<FinchCommand> commands, int motorSpeed, int lEDBrightness, int delayDuration)
        {
            DisplayHeader("execute finch commands");

            Console.WriteLine("click any key when ready to execute commands");
            DisplayContinuePrompt();
            for (int index = 0; index < commands.Count; index++)
            {
                Console.WriteLine($"command:{commands[index]}");

                switch (commands[index])
                {
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        myFinch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        myFinch.wait(delayDuration);
                        break;
                    case FinchCommand.TURNRIGHT:
                        myFinch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        myFinch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        myFinch.setLED(lEDBrightness, lEDBrightness, lEDBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        myFinch.setLED(0, 0, 0);
                        break;
                    default:
                        break;
                }
            }
            DisplayContinuePrompt();
            
        }

        static void DisplayGetFinchCommands(List<FinchCommand> commands, int numberOfCommands)
        {

            FinchCommand command;
            DisplayHeader("get finch commands");

            for (int index = 0; index < numberOfCommands; index++)
            {
                Console.Write($"command #{index + 1}:");

               Enum.TryParse(Console.ReadLine(), out command);
                commands.Add(command);
            }
            Console.WriteLine();
            Console.WriteLine("the commands:");
            foreach (FinchCommand finchcommand in commands)
            {
                Console.WriteLine("\t" + finchcommand);
            }
            DisplayContinuePrompt();
         

           
        }
        static void DisplayFinchCommands(List<FinchCommand> commands)
        {

            DisplayHeader("Finch Commands");

            if (commands != null)
            {
                Console.WriteLine("The commands:");
                foreach (FinchCommand command in commands)
                {
                    Console.WriteLine(command);
                }
            }
            else
            {
                Console.WriteLine("Please enter Finch Robot commands first.");
            }

            DisplayContinuePrompt();

        }

        static int DisplayGetDelayDuration()
        {
            int DelayDuration;
            string userResponse;

            DisplayHeader("length od delay");

            Console.WriteLine("enter length of delay(miliseconds):");
            userResponse = Console.ReadLine();
            //DelayDuration = int.Parse(userResponse);
            //DelayDuration = Convert.ToInt32(userResponse);
            int.TryParse(userResponse, out DelayDuration);

            //int.TryParse(Console.ReadLine(), out DelayDuration);

            DisplayContinuePrompt();

            return DelayDuration;
        }
        static int DisplayGetMotorSpeed()
        {
            int MotorSpeed;
            string userResponse;

            DisplayHeader("Motor Speed");

            Console.WriteLine("enter the motor speed [1 - 255]");
            userResponse = Console.ReadLine();
            //DelayDuration = int.Parse(userResponse);
            //DelayDuration = Convert.ToInt32(userResponse);
            int.TryParse(userResponse, out MotorSpeed);

            //int.TryParse(Console.ReadLine(), out DelayDuration);

            DisplayContinuePrompt();

            return MotorSpeed;
        }
        static int DisplayGetLEDBrightness()
        {
            int LEDBrightness;
            string userResponse;

            DisplayHeader("LED Brightness");

            Console.WriteLine("enter LED Brightness [1 - 255]");
            userResponse = Console.ReadLine();
            //DelayDuration = int.Parse(userResponse);
            //DelayDuration = Convert.ToInt32(userResponse);
            int.TryParse(userResponse, out LEDBrightness);

            //int.TryParse(Console.ReadLine(), out DelayDuration);

            DisplayContinuePrompt();

            return LEDBrightness;
        }

        /// <summary>
        /// get the number of commands from the user
        /// </summary>
        /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
        {
            int numberOfCommands;
            string userResponse;

            DisplayHeader("Number of Commands");

            Console.Write("Enter the number of commands:");
            userResponse = Console.ReadLine();

            numberOfCommands = int.Parse(userResponse);

            return numberOfCommands;
        }

        /// <summary>
        /// initialize and confirm the finch connects
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
        {
            DisplayHeader("Initialize the Finch");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            while (!myFinch.connect())
            {
                Console.WriteLine("Please confirm the Finch Robot is connect");
                DisplayContinuePrompt();
            }

            FinchConnectedAlert(myFinch);
            Console.WriteLine("Your Finch Robot is now connected");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// audio notification that the finch is connected
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
        {
            myFinch.setLED(0, 255, 0);

            for (int frequency = 17000; frequency > 100; frequency -= 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(10);
            }

            myFinch.noteOff();
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\tProgram Your Finch");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen and disconnect finch robot
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tThank You!");
            Console.WriteLine();

            myFinch.disConnect();

            DisplayContinuePrompt();
        }

        #region HELPER  METHODS

        /// <summary>
        /// display header
        /// </summary>
        /// <param name="header"></param>
        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        /// <summary>
        /// display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to contiinue.");
            Console.ReadKey();
        }

        #endregion
    }
}
