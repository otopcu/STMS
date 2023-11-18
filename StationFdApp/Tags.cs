// **************************************************************************************************
//		Tags
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.3.1
//			at		: 	Friday, October 27, 2017 9:50:43 PM
//		compatible with		: 	RACoN v.0.0.2.5
//
//		copyright		: 	(C) 2016-2017 Okan Topcu
//		email			: 	otot.support@outlook.com
// **************************************************************************************************
/// <summary>
/// This file includes the user-defined tags.
/// </summary>
// System
using System;
using System.Collections.Generic;
// Racon
using Racon;
using Racon.RtiLayer;
// Application
// Application
using stms.Som;

namespace stms
{
  public static class Tags
  {
    // Counter for update reflection
    public static int updateReflectTag; // !!! provide default value
    // Reason for send/receive
    public static string sendReceiveTag; // !!! provide default value
    // Reason for deletion of the object instance
    public static string deleteRemoveTag; // !!! provide_default_value
    public const string divestitureRequestTag = "NA";
    public const string divestitureCompletionTag = "NA";
    public const string acquisitionRequestTag = "NA";
    // Counter for request update
    public static int requestUpdateTag; // !!! provide default value


  }
}
