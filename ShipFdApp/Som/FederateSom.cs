// **************************************************************************************************
//		FederateSom
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.8
//			at		: 	Sunday, November 20, 2016 10:41:35 PM
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
  public class FederateSom : Racon.ObjectModel.CObjectModel
  {
    #region Declarations
    #region SOM Declaration
    public stms.Som.CShipOC ShipOC;
    public stms.Som.CStationOC StationOC;
    public HlaDimension AreaOfResponsibility;
    public stms.Som.CRadioMessageIC RadioMessageIC;
    public stms.Som.CCommunicationSpace CommunicationSpace;
    #endregion
    #endregion //Declarations

    #region Constructor
    public FederateSom() : base()
    {
      // Construct SOM
      ShipOC = new stms.Som.CShipOC();
      AddToObjectModel(ShipOC);
      StationOC = new stms.Som.CStationOC();
      AddToObjectModel(StationOC);
      RadioMessageIC = new stms.Som.CRadioMessageIC();
      AddToObjectModel(RadioMessageIC);
      // Add Dimensions
      AreaOfResponsibility = new HlaDimension("AreaOfResponsibility");
      AddToObjectModel(AreaOfResponsibility);
      CommunicationSpace = new stms.Som.CCommunicationSpace();
      AddToObjectModel(CommunicationSpace);
    }
    #endregion //Constructor
  }
}
