using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
//Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  public class Program
  {
    static CStationHlaObject station;
    static bool Terminate = false; // exit switch for app
    static public bool IsRadioOn = false;
    static double old_time = DateTime.Now.TimeOfDay.TotalSeconds;
    static PacingEnum Pacing; // for pacing control
    // Communication layer related structures
    static public CStationFdApp federate; //Application-specific federate 
    // Local data structures
    static public BindingList<CShipHlaObject> ShipObjects; // Keeps the ships in the environment
    static public BindingList<CTrackHlaObject> MyTracks; // Keeps the tracks monitored by this station
    static public BindingList<CTrackHlaObject> OtherTracks; // Keeps the tracks reported by other stations
    static public BindingList<CStationHlaObject> StationObjects; // Keeps the stations in the environment

    // Main Entrance for Application
    static void Main(string[] args)
    {
      // *************************************************
      // Initialization
      // *************************************************
      // Instantiation
      federate = new CStationFdApp();// Initialize the application-specific federate
      // Local data
      station = new CStationHlaObject(federate.Som.StationOC);// local object
      StationObjects = new BindingList<CStationHlaObject>();
      ShipObjects = new BindingList<CShipHlaObject>();
      MyTracks = new BindingList<CTrackHlaObject>();
      OtherTracks = new BindingList<CTrackHlaObject>();

      // UI initialization
      PrintVersion();
      Thread ConsoleKeyListener = new Thread(new ThreadStart(ProcessKeyboard));
      ConsoleKeyListener.Name = "KeyListener";

      // Racon Initialization
      // Getting the information/debugging messages from RACoN
      federate.StatusMessageChanged += Federate_StatusMessageChanged;
      federate.LogLevel = LogLevel.ALL;

      // initialization of station Properties
      Console.ForegroundColor = ConsoleColor.Yellow;
      setStationConfiguration(); // get user input
      Console.Title = "StationFdApp: " + station.StationName; // set console title
      printConfiguration();// report to user
      ConsoleKeyListener.Start();// start keyboard event listener

      // *************************************************
      // RTI Initialization
      // *************************************************
      // Initialize the federation execution
      federate.FederationExecution.FederateName = station.StationName; // set federate name
      federate.FederationExecution.Name = "Dardanelles";
      federate.FederationExecution.FederateType = "StationFederate";
      federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
      federate.FederationExecution.FDD = @".\StmsFom.xml";
      // Connect
      federate.Connect(CallbackModel.EVOKED, federate.FederationExecution.ConnectionSettings);
      // Create federation execution
      federate.CreateFederationExecution(federate.FederationExecution.Name, federate.FederationExecution.FomModules);
      // Join federation execution
      federate.JoinFederationExecution(federate.FederationExecution.FederateName, federate.FederationExecution.FederateType, federate.FederationExecution.Name, federate.FederationExecution.FomModules);
     
      // Declare Capability
      federate.DeclareCapability();

      //// Initialize the second federation execution
      //CStationFdApp federateForBosporus = new CStationFdApp();
      //federateForBosporus.StatusMessageChanged += Federate_StatusMessageChanged;
      //federateForBosporus.LogLevel = LogLevel.ALL;
      //federateForBosporus.FederationExecution.Name = "Bosporus";
      //federateForBosporus.FederationExecution.FederateType = "StationFederate";
      //federateForBosporus.FederationExecution.ConnectionSettings = "rti://127.0.0.1";
      //federateForBosporus.FederationExecution.FDD = @".\StmsFom.xml";
      //// Connect
      //federateForBosporus.Connect(CallbackModel.EVOKED, federateForBosporus.FederationExecution.ConnectionSettings);
      //// Create federation execution
      //federateForBosporus.CreateFederationExecution(federateForBosporus.FederationExecution.Name, federateForBosporus.FederationExecution.FomModules);
      //// Join federation execution
      //federateForBosporus.JoinFederationExecution(federateForBosporus.FederationExecution.FederateName, federateForBosporus.FederationExecution.FederateType, federateForBosporus.FederationExecution.Name, federateForBosporus.FederationExecution.FomModules);
      //// Declare Capability
      //federateForBosporus.DeclareCapability();

      // TM Initialization
      federate.EnableAsynchronousDelivery();
      //federate.EnableTimeRegulation(federate.Lookahead); // Lookahead is where this federate guarantees that it won't send any TSO message during this period
      federate.EnableTimeConstrained();

      // Various RTI Service Tests

      //// DDM tests
      //// DDM Initialization
      //if (station.Location == LocationEnum.West)// create region according to the location
      //  federate.CreateWestRegion();
      //else
      //  federate.CreateEastRegion();
      //// Create a list of attribute set and region set pairs
      //AttributeHandleSetRegionHandleSetPairVector pairs = new AttributeHandleSetRegionHandleSetPairVector();
      //// Construct the region set
      //List<HlaRegion> regions = new List<HlaRegion>();
      //regions.Add(federate.aor1);
      //// Populate the pairs. Here we use all the attributes of the object
      //pairs.Pairs.Add(new KeyValuePair<List<HlaAttribute>, List<HlaRegion>>(federate.Som.ShipOC.Attributes, regions));
      //federate.subscribeObjectClassAttributesWithRegions(federate.Som.ShipOC, pairs);
      //federate.RequestAttributeValueUpdateWithRegions(federate.Som.ShipOC, pairs, "user-supplied tag");
      //federate.UnsubscribeObjectClassWithRegions(federate.Som.ShipOC, pairs);

      //// FM tests
      //// Federate Save and Restore
      //federate.RequestFederationSave("Save_BeforeLunch");
      //federate.FederateSaveBegun();
      //// Operations for Application Specific Saving of Federate State
      //federate.FederateSaveComplete(true);
      //federate.QueryFederationSaveStatus();
      //federate.QueryFederationRestoreStatus();
      //// Support services
      //federate.Som.AreaOfResponsibility.Handle = federate.GetDimensionHandle(federate.Som.AreaOfResponsibility.Name);
      //federate.GetDimensionName(federate.Som.AreaOfResponsibility.Handle);

      // *************************************************
      // Main Simulation Loop - loops until ESC is pressed
      // *************************************************
      do
      {
        // process rti events (callbacks) and tick
        if (federate.FederateState.HasFlag(FederateStates.JOINED))
          federate.Run();

        // Track Manager
        for (int i = MyTracks.Count - 1; i >= 0; i--)
          MyTracks[i].ProcessTrack(station.Location); // safe way to remove an element

      } while (!Terminate);

      // Test OwM
      federate.QueryAttributeOwnership(station, federate.Som.StationOC.PrivilegeToDelete);
      bool res = federate.IsAttributeOwnedByFederate(station, federate.Som.StationOC.PrivilegeToDelete);

      //// Test for changing attribute order
      //List<Racon.RtiLayer.HlaAttribute> attributes = new List<Racon.RtiLayer.HlaAttribute>();
      //attributes.Add(federate.Som.StationOC.StationName);
      //federate.ChangeAttributeOrderType(station, attributes, Racon.OrderType.TimeStamp);// change the order type of attributes as TSO message

      //// Test for changing interaction order
      //federate.ChangeInteractionOrderType(federate.Som.RadioMessageIC, Racon.OrderType.Receive);

      // TM Tests
      federate.nextMessageRequest(federate.Time + 10);
      double time = federate.QueryLogicalTime();
      double lookahead = federate.QueryLookahead();
      double galt;
      bool res1 = federate.queryGALT(out galt);
      bool res2 = federate.queryGALT();
      double list;
      bool res4 = federate.QueryLITS(out list);
      bool res3 = federate.QueryLITS();

      //// HLA13 calls
      //time = federate.QueryFederateTime();
      //galt = federate.QueryLBTS();
      //list = federate.QueryMinNextEventTime();

      //Console.WriteLine("Lookahead: " + federate.QueryLookahead()); // intial lookahead
      //federate.ModifyLookahead(3); // modify lookahead
      //Console.WriteLine("Lookahead: " + federate.QueryLookahead()); // modified lookahead

      //federate.FlushQueueRequest(time + lookahead);

      // *************************************************
      // Shutdown
      // *************************************************
      // Finalize user interface
      ConsoleKeyListener.Abort();
      // Finalize Federation Execution
      // Remove objects
      federate.DeleteObjectInstance(StationObjects[0]);
      // Disable time management
      federate.DisableTimeRegulation();
      //federate.DisableTimeConstrained();
      federate.DisableAsynchronousDelivery();
      // Leave and destroy federation execution
      bool result2 = federate.FinalizeFederation(federate.FederationExecution, ResignAction.NO_ACTION);

      // Dumb trace log
      System.IO.StreamWriter file = new System.IO.StreamWriter(@".\TraceLog.txt");
      file.WriteLine(federate.TraceLog);
      file.Close();
      //Console.WriteLine(federate.TraceLog.ToString());

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
            if (IsRadioOn)
            {
              var timestamp = federate.Time + federate.Lookahead;
              bool res = federate.SendMessage(msg, timestamp);
              if (res)
              {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Radio message is sent with timestamp: {timestamp}, where logical time: {federate.Time} and lookahead: {federate.Lookahead}");

                // Request time advance - and then send message with new timestamp in the callback
                federate.TimeAdvanceRequest(federate.Time + federate.Lookahead);
                //federate.TimeAdvanceRequestAvailable(0.2);
              }
            }
            break;
          // Pacing control
          case ConsoleKey.C:
            Console.ForegroundColor = ConsoleColor.Yellow;
            int pacing = 0;
            bool cont = true;
            do
            {
              Console.Write("Pacing (0-SlowPacing, 1-FastPacing): ");
              bool res = int.TryParse(Console.ReadLine(), out pacing);
              if (res)
              {
                switch (pacing)
                {
                  case 0:
                    Pacing = (PacingEnum)pacing;
                    federate.RegisterFederationSynchronizationPoint(Pacing.ToString(), "Slow pacing is requested");
                    cont = false;
                    break;
                  case 1:
                    Pacing = (PacingEnum)pacing;
                    federate.RegisterFederationSynchronizationPoint(Pacing.ToString(), "Fast pacing is requested");
                    cont = false;
                    break;
                  default:
                    cont = true;
                    break;
                }
              }
            } while (cont);
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
      Console.WriteLine((sender as CStationFdApp).StatusMessage);
    }
    // Set ship configuration
    private static void setStationConfiguration()
    {
      //Console.WriteLine();
      //Console.Write("Enter Station Name: ");
      //station.StationName = Console.ReadLine();
      //int pos = 0;
      //do
      //{
      //  Console.Write("Enter Initial Location (0-west, 1-east): ");
      //  int.TryParse(Console.ReadLine(), out pos);
      //} while ((pos != 0) && (pos != 1));
      //station.Location = (LocationEnum)((pos == 0) ? LocationEnum.West : LocationEnum.West);

      // Quick Entry
      station.StationName = "GB" + DateTime.Now.Second.ToString();
      station.Location = LocationEnum.East;
      // Encapsulate 
      StationObjects.Add(station);
    }
    // Print configuration
    private static void printConfiguration()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("\nThanks for input! The station configuration:");
      Console.WriteLine("Callsign: {0}", station.StationName);
      Console.WriteLine("Location: {0}", station.Location);
    }
    // Print status
    private static void printStatus()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("*************** Status ***************");
      Console.WriteLine("Stations:");
      foreach (var station in StationObjects)
      {
        Console.WriteLine($"{station.StationName}: {station.Location}");
      }
      Console.WriteLine("\nTracks:");
      foreach (var track in MyTracks)
      {
        Console.WriteLine($"{track.TrackNo}: {track.TrackName} ({track.TrackPosition.X}, {track.TrackPosition.Y})");
      }
      Console.WriteLine("**************************************");
    }
    // Print version
    private static void PrintVersion()
    {
      Console.WriteLine(
        "***************************************************************************\n"
        + "                        " + "StationFdApp v1.0.0" + "\n"
        + "***************************************************************************");
    }

  }
}
