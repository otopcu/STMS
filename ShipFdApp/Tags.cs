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

namespace stms
{
  public static class Tags
  {
    // Counter for update reflection
    public static int updateReflectTag = 0; // !!! provide default value
    // Reason for send/receive
    public static string sendReceiveTag = "sendReceiveTag"; // !!! provide default value
    // Reason for deletion of the object instance
    public static string deleteRemoveTag = "deleteRemoveTag"; // !!! provide default value
    public const string divestitureRequestTag = "NA";
    public const string divestitureCompletionTag = "NA";
    public const string acquisitionRequestTag = "NA";
    // Counter for request update
    public static int requestUpdateTag = 0; // !!! provide default value
  }
}
