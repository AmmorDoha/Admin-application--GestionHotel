using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AdminApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }
    public bool IsAvailable { get; set; }
    public string Status { get; set; }
    public List<Reservation> Reservations { get; set; }
}