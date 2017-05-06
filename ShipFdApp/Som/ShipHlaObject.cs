// **************************************************************************************************
//		CShipHlaObject
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.9
//			at		: 	Saturday, November 26, 2016 6:07:04 PM
//		compatible with		: 	RACoN v.0.0.2.1
//
//		copyright		: 	(C) 2016 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// This is a wrapper class for local data structures. This class is extended from the object model of RACoN API
/// </summary>

using System;
// Racon
using Racon;
using Racon.RtiLayer;

namespace stms.Som
{
  // Encapsulated Object for HLA
  public class CShipHlaObject : HlaObject
  {
    #region Declarations
    // TODO: Declare your local object structure here
     public CShip Ship;
    #endregion //Declarations


    #region Constructor
    public CShipHlaObject(HlaObjectClass _type) : base(_type)
    {
      // TODO: Instantiate local data here
      Ship = new CShip();
    }
    // Copy constructor - used in callbacks
    public CShipHlaObject(HlaObject _obj) : base(_obj)
    {
      // TODO: Instantiate local data here
      Ship = new CShip();
    }
    #endregion //Constructor

  }
}
