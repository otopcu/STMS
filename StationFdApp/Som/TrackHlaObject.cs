// **************************************************************************************************
//		CTrackHlaObject
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
/// This is a wrapper class for local data structures. This class is extended from the object model of RACoN API
/// </summary>

// System
using System;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;


namespace stms.Som
{
  public enum OutOfZoneEnum
  {
    OutOfStrait,
    OutOfAor
  };

  public class COutofZoneArgs : EventArgs
  {
    public OutOfZoneEnum Zone;
    public CTrackHlaObject Track;
    public COutofZoneArgs(OutOfZoneEnum zone, CTrackHlaObject track)
    {
      Zone = zone;
      Track = track;
    }
  };

  public class CTrackHlaObject : HlaObject
  {
    #region Declarations
    public uint TrackNo; // = ship object handle
    public string TrackName; // = ship callsign
    private PositionType _TrackPosition;
    public PositionType TrackPosition
    {
      get { return _TrackPosition; }
      set
      {
        _TrackPosition = value;
      }
    }
    public LocationEnum TrackHeading;
    public SpeedEnum TrackSpeed;
    #endregion //Declarations

    // Events
    public event EventHandler<COutofZoneArgs> OutOfZone;
    // Event Raisers
    protected virtual void OnOutOfZone(COutofZoneArgs args)
    {
      OutOfZone?.Invoke(this, args);// Raise the event.
    }

    #region Constructor
    public CTrackHlaObject(HlaObjectClass _type) : base(_type)
    {
      // TODO: Instantiate local data here
      TrackPosition = new PositionType { X = 10, Y = 0 };
      //TrackZone = zone;
      //if (TrackZone == LocationEnum.West)
      //  TrackPosition = new PositionType { X = 10, Y = 0 };
      //else
      //  TrackPosition = new PositionType { X = 10, Y = 0 };
    }
    // Copy constructor - used in callbacks
    public CTrackHlaObject(HlaObject _obj) : base(_obj)
    {
      // TODO: Instantiate local data here
      // var Data = new Your_LocalData_Type();
    }
    #endregion //Constructor

    public void ProcessTrack(LocationEnum stationLocation)
    {
      switch (stationLocation)
      {
        case LocationEnum.West:
          if (TrackPosition.X < -10)
            OnOutOfZone(new COutofZoneArgs(OutOfZoneEnum.OutOfStrait, this));
          else if (TrackPosition.X > 0)
            OnOutOfZone(new COutofZoneArgs(OutOfZoneEnum.OutOfAor, this));
          break;
        case LocationEnum.East:
          if (TrackPosition.X > 10)
            OnOutOfZone(new COutofZoneArgs(OutOfZoneEnum.OutOfStrait, this));
          else if (TrackPosition.X < 0)
            OnOutOfZone(new COutofZoneArgs(OutOfZoneEnum.OutOfAor, this));
          break;
      }
    }
  }
}
