
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JizdniRad.Models;

[Table("Stop")]
public class Stop
{
    public int ID { get; set; }
    public string Name { get; set; }
}