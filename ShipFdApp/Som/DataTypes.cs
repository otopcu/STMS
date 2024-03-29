﻿// **************************************************************************************************
//		Data Types
//
//		generated
//			by		: 	Simulation Generator (SimGe) v.0.2.9
//			at		: 	Wednesday, November 23, 2016 10:29:55 AM
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
  //using HLAASCIIchar = System.Char;

  #region Enumerated Dataypes
  // Location Type
  public enum LocationEnum { West = 0, East = 1 };

  // Speed enumeration {very slow, slow, fast, very fast}. It indicates the speed category of a ship.
  public enum SpeedEnum { VerySlow = 0, Slow = 1, Fast = 2, VeryFast = 3 };

  // It enumerates areas of responsibility
  public enum AreaEnum { aor1 = 0, aor2 = 1 };

  // It enumerates the radio channels
  public enum RadioChannelEnum { Channel1 = 0, Channel2 = 1 };

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

  //// Chapter 5 Exercise 1
  //public struct FacultyType
  //{
  //  public FacultyPositionEnum FacultyStatus;
  //  public int LectureHours; // only valid when FacultyStatus = Instructor
  //  public ExperienceLevel Level; // only valid when FacultyStatus = VisitingFaculty
  //  public TitleRateScale TitleRate; // only valid when FacultyStatus includes Professor
  //}

  #endregion
}
