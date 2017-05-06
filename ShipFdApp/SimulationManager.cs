// **************************************************************************************************
//		CSimulationManager
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.8
//			at		: 	Saturday, November 19, 2016 9:17:24 PM
//		compatible with		: 	RACoN v.0.0.2.1
//
//		copyright		: 	(C) 2016 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// The Simulation Manager manages the (multiple) federation execution(s) and the (multiple instances of) joined federate(s).
/// </summary>

using System;
using System.ComponentModel;
using System.Timers;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  public class CSimulationManager
  {
    #region Declarations
    // Communication layer related structures
    public CShipFdApp federate; // Application-specific federate

    // Local data structures and the computational models
    public BindingList<CShipHlaObject> ShipObjects; // Keeps the ships in the environment
    public BindingList<CStationHlaObject> StationObjects; // Keeps the stations in the environment
    public Timer timer = new Timer(5000); // Timer to report the position periodically
    public bool IsRadioOn = false;
    #endregion //Declarations

    #region Constructor
    public CSimulationManager()
    {
      // Initalize class elements
      timer.Elapsed += TimerElapsed;

      // Initialize the application-specific federate
      federate = new CShipFdApp(this);
      // Initialize the federation execution
      federate.FederationExecution.Name = "Dardanelles";
      federate.FederationExecution.FederateType = "ShipFd";
      //federate.FederationExecution.ConnectionSettings = "rti://192.168.0.100";
      federate.FederationExecution.ConnectionSettings = "rti://127.0.0.1";

      // Time management
      federate.Lookahead = 1;

      // Handle RTI type variation
      initialize();

      // Populate the local ship list
      ShipObjects = new BindingList<CShipHlaObject>();
      StationObjects = new BindingList<CStationHlaObject>();
    }
    #endregion //Constructor

    #region Methods
    // Handles naming variation according to HLA specification
    private void initialize()
    {
      switch (federate.RTILibrary)
      {
        case RTILibraryType.HLA13_DMSO:
        case RTILibraryType.HLA13_Portico:
        case RTILibraryType.HLA13_OpenRti:
          federate.Som.ShipOC.Name = "ObjectRoot.Ship";
          federate.Som.ShipOC.PrivilegeToDelete.Name = "privilegeToDelete";
          federate.Som.StationOC.Name = "ObjectRoot.Station";
          federate.Som.StationOC.PrivilegeToDelete.Name = "privilegeToDelete";
          federate.Som.RadioMessageIC.Name = "InteractionRoot.RadioMessage";
          federate.FederationExecution.FDD = @".\StmsFom.fed";
          break;
        case RTILibraryType.HLA1516e_Portico:
        case RTILibraryType.HLA1516e_OpenRti:
          federate.Som.ShipOC.Name = "HLAobjectRoot.Ship";
          federate.Som.ShipOC.PrivilegeToDelete.Name = "HLAprivilegeToDeleteObject";
          federate.Som.StationOC.Name = "HLAobjectRoot.Station";
          federate.Som.StationOC.PrivilegeToDelete.Name = "HLAprivilegeToDeleteObject";
          federate.Som.RadioMessageIC.Name = "HLAinteractionRoot.RadioMessage";
          federate.FederationExecution.FDD = @".\StmsFom.xml";
          break;
      }
    }
    // Update Ship Position
    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
      // Update postition and report
      federate.UpdatePosition(ShipObjects[0]);
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine($"{ShipObjects[0].Ship.Callsign}: {ShipObjects[0].Ship.Heading}, {ShipObjects[0].Ship.Speed}, ({ShipObjects[0].Ship.Position.X}, {ShipObjects[0].Ship.Position.Y})");

      //// report all ships
      //foreach (var item in ShipObjects)
      //{
      //  Console.WriteLine($"{item.Ship.Callsign}: ({item.Ship.Position.X}, {item.Ship.Position.Y}), {item.Ship.Heading}, {item.Ship.Speed}");
      //}

      // Force a garbage collection to occur.
      GC.Collect();
    }
    #endregion //Methods
  }
}
