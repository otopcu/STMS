// **************************************************************************************************
//		CTrackOC
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
  public class CTrackOC : HlaObjectClass
  {
    #region Declarations
    public HlaAttribute TrackNumber;
    public HlaAttribute TrackPosition;
    public HlaAttribute TrackHeading;
    public HlaAttribute TrackSpeed;
    #endregion //Declarations
    
    #region Constructor
    public CTrackOC() : base()
    {
      // Initialize Class Properties
      Name = "HLAobjectRoot.Track";
      ClassPS = PSKind.PublishSubscribe;
      
      // Create Attributes
      // TrackNumber
      TrackNumber = new HlaAttribute("TrackNumber", PSKind.PublishSubscribe);
      Attributes.Add(TrackNumber);
      // TrackPosition
      TrackPosition = new HlaAttribute("TrackPosition", PSKind.PublishSubscribe);
      Attributes.Add(TrackPosition);
      // TrackHeading
      TrackHeading = new HlaAttribute("TrackHeading", PSKind.PublishSubscribe);
      Attributes.Add(TrackHeading);
      // TrackSpeed
      TrackSpeed = new HlaAttribute("TrackSpeed", PSKind.PublishSubscribe);
      Attributes.Add(TrackSpeed);
    }
    #endregion //Constructor
  }
}
