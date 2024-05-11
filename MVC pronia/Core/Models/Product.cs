using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;
public class Product:BaseEntity
{
    public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
    public Category? Category  { get; set; }
    public int? CategoryId { get; set; }
    public List<Tag> Tags { get; set; }
    public string? MainImgUrl { get; set; }
    [NotMapped]
    public IFormFile MainPhotoFile { get; set; }
    public string? ImgUrl { get; set; }
    [NotMapped]
    public IFormFile PhotoFile { get; set; }
}
