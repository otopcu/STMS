using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stms.Som
{
  public class HLAboolean
  {
    public static implicit operator HLAboolean(bool instance)
    {
      //implicit cast logic
      return instance;
    }
    public static implicit operator bool(HLAboolean instance)
    {
      //implicit cast logic
      return instance;
    }
    public HLAboolean() { }
  }
  public class HLAbyte
  {
    public static implicit operator HLAbyte(byte instance)
    {
      //implicit cast logic
      return instance;
    }
    public static implicit operator byte(HLAbyte instance)
    {
      //implicit cast logic
      return instance;
    }
    //public HLAbyte() { }
  }

}
