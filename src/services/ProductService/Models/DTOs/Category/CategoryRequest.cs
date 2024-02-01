﻿namespace ProductService.Models.DTOs.Category
{
    public class CategoryRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public bool? Enable { get; set; } = true;
    }
}