using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AdminApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class RoomType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal BasePrice { get; set; }
    public int Capacity { get; set; }
    public List<Room> Rooms { get; set; }
}