using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Racon
using Racon;
using Racon.RtiLayer;
// App
using stms.Som;

namespace stms
{
  // Local Domain Object
  public class CShip
  {
    public string Callsign;
    public LocationEnum Heading;
    public PositionType Position;
    public SpeedEnum Speed;
    public bool Exit = false; // is ship exited the strait?
    public LocationEnum Location;
    //public bool IsZoneTransferRequested = false; // Switching between area of responsibilities
    //public bool RequestTransfer = false;

    public CShip()
    {
      Callsign = "";
      Position = new PositionType { X = 10, Y = 0 };
      Heading = LocationEnum.West;
      Speed = SpeedEnum.Fast;
    }

    // Computational Model
    public void Move(double dt)
    {
      double dx = 0.1; // distance taken in delta time.
      // length of the strait is 20 km.
      switch (Speed)
      {
        case SpeedEnum.VerySlow:
          dx = 1.0 / 30.0 * dt; // in 10 min
          break;
        case SpeedEnum.Slow:
          dx = 1.0 / 15.0 * dt; // in 5 min
          break;
        case SpeedEnum.Fast:
          dx = 1.0 / 6.0 * dt; // in 2 min
          break;
        case SpeedEnum.VeryFast:
          dx = 1.0 / 3.0 * dt; // in 1 min
          break;
      }
      switch (Heading)
      {
        case LocationEnum.West:
          Position.X -= (float)dx;
          //if ((!IsZoneTransferRequested) && (Position.X < 0)) RequestTransfer = true;
          if (Position.X < -10) Exit = true;
          break;
        case LocationEnum.East:
          Position.X += (float)dx;
          //if ((!IsZoneTransferRequested) && (Position.X > 0)) RequestTransfer = true;
          if (Position.X > 10) Exit = true;
          break;
      }
    }
  }

}
