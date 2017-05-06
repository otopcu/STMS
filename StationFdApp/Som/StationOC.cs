// **************************************************************************************************
//		CStationOC
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
/// This class is extended from the object model of RACoN API
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
  public class CStationOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute Location;
    public HlaAttribute StationName;
    #endregion //Declarations
    
    #region Constructor
    public CStationOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Station";
      ClassPS = PSKind.PublishSubscribe;
      
      // Create Attributes
      // Location
      Location = new HlaAttribute("Location", PSKind.PublishSubscribe);
      Attributes.Add(Location);
      // StationName
      StationName = new HlaAttribute("StationName", PSKind.PublishSubscribe);
      Attributes.Add(StationName);
    }
    #endregion //Constructor
  }
}
