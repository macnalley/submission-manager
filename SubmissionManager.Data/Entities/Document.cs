using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SubmissionManager.Data.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SubmissionId")]
        public int SubmissionId { get; set; }
        [Required, NotMapped]
        public IFormFile File { get; set; }
        [FileExtensions(Extensions = "doc,docx,odt,txt,rtf"), NotMapped]
        public string UploadedFileName 
        {
            get
            {
                if (File != null)
                    return File.FileName;
                else
                    return "";
            }
        }
        public string FileName { get; set; } = "";
        public string DocumentPath { get; set; } = "";
    }
}