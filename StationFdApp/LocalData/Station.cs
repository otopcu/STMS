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
  public class CStation
  {
    public string StationName;
    public LocationEnum Location;

    public CStation()
    {
      StationName = "";
      Location = LocationEnum.West;
    }

    // Computational Model
  }

}
