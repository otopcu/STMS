// **************************************************************************************************
//		Data Types
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
/// This file includes the enumerated data types.
/// </summary>

namespace stms.Som
{
  #region Enumerated Dataypes
    // Location Type
  public enum LocationEnum { West, East };
  
    // Speed enumeration {very slow, slow, fast, very fast}. It indicates the speed category of a ship.
  public enum SpeedEnum { VerySlow, Slow, Fast, VeryFast };
  
    // It enumerates areas of responsibility
  public enum AreaEnum { aor1, aor2 };
  
    // It enumerates the radio channels
  public enum RadioChannelEnum { Channel1, Channel2 };
  
  #endregion
  #region Fixed Record Dataypes
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
