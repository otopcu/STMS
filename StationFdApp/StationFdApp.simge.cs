// **************************************************************************************************
//		CStationFdApp
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
/// The application specific federate that is extended from the Generic Federate Class of RACoN API
/// </summary>

// System
using System;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
using stms.Som;

namespace stms
{
  public partial class CStationFdApp : Racon.CGenericFederate
  {
    #region Declarations
    public stms.Som.FederateSom Som;
    #endregion //Declarations

    #region Constructor
    public CStationFdApp() : base(RTILibraryType.HLA1516e_OpenRti)
    {
      // Create and Attach Som to federate
      Som = new stms.Som.FederateSom();
      SetSom(Som);
    }
    #endregion //Constructor

    #region Event Handlers
    #region Federate Callback Event Handlers
    #region Federation Management Callbacks
    // FdAmb_ConnectionLost
    public override void FdAmb_ConnectionLost(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_ConnectionLost(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_ConnectionLost");
      #endregion //User Code
    }
    // FdAmb_InitiateFederateSaveHandler
    public override void FdAmb_InitiateFederateSaveHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_InitiateFederateSaveHandler(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_InitiateFederateSaveHandler");
      #endregion //User Code
    }
    // FdAmb_InitiateFederateRestoreHandler
    public override void FdAmb_InitiateFederateRestoreHandler(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_InitiateFederateRestoreHandler(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_InitiateFederateRestoreHandler");
      #endregion //User Code
    }

    // FdAmb_FederationRestored
    public override void FdAmb_FederationRestored(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_FederationRestored(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_FederationRestored");
      #endregion //User Code
    }
    // FdAmb_FederationRestoreBegun
    public override void FdAmb_FederationRestoreBegun(object sender, HlaFederationManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_FederationRestoreBegun(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_FederationRestoreBegun");
      #endregion //User Code
    }
    #endregion //Federation Management Callbacks
    #region Declaration Management Callbacks
    #endregion //Declaration Management Callbacks
    #region Object Management Callbacks
    #endregion //Object Management Callbacks
    #region Ownership Management Callbacks
    // FdAmb_AttributeOwnershipAcquisitionCancellationConfirmed
    public override void FdAmb_AttributeOwnershipAcquisitionCancellationConfirmed(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipAcquisitionCancellationConfirmed(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipAcquisitionCancellationConfirmed");
      #endregion //User Code
    }
    // FdAmb_AttributeOwnershipUnavailable
    public override void FdAmb_AttributeOwnershipUnavailable(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipUnavailable(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipUnavailable");
      #endregion //User Code
    }
    // FdAmb_AttributeOwnershipDivestitureNotified
    public override void FdAmb_AttributeOwnershipDivestitureNotified(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipDivestitureNotified(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipDivestitureNotified");
      #endregion //User Code
    }
    // FdAmb_AttributeOwnershipAcquisitionNotified
    public override void FdAmb_AttributeOwnershipAcquisitionNotified(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipAcquisitionNotified(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipAcquisitionNotified");
      #endregion //User Code
    }
    // FdAmb_AttributeOwnershipInformed
    public override void FdAmb_AttributeOwnershipInformed(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipInformed(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipInformed");
      #endregion //User Code
    }
    // FdAmb_AttributeOwnershipReleaseRequestedHandler
    public override void FdAmb_AttributeOwnershipReleaseRequestedHandler(object sender, HlaOwnershipManagementEventArgs data)
    {
      // Call the base class handler
      base.FdAmb_AttributeOwnershipReleaseRequestedHandler(sender, data);

      #region User Code
      throw new NotImplementedException("FdAmb_AttributeOwnershipReleaseRequestedHandler");
      #endregion //User Code
    }
    #endregion //Ownership Management Callbacks    
    #endregion //Federate Callback Event Handlers
    #endregion //Event Handlers
  }
}
