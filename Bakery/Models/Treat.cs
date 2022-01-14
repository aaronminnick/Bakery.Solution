using System.Collections.Generic;

namespace Bakery.Models
{
  public class Treat
  {
    public Treat()
    {
      FlavorTreats = new HashSet<FlavorTreat> {};
    }
    
    public int TreatId {get; set;}
    public double Price {get; set;}
    public string Description {get; set;}
    public virtual ICollection<FlavorTreat> FlavorTreats {get;}
  }
}