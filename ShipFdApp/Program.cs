using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
//Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  class Program
  {
    // Globals
    static CSimulationManager manager = new CSimulationManager();
    // Local object
    static CShip ship = new CShip(); // Own ship
    static bool Terminate = false; // exit switch for app
    static double old_time = DateTime.Now.TimeOfDay.TotalSeconds;
    static bool test = false; // guard for testing for various RTI interface
    static void Main(string[] args)
    {
      // *************************************************
      // Initialization
      // *************************************************
      // Program initialization

      // UI initialization
      PrintVersion();
      Thread ConsoleKeyListener = new Thread(new ThreadStart(ProcessKeyboard));
      ConsoleKeyListener.Name = "KeyListener";

      // Racon Initialization
      // Getting the information/debugging messages from RACoN
      manager.federate.StatusMessageChanged += Federate_StatusMessageChanged;
      manager.federate.LogLevel = LogLevel.ALL;

      // initialization of Ship Properties
      Console.ForegroundColor = ConsoleColor.Gray;
      setShipConfiguration(); // get user input
      Console.Title = "ShipFdApp: " + ship.Callsign; // set console title
      manager.federate.FederationExecution.FederateName = ship.Callsign; // set feedrate name
      printConfiguration();// report to user
      ConsoleKeyListener.Start();// start keyboard event listener

      // *************************************************
      // Initialization
      // *************************************************
      // Federation Initialization
      // connect, create and join to federation execution, declare object model
      bool result = manager.federate.InitializeFederation(manager.federate.FederationExecution);
      // TM Initialization
      manager.federate.EnableAsynchronousDelivery();
      manager.federate.EnableTimeConstrained();
      // DDM Initialization
      if (ship.Location == LocationEnum.West)// create region according to the initial location
        manager.federate.CreateWestRegion();
      else
        manager.federate.CreateEastRegion();

      // FM Test
      manager.federate.ListFederationExecutions();
      //string name = federate.GetFederateName(federate.FederateHandle);
      //federate.GetFederateHandle(name);

      // *************************************************
      // Main Simulation Loop - loops until ESC is pressed
      // *************************************************
      old_time = DateTime.Now.TimeOfDay.TotalSeconds;
      do
      {
        // process rti events (callbacks) and tick
        if (manager.federate.FederateState.HasFlag(Racon.FederateStates.JOINED))
          manager.federate.Run();

        // Move our local ship
        ship.Move(GetTimeStep());

        if (test)
        {
          //  //manager.federate.FinalizeFederation(manager.federate.FederationExecution, ResignAction.NO_ACTION);
          //  //manager.federate.InitializeFederation(manager.federate.FederationExecution);
          //  manager.federate.ResignFederationExecution();
          //  manager.federate.JoinFederationExecution(manager.federate.FederationExecution.FederateName, manager.federate.FederationExecution.FederateType, manager.federate.FederationExecution.Name, manager.federate.FederationExecution.FomModules);
          //  test = false;
        }

        //// Zone transfer
        //if (ship.Location == LocationEnum.West) // New zone
        //{
        //  manager.federate.DeleteRegion(manager.federate.aor2);
        //  manager.federate.CreateWestRegion();
        //}
        //else
        //{
        //  manager.federate.DeleteRegion(manager.federate.aor1);
        //  manager.federate.CreateEastRegion();
        //}

        //// Request a zone transfer
        //if (ship.RequestTransfer)
        //{
        //  List<uint> federates = new List<uint>();
        //  federates.Add(0);
        //  manager.federate.RegisterFederationSynchronizationPoint("ZoneChangePoint", "this is a tag", federates);
        //  ship.RequestTransfer = false;
        //  ship.IsZoneTransferRequested = true;
        //}

      } while (!Terminate && !ship.Exit);

      // TM Tests
      manager.federate.DisableAsynchronousDelivery();
      manager.federate.DisableTimeConstrained();
      manager.federate.QueryLogicalTime();
      manager.federate.QueryLookahead(); // generates exception as this federate is TC.
      double galt;
      bool res = manager.federate.queryGALT(out galt);
      bool res2 = manager.federate.queryGALT();
      double lits;
      bool res4 = manager.federate.QueryLITS(out lits);
      bool res3 = manager.federate.QueryLITS();

      // *************************************************
      // Shutdown
      // *************************************************
      // Finalize user interface
      ConsoleKeyListener.Abort();
      // Finalize Federation Execution
      // Remove objects
      manager.timer.Stop(); // stop reporting the ship position
      manager.federate.DeleteObjectInstance(manager.ShipObjects[0]);
      // Leave and destroy federation execution
      bool result2 = manager.federate.FinalizeFederation(manager.federate.FederationExecution);
      //bool result2 = manager.federate.FinalizeFederation(manager.federate.FederationExecution, Racon.ResignAction.DELETE_OBJECTS);

      // Dumb trace log
      StreamWriter file = new System.IO.StreamWriter(@".\TraceLog.txt");
      file.WriteLine(manager.federate.TraceLog);
      file.Close();
      //Console.WriteLine(manager.federate.TraceLog.ToString());

      // Keep the console window open in debug mode.
      Console.WriteLine("Press any key to exit.");
      Console.ReadKey();
    }

    // Get time step
    private static double GetTimeStep()
    {
      // Calculate simulation step time
      double curr_time, dt;

      curr_time = DateTime.Now.TimeOfDay.TotalSeconds;
      dt = curr_time - old_time;
      old_time = curr_time;

      return dt;
    }
    // Process KB events
    private static void ProcessKeyboard()
    {
      do
      {
        // input processing
        switch (Console.ReadKey(true).Key)
        {
          // Print status (tracks etc.)
          case ConsoleKey.P:
            printStatus();
            break;
          // Send Message
          case ConsoleKey.S:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Message: ");
            var msg = Console.ReadLine();
            if (manager.IsRadioOn)
            {
              bool res = manager.federate.SendMessage(msg);
              if (res) Console.WriteLine("Radio message is sent.");
            }
            break;
          // test
          case ConsoleKey.T:
            Console.WriteLine("test.");
            test = true;
            break;
          case ConsoleKey.Escape:
            Terminate = true;
            break;
        }
      } while (true);
    }
    // Racon Information received
    private static void Federate_StatusMessageChanged(object sender, EventArgs e)
    {
      Console.ResetColor();
      Console.WriteLine((sender as CShipFdApp).StatusMessage);
    }
    // Set ship configuration
    private static void setShipConfiguration()
    {
      //Console.ForegroundColor = ConsoleColor.Yellow;
      //Console.WriteLine();
      //Console.Write("Enter Callsign: ");
      //ship.Callsign = Console.ReadLine();
      //int pos = 0;
      //do
      //{
      //  Console.Write("Enter Initial Location (0-west, 1-east): ");
      //  int.TryParse(Console.ReadLine(), out pos);
      //} while ((pos != 0) && (pos != 1));
      //ship.Heading = (LocationEnum)((pos == 0) ? 1 : 0); // heading is the reverse of position
      //ship.Location = (LocationEnum)(pos);
      //if (pos == 0)
      //{
      //  ship.Position.X = -10;
      //  ship.Position.Y = -1;
      //}
      //else
      //{
      //  ship.Position.X = 10;
      //  ship.Position.Y = 1;
      //}
      //int sp;
      //do
      //{
      //  Console.Write("Enter Speed (0-very slow, 1-slow, 2-fast, 3-very fast): ");
      //  int.TryParse(Console.ReadLine(), out sp);
      //} while ((sp < 0) || (sp > 3));
      //Console.WriteLine();
      //ship.Speed = (SpeedEnum)sp;

      // Initialization with default settings
      ship.Callsign = "Ship-" + DateTime.Now.Second.ToString();
      ship.Heading = LocationEnum.West;
      ship.Position.X = 10;
      ship.Position.Y = 1;
      ship.Speed = SpeedEnum.VerySlow;
      ship.Location = LocationEnum.East;

      // Encapsulate own ship and add to list
      CShipHlaObject encapsulatedShipObject = new CShipHlaObject(manager.federate.Som.ShipOC);
      encapsulatedShipObject.Ship = ship;
      manager.ShipObjects.Add(encapsulatedShipObject);
    }
    // Print ship configuration
    private static void printConfiguration()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("\nThanks for input! The local ship configuration:");
      Console.WriteLine("Callsign: {0}", ship.Callsign);
      Console.WriteLine($"Position: ({ship.Position.X}, {ship.Position.Y})");
      Console.WriteLine("Heading: {0}", ship.Heading);
      Console.WriteLine("Speed: {0}\n", ship.Speed);
    }
    // Print status
    private static void printStatus()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("*************** Status ***************");
      Console.WriteLine("Stations:");
      foreach (var station in manager.StationObjects)
      {
        Console.WriteLine($"{station.StationName}: {station.Location}");
      }
      Console.WriteLine("\nShips:");
      foreach (var item in manager.ShipObjects)
      {
        Console.WriteLine($"{item.Ship.Callsign}: {item.Ship.Heading}, {item.Ship.Speed}, ({item.Ship.Position.X}, {item.Ship.Position.Y})");
      }
      Console.WriteLine("**************************************");
    }
    private static void PrintVersion()
    {
      Console.WriteLine(
        "***************************************************************************\n"
        + "                        " + "ShipFdApp v1.0.0" + "\n"
        + "***************************************************************************");
    }

  }
}
