using System;
using System.Collections.Generic;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class DayOfTheWeek
{
    private DayOfTheWeek()
    {
        Availability = new HashSet<Availability>();
    }

    public int IdDayOfTheWeek { get; private set; }
    public string Name { get; private set; }

            public virtual ICollection<Availability> Availability { get; private set; }
}
}
