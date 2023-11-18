// **************************************************************************************************
//		Data Types
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.3.0
//			at		: 	Saturday, October 7, 2017 11:55:17 AM
//		compatible with		: 	RACoN v.0.0.2.3
//
//		copyright		: 	(C) 2016-2017 Okan Topcu
//		email			: 	otot.support@outlook.com
// ************************************************************************************************** 
/// <summary>
/// This file includes the enumerated data types.
/// </summary>

// System
using System;
using System.Collections.Generic;
// Racon
using Racon;
using Racon.RtiLayer;
namespace stms.Som
{
  #region Enumerated Datatypes
  // Location Type
  public enum LocationEnum { West = 0, East = 1 };

  // Speed enumeration {very slow, slow, fast, very fast}. It indicates the speed category of a ship.
  public enum SpeedEnum { VerySlow = 0, Slow = 1, Fast = 2, VeryFast = 3 };

  // It enumerates areas of responsibility
  public enum AreaEnum { aor1 = 0, aor2 = 1 };

  // It enumerates the radio channels
  public enum RadioChannelEnum { Channel1 = 0, Channel2 = 1 };

  #endregion
  #region Fixed Record Datatypes
  // Position X and Y
  public struct PositionType
  {
    public float X; // X value
    public float Y; // Y value
  }

  #endregion
  #region User-added Dataypes
  public enum PacingEnum { SlowPacing, FastPacing }; // represents sync points
  #endregion



}
