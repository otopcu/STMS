// **************************************************************************************************
//		CStationOC
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.8
//			at		: 	Sunday, November 20, 2016 10:57:53 PM
//		compatible with		: 	RACoN v.0.0.2.1
//
//		copyright		: 	(C) 2016 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// This class is extended from the object model of RACoN API
/// </summary>

using System;
// Racon
using Racon;
using Racon.RtiLayer;


namespace stms.Som
{
  public class CStationOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute StationName;
    public HlaAttribute Location;
    #endregion //Declarations

    #region Constructor
    public CStationOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Station";
      ClassPS = PSKind.Subscribe;

      // Create Attributes
      // StationName
      StationName = new HlaAttribute("StationName", PSKind.Subscribe);
      Attributes.Add(StationName);
      // Location
      Location = new HlaAttribute("Location", PSKind.Subscribe);
      Attributes.Add(Location);
    }
    #endregion //Constructor
  }
}
