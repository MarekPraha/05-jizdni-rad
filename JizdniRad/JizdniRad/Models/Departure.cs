
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JizdniRad.Models;

[Table("Departures")]
public partial class Departure
{
    public int ID { get; set; }

    public int LineID { get; set; }

    public TimeSpan Time { get; set; }

    [Column("WeekDay")]
    public int WeekDayNumber { get; set; }

    public virtual Line Line { get; set; }

    [NotMapped]
    public WeekDay WeekDay
    {
        get => (WeekDay)WeekDayNumber;
        set => WeekDayNumber = (int)value;
    }

}

public enum WeekDay
{
    Normal = 0,
    Saturday = 1,
    Sunday = 2,
}