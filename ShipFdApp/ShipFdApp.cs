// **************************************************************************************************
//		CShipFdApp
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.8
//			at		: 	Friday, November 18, 2016 7:11:23 PM
//		compatible with		: 	RACoN v.0.0.2.1
//
//		copyright		: 	(C) 2016 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// The application specific federate that is extended from the Generic Federate Class of RACoN API. This file is intended for manual code operations.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  // CShipFdApp extends and specializes the generic federate
  public partial class CShipFdApp : Racon.CGenericFederate
  {
    #region Manually Added Code

    // Local Data
    private CSimulationManager manager;
    private object thisLock = new object();
    // DDM Declarations
    public HlaRegion aor1;
    public HlaRegion aor2;

    #region Constructor
    public CShipFdApp(CSimulationManager parent) : this()
    {
      manager = parent; // Set simulation manager
      // Create regions manually
    }
    #endregion //Constructor

    // Cut and paste the callbacks that you want to modify from the Generated file (ShipFdApp.simge.cs)
    #region Federation Management Callbacks
    // FdAmb_OnSynchronizationPointRegistrationConfirmedHandler
    public override void FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(sender, data);

      #region User Code
      // do nothing
      #endregion //User Code
    }
    // FdAmb_OnSynchronizationPointRegistrationFailedHandler
    public override void FdAmb_OnSynchronizationPointRegistrationFailedHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_OnSynchronizationPointRegistrationFailedHandler(sender, data);

      #region User Code
      // do nothing
      #endregion //User Code
    }
    // FdAmb_SynchronizationPointAnnounced
    public override void FdAmb_SynchronizationPointAnnounced(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_SynchronizationPointAnnounced(sender, data);

      #region User Code
      PacingEnum Pacing;
      Enum.TryParse(data.Label, out Pacing);
      switch (Pacing)
      {
        case PacingEnum.SlowPacing:
          manager.ShipObjects[0].Ship.Speed = SpeedEnum.VerySlow;
          break;
        case PacingEnum.FastPacing:
          manager.ShipObjects[0].Ship.Speed = SpeedEnum.VeryFast;
          break;
        default:
          break;
      }
      // Report that the ship is adjusted her speed according to the requested pacing
      manager.federate.SynchronizationPointAchieved(data.Label, true);
      #endregion //User Code
    }
    // FdAmb_FederationSynchronized
    public override void FdAmb_FederationSynchronized(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_FederationSynchronized(sender, data);

      #region User Code
     Report($"simulation pacing ({data.Label}) is completed." + Environment.NewLine);
      #endregion //User Code
    }
    #endregion

    #region Declaration Management Callbacks
    // Turn Interactions Off
    public override void FdAmb_TurnInteractionsOffAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnInteractionsOffAdvisedHandler(sender, data);

      #region User Code
      manager.IsRadioOn = false;
      #endregion //User Code
    }
    // Turn Interactions On
    public override void FdAmb_TurnInteractionsOnAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnInteractionsOnAdvisedHandler(sender, data);

      #region User Code
      manager.IsRadioOn = true;
      #endregion //User Code
    }
    // Start Registration
    public override void FdAmb_StartRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_StartRegistrationForObjectClassAdvisedHandler(sender, data);

      #region User Code
      // Check that this is for the ShipOC
      if (data.ObjectClassHandle == Som.ShipOC.Handle)
        RegisterObject(manager.ShipObjects[0]);

      manager.timer.Start(); // move this to turn on attribute update callback
      #endregion //User Code
    }
    // Stop Registration
    public override void FdAmb_StopRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_StopRegistrationForObjectClassAdvisedHandler(sender, data);

      #region User Code
      manager.timer.Stop(); // move this to turn off attribute update callback
      #endregion //User Code
    }

    #endregion //Declaration Management Callbacks

    #region Object Management Callbacks
    // Interaction Received
    public override void FdAmb_InteractionReceivedHandler(object sender, HlaInteractionEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_InteractionReceivedHandler(sender, data);

      #region User Code
      // Which interaction class?
      if (data.Interaction.ClassHandle == Som.RadioMessageIC.Handle)
      {
        string sentBy = "";
        string msg = "";
        var ts = new DateTime();

        // Get parameter values
        // 1st Method: Check which parameter is updated
        if (data.IsValueUpdated(Som.RadioMessageIC.Callsign))
          sentBy = data.GetParameterValue<string>(Som.RadioMessageIC.Callsign);
        if (data.IsValueUpdated(Som.RadioMessageIC.Message))
          msg = data.GetParameterValue<string>(Som.RadioMessageIC.Message);
        if (data.IsValueUpdated(Som.RadioMessageIC.TimeStamp))
          ts = data.GetParameterValue<DateTime>(Som.RadioMessageIC.TimeStamp);

        // 2nd method: iterate through parameter set
        //foreach (var item in data.Interaction.Parameters)
        //{
        //  if (Som.RadioMessageIC.Callsign.Handle == item.Handle) sentBy = item.GetValue<string>();
        //  else if (Som.RadioMessageIC.Message.Handle == item.Handle) msg = item.GetValue<string>();
        //  else if (Som.RadioMessageIC.TimeStamp.Handle == item.Handle) ts = item.GetValue<DateTime>(); // must match with AddValue() type
        //}
        // TSO message - additional properties
        double timestamp = data.Time; // timestamp in case of a message sent with a timestamp
        var handle = data.SupplementalReceiveInfo.ProducingFederateHandle; // returns the federate handle of the federate, which produces this message
        var retractionHandle = data.RetractionHandle; // returns event retraction handle in acse of a TSO message

        Report($"RadioMessage: From: {sentBy} > {msg} at {ts} with logical timestamp {timestamp}"+ Environment.NewLine);
      }
      #endregion //User Code
    }
    public override void FdAmb_ObjectDiscoveredHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ObjectDiscoveredHandler(sender, data);

      #region User Code
      // Check the class type of the discovered object
      if (data.ClassHandle == Som.ShipOC.Handle) // A ship
      {
        // Create and add a new ship to the list
        CShipHlaObject newShip = new CShipHlaObject(data.ObjectInstance);
        newShip.Type = Som.ShipOC;// if user forgets to set type of the object, then an exception generated
        manager.ShipObjects.Add(newShip);

        // Request Update Values of Attributes        
        // (1) Request update values of all attributes for a specific object instance
        //RequestAttributeValueUpdate(newShip);


        // (2) Request an update for all attribute values of all object instances of a specific object class
        //RequestAttributeValueUpdate(Som.ShipOC);

        // (3) Request Update Values for specific attributes only
        List<HlaAttribute> attributes = new List<HlaAttribute>();
        attributes.Add(Som.ShipOC.Callsign);
        RequestAttributeValueUpdate(newShip, attributes);
      }
      else if (data.ClassHandle == Som.StationOC.Handle) // A station
      {
        // Create and add a new station to the list
        CStationHlaObject newStation = new CStationHlaObject(data.ObjectInstance);
        newStation.Type = Som.StationOC;
        manager.StationObjects.Add(newStation);

        // Request Update Values of Attributes        
        RequestAttributeValueUpdate(newStation, null);
      }
      #endregion //User Code
    }
    // Attribute Value Update is Requested
    public override void FdAmb_AttributeValueUpdateRequestedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeValueUpdateRequestedHandler(sender, data);

      #region User Code
      // !!! If this federate is created only one object instance, then it is sufficient to check the handle of that object, otherwise we need to check all the collection
      if (data.ObjectInstance.Handle == manager.ShipObjects[0].Handle)
      {
        // We can further try to figure out the attributes for which update is requested.
        //foreach (var item in data.ObjectInstance.Attributes)
        //{
        //  if (item.Handle == Som.ShipOC.Callsign.Handle) UpdateName(manager.Ships[0]);
        //  else if (item.Handle == Som.ShipOC.Heading.Handle) UpdateHeading(manager.Ships[0]);
        //  else if (item.Handle == Som.ShipOC.Position.Handle) UpdatePosition(manager.Ships[0]);
        //  else if (item.Handle == Som.ShipOC.Speed.Handle) UpdateSpeed(manager.Ships[0]);
        //}

        // We can update all attributes if we dont want to check every attribute.
        UpdateAll(manager.ShipObjects[0]);
        //UpdateName(manager.Ships[0]);
        //UpdatePosition(manager.Ships[0]);
        //UpdateHeading(manager.Ships[0]);
        //UpdateSpeed(manager.Ships[0]);
      }
      #endregion //User Code
    }
    // Reflect Object Attributes
    public override void FdAmb_ObjectAttributesReflectedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ObjectAttributesReflectedHandler(sender, data);

      #region User Code
      foreach (var item in manager.StationObjects)
      {
        // Update station
        if (data.ObjectInstance.Handle == item.Handle)
        {
          // Get parameter values - 2nd method
          foreach (var pair in data.ObjectInstance.Attributes)
          {
            if (pair.Handle == Som.StationOC.StationName.Handle) item.StationName = pair.GetValue<string>();
            else if (pair.Handle == Som.StationOC.Location.Handle) item.Location = pair.GetValue<LocationEnum>();
          }
        }
      }
      foreach (var item in manager.ShipObjects)
      {
        // Update ship
        if (data.ObjectInstance.Handle == item.Handle)
        {
          // Get parameter values - 1st method
          // First check whether  the attr is updated or not. Result returns 0/null if the updated attribute set does not contain the attr and its value 
          if (data.IsValueUpdated(Som.ShipOC.Callsign))
            item.Ship.Callsign = data.GetAttributeValue<string>(Som.ShipOC.Callsign);
          if (data.IsValueUpdated(Som.ShipOC.Heading))
            item.Ship.Heading = (LocationEnum)data.GetAttributeValue<uint>(Som.ShipOC.Heading);
          if (data.IsValueUpdated(Som.ShipOC.Position))
            item.Ship.Position = data.GetAttributeValue<PositionType>(Som.ShipOC.Position);
          if (data.IsValueUpdated(Som.ShipOC.Speed))
            item.Ship.Speed = (SpeedEnum)data.GetAttributeValue<uint>(Som.ShipOC.Speed);

          // Get parameter values - 2nd method
          //foreach (var pair in data.ObjectInstance.Attributes)
          //{
          //  if (pair.Handle == Som.ShipOC.Callsign.Handle) item.Local.Callsign = pair.GetValue<string>();
          //  else if (pair.Handle == Som.ShipOC.Position.Handle) item.Local.Position = pair.GetValue<PositionType>();
          //}

          // report to the user
          //Report($"Callsign: {item.Local.Callsign}, Heading: {item.Local.Heading.ToString()}, Position: ({item.Local.Position.X}, {item.Local.Position.Y}), Speed: {item.Local.Speed.ToString()}" + Environment.NewLine);
        }
      }
      #endregion //User Code
    }
    // An Object is Removed
    public override void FdAmb_ObjectRemovedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ObjectRemovedHandler(sender, data);

      #region User Code
      // Lock while taking a snapshot - to avoid foreach loop enumeration exception
      object[] snap;
      lock (thisLock)
      {
        snap = manager.ShipObjects.ToArray();
      }
      foreach (CShipHlaObject ship in snap)
      {
        if (data.ObjectInstance.Handle == ship.Handle)// Find the Object
        {
          manager.ShipObjects.Remove(ship);
          Report($"Ship: {ship.Ship.Callsign} left. Number of Ships Now: {manager.ShipObjects.Count}"+ Environment.NewLine);
        }
      }
      #endregion //User Code
    }
    public override void FdAmb_TurnUpdatesOnForObjectInstanceAdvisedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnUpdatesOnForObjectInstanceAdvisedHandler(sender, data);

      #region User Code
      // Start to update the position periodically
      manager.timer.Start(); // OpenRti does not support this callback
      #endregion //User Code
    }
    public override void FdAmb_TurnUpdatesOffForObjectInstanceAdvisedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnUpdatesOffForObjectInstanceAdvisedHandler(sender, data);

      #region User Code
      // Stop to update the position
      manager.timer.Stop();
      #endregion //User Code
    }
    #endregion //Object Management Callbacks

    #region Time Management Callbacks
    // FdAmb_TimeRegulationEnabled
    public override void FdAmb_TimeRegulationEnabled(object sender, HlaTimeManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TimeRegulationEnabled(sender, data);

      #region User Code
      Time = data.Time; //  Current logical time of the joined federate set by RTI
      Report("Logical time set by RTI TR: " + Time);
      #endregion //User Code
    }
    // FdAmb_TimeConstrainedEnabled
    public override void FdAmb_TimeConstrainedEnabled(object sender, HlaTimeManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TimeConstrainedEnabled(sender, data);

      #region User Code
      Time = data.Time; //  Current logical time of the joined federate set by RTI
      Report("Logical time set by RTI TC: " + Time);
      #endregion //User Code
    }
    // FdAmb_TimeAdvanceGrant
    public override void FdAmb_TimeAdvanceGrant(object sender, HlaTimeManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TimeAdvanceGrant(sender, data);

      #region User Code
      Time = data.Time; //  Current logical time of the joined federate set by RTI
      Report("Logical time set by RTI: " + Time);
      #endregion //User Code
    }
    // FdAmb_RequestRetraction
    public override void FdAmb_RequestRetraction(object sender, HlaTimeManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_RequestRetraction(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_RequestRetraction");
      #endregion //User Code
    }
    #endregion //Time Management Callbacks


    private void RegisterObject(HlaObject obj)
    {
      RegisterHlaObject(obj);

      //// DDM - register object with regions
      //// Create a list of attribute set and region set pairs
      //AttributeHandleSetRegionHandleSetPairVector pairs = new AttributeHandleSetRegionHandleSetPairVector();
      //// Construct the region set
      //List<HlaRegion> regions = new List<HlaRegion>();
      //regions.Add(aor1);
      //// Populate the pairs. Here we use all the attributes of the object
      //pairs.Pairs.Add(new KeyValuePair<List<HlaAttribute>, List<HlaRegion>>(obj.Attributes, regions));
      //// register object attributes with related regions
      //RegisterHlaObject(obj, pairs);
      //associateRegionsForUpdates(obj, pairs);
    }
    // Create AOR-1 (West)
    public void CreateWestRegion()
    {
      // Get all dimension handles associated with the region
      GetAllDimensionHandles();
      // Instantiate a region object
      aor1 = new HlaRegion("aor1");
      // Create a set of dimensions related with this region
      List<HlaDimension> dimensions = new List<HlaDimension>();
      dimensions.Add(Som.AreaOfResponsibility);
      // Create region
      CreateRegion(aor1, dimensions);
      // Set range lower and upper bounds for each dimension
      SetRangeBounds(aor1.Handle, Som.AreaOfResponsibility.Handle, 0, 0);
      // create a set of regions
      List<HlaRegion> regions = new List<HlaRegion>();
      regions.Add(aor1);
      // Commit region modifications
      CommitRegionModifications(regions);
    }
    // Create AOR-2 (East)
    public void CreateEastRegion()
    {
      // Get all dimension handles associated with the region
      GetAllDimensionHandles();
      // Instantiate a region object
      aor2 = new HlaRegion("aor2");
      // Create a set of dimensions related with this region
      List<HlaDimension> dimensions = new List<HlaDimension>();
      dimensions.Add(Som.AreaOfResponsibility);
      // Create region
      CreateRegion(aor2, dimensions);
      // Set range lower and upper bounds for each dimension
      SetRangeBounds(aor2.Handle, Som.AreaOfResponsibility.Handle, 1, 2);
      // create a set of regions
      List<HlaRegion> regions = new List<HlaRegion>();
      regions.Add(aor2);
      // Commit region modifications
      CommitRegionModifications(regions);
    }

    // Send radio message
    public bool SendMessage(string txt)
    {
      HlaInteraction interaction = new Racon.RtiLayer.HlaInteraction(Som.RadioMessageIC, "RadioMessage");

      // Add Values
      interaction.AddParameterValue(Som.RadioMessageIC.Callsign, manager.ShipObjects[0].Ship.Callsign); // String
      interaction.AddParameterValue(Som.RadioMessageIC.Message, txt); // String
      interaction.AddParameterValue(Som.RadioMessageIC.TimeStamp, DateTime.Now); // DateTime

      //interaction.AddParameterValue(Som.RadioMessageIC.TimeStamp, 1); // int, long
      //interaction.AddParameterValue<double>(Som.RadioMessageIC.TimeStamp, 5.5); // double
      //interaction.AddParameterValue(Som.RadioMessageIC.TimeStamp, 1.6f); // float
      //interaction.AddParameterValue<uint>(Som.RadioMessageIC.TimeStamp, 1); // uint
      //interaction.AddParameterValue(this.ChatIC.TimeStamp, (uint)StatusTypes.READY);// Enums

      // Send interaction
      return (SendInteraction(interaction));
    }
    // Update attribute values
    private void UpdateAll(CShipHlaObject ship)
    {
      // Add Values
      ship.AddAttributeValue(Som.ShipOC.Callsign, ship.Ship.Callsign);
      ship.AddAttributeValue(Som.ShipOC.Heading, (uint)ship.Ship.Heading);
      ship.AddAttributeValue<PositionType>(Som.ShipOC.Position, ship.Ship.Position);
      ship.AddAttributeValue(Som.ShipOC.Speed, (uint)ship.Ship.Speed);
      UpdateAttributeValues(ship);
    }
    private void UpdateName(CShipHlaObject ship)
    {
      // Add Values
      ship.AddAttributeValue(Som.ShipOC.Callsign, ship.Ship.Callsign);
      UpdateAttributeValues(ship);
    }
    private void UpdateHeading(CShipHlaObject ship)
    {
      // Add Values
      ship.AddAttributeValue(Som.ShipOC.Heading, (uint)ship.Ship.Heading);
      UpdateAttributeValues(ship);

      // Update attribute using a logical timestamp
      //EventRetractionHandle handle = UpdateAttributeValues(user, 3.14);
    }
    public void UpdatePosition(CShipHlaObject ship)
    {
      // Add Values
      ship.AddAttributeValue<PositionType>(Som.ShipOC.Position, ship.Ship.Position);
      UpdateAttributeValues(ship);
    }
    private void UpdateSpeed(CShipHlaObject ship)
    {
      // Add Values
      ship.AddAttributeValue(Som.ShipOC.Speed, (uint)ship.Ship.Speed);
      UpdateAttributeValues(ship);
    }
    // report
    private void Report(string txt)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(txt);
    }

    #endregion //Manually Added Code
  }
}

/*
// Send Interaction
HlaInteraction interaction = new Racon.RtiLayer.HlaInteraction(StartScenarioIC, "StartScenario");

// Add Values
interaction.AddParameterValue(StartScenarioIC.ScenarioFile, "EmergencyEvacuationScenario.xml"); // String
interaction.AddParameterValue(StartScenarioIC.Role, (uint)Roles.ChiefWarden);// Enums

// Send interaction
SendInteraction(interaction);
*/

/*
// Receive Interaction
public override void FdAmb_InteractionReceivedHandler(object sender, HlaInteractionEventArgs data)
{
  // Call the base class handler
  base.FdAmb_InteractionReceivedHandler(sender, data);

  #region User Code
  // Which interaction class?
  if (data.Interaction.ClassHandle == StartScenarioIC.Handle)
  {
    string scenarioFile = "";
    Roles role;

    // Get parameter values
    if (data.IsValueUpdated(StartScenarioIC.ScenarioFile))
      scenarioFile = data.GetParameterValue<string>(StartScenarioIC.ScenarioFile);
    if (data.IsValueUpdated(StartScenarioIC.Role))
      role = (Roles)data.GetParameterValue<uint>(StartScenarioIC.Role);
  }
  #endregion
}
*/
