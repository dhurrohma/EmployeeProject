using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("kemandoran")]
        public string Kemandoran { get; set; }
    }
}
