// **************************************************************************************************
//		FederateSom
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
  public class FederateSom : Racon.ObjectModel.CObjectModel
  {
    #region Declarations
    #region SOM Declaration
    public stms.Som.CShipOC ShipOC;
    public stms.Som.CStationOC StationOC;
    public stms.Som.CTrackOC TrackOC;
    public stms.Som.CRadioMessageIC RadioMessageIC;
    public HlaDimension AreaOfResponsibility;
    // HLA13
    public stms.Som.CCommunicationSpace CommunicationSpace;
    public stms.Som.CAreaSpace AreaSpace;
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
      TrackOC = new stms.Som.CTrackOC();
      AddToObjectModel(TrackOC);
      RadioMessageIC = new stms.Som.CRadioMessageIC();
      AddToObjectModel(RadioMessageIC);
      // Add Dimensions
      AreaOfResponsibility = new HlaDimension("AreaOfResponsibility");
      AddToObjectModel(AreaOfResponsibility);
      // HLA13
      CommunicationSpace = new stms.Som.CCommunicationSpace();
      AddToObjectModel(CommunicationSpace);
      AreaSpace = new stms.Som.CAreaSpace();
      AddToObjectModel(AreaSpace);
    }
    #endregion //Constructor
  }
}
