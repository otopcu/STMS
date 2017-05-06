// **************************************************************************************************
//		CRadioMessageIC
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
  public class CRadioMessageIC : HlaInteractionClass
  {
    #region Declarations
    public HlaParameter TimeStamp;
    public HlaParameter Callsign;
    public HlaParameter Message;
    #endregion //Declarations

    #region Constructor
    public CRadioMessageIC() : base()
    {
      // Initialize Class Properties
      Name = "HLAinteractionRoot.RadioMessage";
      ClassPS = PSKind.PublishSubscribe;

      // Create Parameters
      // TimeStamp
      TimeStamp = new HlaParameter("TimeStamp");
      Parameters.Add(TimeStamp);
      // Callsign
      Callsign = new HlaParameter("Callsign");
      Parameters.Add(Callsign);
      // Message
      Message = new HlaParameter("Message");
      Parameters.Add(Message);
    }
    #endregion //Constructor
  }
}
