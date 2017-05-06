// **************************************************************************************************
//		CShipOC
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
  public class CShipOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute Speed;
    public HlaAttribute Heading;
    public HlaAttribute Position;
    public HlaAttribute Callsign;
    #endregion //Declarations
    
    #region Constructor
    public CShipOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Ship";
      ClassPS = PSKind.Subscribe;
      
      // Create Attributes
      // Speed
      Speed = new HlaAttribute("Speed", PSKind.Neither);
      Attributes.Add(Speed);
      // Heading
      Heading = new HlaAttribute("Heading", PSKind.Neither);
      Attributes.Add(Heading);
      // Position
      Position = new HlaAttribute("Position", PSKind.Subscribe);
      Attributes.Add(Position);
      // Callsign
      Callsign = new HlaAttribute("Callsign", PSKind.Subscribe);
      Attributes.Add(Callsign);
    }
    #endregion //Constructor
  }
}
