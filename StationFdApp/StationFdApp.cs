// **************************************************************************************************
//		CStationFdApp
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.9
//			at		: 	Friday, December 16, 2016 8:10:19 PM
//		compatible with		: 	RACoN v.0.0.2.1
//
//		copyright		: 	(C) 2016 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// The application specific federate that is extended from the Generic Federate Class of RACoN API. This file is intended for manual code operations.
/// </summary>

// System
using System;
using System.Linq;
using System.Collections.Generic;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  public partial class CStationFdApp : Racon.CGenericFederate
  {
    #region Manually Added Code
    // Local Data
    private object thisLock = new object();

    // DDM Declarations
    public HlaRegion aor1;
    public HlaRegion aor2;

    #region Constructor
    #endregion //Constructor

    #region Event Handlers- Overrides
    #region Federation Management Callbacks
    // FdAmb_OnSynchronizationPointRegistrationConfirmedHandler
    public override void FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_OnSynchronizationPointRegistrationConfirmedHandler(sender, data);

      #region User Code
      Report($"Pacing request ({data.Label}) is accepted by RTI." + Environment.NewLine);
      #endregion //User Code
    }
    // FdAmb_OnSynchronizationPointRegistrationFailedHandler
    public override void FdAmb_OnSynchronizationPointRegistrationFailedHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_OnSynchronizationPointRegistrationFailedHandler(sender, data);

      #region User Code
      Report($"Pacing request ({data.Label}) is NOT accepted by RTI. Reason: {data.Reason}" + Environment.NewLine);
      #endregion //User Code
    }
    // FdAmb_SynchronizationPointAnnounced
    public override void FdAmb_SynchronizationPointAnnounced(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_SynchronizationPointAnnounced(sender, data);

      #region User Code
      //Report.WriteLine($"Ready for zone transfer. Label: {data.Label}" + Environment.NewLine);
      SynchronizationPointAchieved(data.Label, true);
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
    // FdAmb_FederationSaved
    public override void FdAmb_FederationSaved(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_FederationSaved(sender, data);

      if (data.Success)
      {
        // federation saved
      }
      else
      {
        // federation not saved. The save failure reason is
        string information = data.Reason;
        Report("The save failure reason: " + information);
      }
    }
    // FdAmb_ConfirmFederationRestorationRequestHandler
    public override void FdAmb_ConfirmFederationRestorationRequestHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ConfirmFederationRestorationRequestHandler(sender, data);

      if (data.Success)
      {
        // restoration request is confirmed
      }
      else
      {
        // restoration request is failed. This callback does not provide a reason.
      }
    }

    #endregion
    #region Declaration Management Callbacks
    // Turn Interactions Off
    public override void FdAmb_TurnInteractionsOffAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnInteractionsOffAdvisedHandler(sender, data);

      #region User Code
      Program.IsRadioOn = false;
      #endregion //User Code
    }
    // Turn Interactions On
    public override void FdAmb_TurnInteractionsOnAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnInteractionsOnAdvisedHandler(sender, data);

      #region User Code
      Program.IsRadioOn = true;
      #endregion //User Code
    }
    // Start Registration
    public override void FdAmb_StartRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_StartRegistrationForObjectClassAdvisedHandler(sender, data);

      #region User Code
      // Check that this is for the StationOC
      if (data.ObjectClassHandle == Som.StationOC.Handle)
        RegisterHlaObject(Program.StationObjects[0]);
      #endregion //User Code
    }

    // Stop Registration
    public override void FdAmb_StopRegistrationForObjectClassAdvisedHandler(object sender, HlaDeclarationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_StopRegistrationForObjectClassAdvisedHandler(sender, data);

      #region User Code
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
        Report($"RadioMessage: From: {sentBy} > {msg} at {ts}" + Environment.NewLine);
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
        newShip.Type = Som.ShipOC;
        Program.ShipObjects.Add(newShip);
        // Create a new track
        CTrackHlaObject track = new CTrackHlaObject(Som.TrackOC);
        track.TrackNo = data.ObjectInstance.Handle;
        Program.MyTracks.Add(track);
        // hook to track event
        track.OutOfZone += HandleTrackOutOfZone;
        // Request Update Values of Attributes        
        RequestAttributeValueUpdate(newShip, null);
        // register the track object
        RegisterHlaObject(track);
      }
      else if (data.ClassHandle == Som.StationOC.Handle) // A station
      {
        // Create and add a new ship to the list
        CStationHlaObject newStation = new CStationHlaObject(data.ObjectInstance);
        newStation.Type = Som.StationOC;
        Program.StationObjects.Add(newStation);

        // Request Update Values of Attributes        
        // (1) Request update values of all attributes for a specific object instance
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
      if (data.ObjectInstance.Handle == Program.StationObjects[0].Handle)
      {
        // We can update all attributes if we dont want to check every attribute.
        var timestamp = Time + Lookahead;
        UpdateStationAttributes(Program.StationObjects[0], timestamp);
      }
      #endregion //User Code
    }
    // Reflect Object Attributes
    public override void FdAmb_ObjectAttributesReflectedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ObjectAttributesReflectedHandler(sender, data);

      #region User Code
      foreach (var item in Program.StationObjects)
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
      foreach (var item in Program.MyTracks)
      {
        // Update track
        if (data.ObjectInstance.Handle == item.TrackNo)
        {
          // Get parameter values - 1st method
          // First check whether  the attr is updated or not. Result returns 0/null if the updated attribute set does not contain the attr and its value 
          if (data.IsValueUpdated(Som.ShipOC.Callsign))
            item.TrackName = data.GetAttributeValue<string>(Som.ShipOC.Callsign);
          if (data.IsValueUpdated(Som.ShipOC.Position))
            item.TrackPosition = data.GetAttributeValue<PositionType>(Som.ShipOC.Position);
          if (data.IsValueUpdated(Som.ShipOC.Heading))
            item.TrackHeading = data.GetAttributeValue<LocationEnum>(Som.ShipOC.Heading);
          if (data.IsValueUpdated(Som.ShipOC.Speed))
            item.TrackSpeed = data.GetAttributeValue<SpeedEnum>(Som.ShipOC.Speed);
        }
      }
      foreach (var item in Program.OtherTracks)
      {
        // Find the Object
        if (data.ObjectInstance.Handle == item.Handle)
        {
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
      object[] stations, otracks, mytracks;
      lock (thisLock)
      {
        stations = Program.StationObjects.ToArray();
        mytracks = Program.MyTracks.ToArray();
        otracks = Program.OtherTracks.ToArray();
      }
      foreach (CStationHlaObject station in stations)
      {
        if (data.ObjectInstance.Handle == station.Handle)// Find the Object
        {
          Program.StationObjects.Remove(station);
          Report($"Station: {station.StationName} left." + Environment.NewLine);
        }
      }
      foreach (CTrackHlaObject track in mytracks)
      {
        if (data.ObjectInstance.Handle == track.TrackNo)// Find the Object
        {
          Program.MyTracks.Remove(track);
          Report($"Ship: {track.TrackName} left." + Environment.NewLine);
        }
      }
      foreach (CTrackHlaObject track in otracks)
      {
        if (data.ObjectInstance.Handle == track.Handle)// Find the Object
        {
          Program.OtherTracks.Remove(track);
          Report($"Track: {track.TrackName} deleted." + Environment.NewLine);
        }
      }
      #endregion //User Code
    }
    public override void FdAmb_TurnUpdatesOnForObjectInstanceAdvisedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnUpdatesOnForObjectInstanceAdvisedHandler(sender, data);

      #region User Code
      #endregion //User Code
    }
    public override void FdAmb_TurnUpdatesOffForObjectInstanceAdvisedHandler(object sender, HlaObjectEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_TurnUpdatesOffForObjectInstanceAdvisedHandler(sender, data);

      #region User Code
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
      Report("Logical time is set by RTI. TR: " + Time);
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
    #region Ownership Management Callbacks
    // FdAmb_AttributeOwnershipAssumptionRequested
    public override void FdAmb_AttributeOwnershipAssumptionRequested(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipAssumptionRequested(sender, data);

      #region User Code
      CTrackHlaObject track = new CTrackHlaObject(Som.TrackOC);
      track.Handle = data.ObjectHandle;
      AttributeOwnershipAcquisition(track, "DivestitureRequestTag");
      #endregion //User Code
    }
    #endregion //Ownership Management Callbacks    
    #endregion

    #region Methods
    // Send radio message
    public bool SendMessage(string txt, double timestamp)
    {
      HlaInteraction interaction = new Racon.RtiLayer.HlaInteraction(Som.RadioMessageIC, "RadioMessage");

      // Add Values
      interaction.AddParameterValue(Som.RadioMessageIC.Callsign, Program.StationObjects[0].StationName); // String
      interaction.AddParameterValue(Som.RadioMessageIC.Message, txt); // String
      interaction.AddParameterValue(Som.RadioMessageIC.TimeStamp, DateTime.Now); // DateTime

      // Send interaction with timestamp
      try
      {
        //SendInteraction(interaction);
        MessageRetraction handle = SendInteraction(interaction, timestamp);
        Retract(handle);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
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
      SetRangeBounds(aor2.Handle, Som.AreaOfResponsibility.Handle, 1, 1);
      // create a set of regions
      List<HlaRegion> regions = new List<HlaRegion>();
      regions.Add(aor2);
      // Commit region modifications
      CommitRegionModifications(regions);
    }
    // Update attribute values
    private void UpdateStationAttributes(CStationHlaObject station, double timestamp)
    {
      // Add Values
      station.AddAttributeValue(Som.StationOC.StationName, station.StationName);
      station.AddAttributeValue(Som.StationOC.Location, (uint)station.Location);
      UpdateAttributeValues(station, timestamp);
    }
    // report
    private void Report(string txt)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(txt);
    }
    // Handle when track is ou of zone
    private void HandleTrackOutOfZone(object sender, COutofZoneArgs data)
    {
      // When a ship crosses over to another zone
      // Select track that is on spot
      List<HlaAttribute> attributes = new List<HlaAttribute>();
      attributes.Add(Som.TrackOC.TrackNumber);
      attributes.Add(Som.TrackOC.TrackPosition);
      attributes.Add(Som.TrackOC.TrackHeading);
      attributes.Add(Som.TrackOC.TrackSpeed);
      if (data.Zone == OutOfZoneEnum.OutOfStrait)
      {
        DeleteObjectInstance(data.Track);
      }
      else
        UnconditionalAttributeOwnershipDivestiture(data.Track, attributes);
      // remove this track
      Program.MyTracks.Remove(data.Track);
    }
    #endregion

    #endregion //Manually Added Code
  }
}
