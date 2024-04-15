
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JizdniRad.Models;

[Table("Line")]
public partial class Line
{
    public int ID { get; set; }

    public DateTime ValidTo { get; set; }

    public string Name { get; set; }

    public string Zone { get; set; }

    public int? FirstStopID { get; set; }

    public virtual ICollection<Departure> Departures { get; set; } = new List<Departure>();

    [ForeignKey("ID")]
    public virtual LineStop? FirstStop { get; set; }
}