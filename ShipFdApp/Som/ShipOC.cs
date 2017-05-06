// **************************************************************************************************
//		CShipOC
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
  public class CShipOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute Callsign;
    public HlaAttribute Position;
    public HlaAttribute Heading;
    public HlaAttribute Speed;
    #endregion //Declarations

    #region Constructor
    public CShipOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Ship";
      ClassPS = PSKind.PublishSubscribe;

      // Create Attributes
      // Callsign
      Callsign = new HlaAttribute("Callsign", PSKind.PublishSubscribe);
      Attributes.Add(Callsign);
      // Position
      Position = new HlaAttribute("Position", PSKind.PublishSubscribe);
      Attributes.Add(Position);
      // Heading
      Heading = new HlaAttribute("Heading", PSKind.PublishSubscribe);
      Attributes.Add(Heading);
      // Speed
      Speed = new HlaAttribute("Speed", PSKind.PublishSubscribe);
      Attributes.Add(Speed);
    }
    #endregion //Constructor
  }
}
