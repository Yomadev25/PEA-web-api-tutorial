﻿namespace PEA_WebAPI.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public DateTime submitDate { get; set; }
    }
}