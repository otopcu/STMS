// **************************************************************************************************
//		CCommunicationSpace
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
/// This class is extended from the routing space model of RACoN API
/// </summary>

using System;
// Racon
using Racon;
using Racon.RtiLayer;


namespace stms.Som
{
  public class CCommunicationSpace : Racon.ObjectModel.CRoutingSpace
  {
    #region Declarations
    // Dimensions
    private Racon.ObjectModel.CDimension RadioChannel;
    // Regions - Manually declare regions such as:
    // public HlaRegion Oda1Region;
    // Region Extents - Manually declare region extents such as:
    // public private HlaExtent exOd1;
    #endregion //Declarations

    #region Constructor
    public CCommunicationSpace() : base("Communication")
    {
      // Create Dimensions
      RadioChannel = new Racon.ObjectModel.CDimension("RadioChannel", this);
      Dimensions.Add(RadioChannel);
    }
    #endregion //Constructor
  }
}
