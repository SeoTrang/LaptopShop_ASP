using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LapTopShop.Models
{
    public class Product
    {
        
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

    
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Status { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string TypeLaptop { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        // [MaxLength]
        public string ProductName { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public int OldPrice { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public int Discount { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public int Price { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public int Quantity { set; get; }
        
        // [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Avatar { set; get; }
        
        // [Required(ErrorMessage = "Vui lòng nhập !")]
        public List<Img> ListFileDetail { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string CpuTechnology { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Kernel { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Threads { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ProcessorSpeedCPU { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string MaxSpeed { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Caching { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Ram { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string TypeRAM { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string RAMBusSpeed { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string MaxRam { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string HardDrive { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ScreenSize { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ScreenResolution { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ScanFrequency { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ScreenTechnology { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string GraphicCard { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string AudioTechnology { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Connector { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string WirelessConnectivity { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Webcam { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string KeyboardLight { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Size { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Weight { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Material { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Battery { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string Os { set; get; }
        
        [Required(ErrorMessage = "Vui lòng nhập !")]
        public string ReleaseTime { set; get; }

        [AllowNull]
        public string Article { set; get; }


        // Trường để tải lên ảnh đại diện
        [BindProperty]
        [DataType(DataType.Upload)]
        // [Required(ErrorMessage ="Chọn file upload")]
        [DisplayName("File Upload")]
        [NotMapped]
        public IFormFile MainImageFile { get; set; }

        // Trường để tải lên danh sách ảnh chi tiết
        [BindProperty]
        [Required(ErrorMessage ="Chọn danh sách file upload")]
        [DisplayName("List File Upload")]
        [NotMapped]
        public List<IFormFile> ProductImageFiles { get; set; }

        public Category Category { get; set; }

    }
}