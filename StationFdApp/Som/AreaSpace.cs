// **************************************************************************************************
//		CAreaSpace
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
/// This class is extended from the routing space model of RACoN API
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
  public class CAreaSpace : Racon.ObjectModel.CRoutingSpace
  {
    #region Declarations
    // Dimensions
    private Racon.ObjectModel.CDimension AreaOfResponsibility;
    // Regions - Manually declare regions such as:
    // public HlaRegion Oda1Region;
    // Region Extents - Manually declare region extents such as:
    // public private HlaExtent exOd1;
    #endregion //Declarations
    
    #region Constructor
    public CAreaSpace() : base("Area")
    {
      // Create Dimensions
      AreaOfResponsibility = new Racon.ObjectModel.CDimension("AreaOfResponsibility", this);
      Dimensions.Add(AreaOfResponsibility);
    }
    #endregion //Constructor
  }
}
