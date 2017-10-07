// **************************************************************************************************
//		FederateSom
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.3.0
//			at		: 	Saturday, October 7, 2017 12:27:52 PM
//		compatible with		: 	RACoN v.0.0.2.3
//
//		copyright		: 	(C) 2016-2017 Okan Topcu
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
    public HlaDimension RadioChannel;
    public stms.Som.CCommunicationSpace CommunicationSpace;// HLA13
    public stms.Som.CAreaSpace AreaSpace;// HLA13
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
      AreaOfResponsibility = new HlaDimension("AreaOfResponsibility");
      AddToObjectModel(AreaOfResponsibility);
      RadioChannel = new HlaDimension("RadioChannel");
      AddToObjectModel(RadioChannel);
      CommunicationSpace = new stms.Som.CCommunicationSpace();// HLA13
      AddToObjectModel(CommunicationSpace);
      AreaSpace = new stms.Som.CAreaSpace();// HLA13
      AddToObjectModel(AreaSpace);
    }
    #endregion //Constructor
  }
}
