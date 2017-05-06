// **************************************************************************************************
//		CRadioMessageIC
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
  public class CRadioMessageIC : HlaInteractionClass
  {
    #region Declarations
    public HlaParameter Message;
    public HlaParameter Callsign;
    public HlaParameter TimeStamp;
    #endregion //Declarations
    
    #region Constructor
    public CRadioMessageIC() : base()
    {
      // Initialize Class Properties
      Name = "HLAinteractionRoot.RadioMessage";
      ClassPS = PSKind.PublishSubscribe;
      
      // Create Parameters
      // Message
      Message = new HlaParameter("Message");
      Parameters.Add(Message);
      // Callsign
      Callsign = new HlaParameter("Callsign");
      Parameters.Add(Callsign);
      // TimeStamp
      TimeStamp = new HlaParameter("TimeStamp");
      Parameters.Add(TimeStamp);
    }
    #endregion //Constructor
  }
}
