using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
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
        public IFormFile? File { get; set; }
        [FileExtensions(Extensions = "doc,docx,txt,rtf"), NotMapped]
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


        public int GetWordCount()
        {
            int wordCount = 0;

            if (Path.GetExtension(FileName) == ".doc" || Path.GetExtension(FileName) == ".docx")
            {
                using (var document = WordprocessingDocument.Open(DocumentPath, false))
                {
                    if (document.ExtendedFilePropertiesPart is not null)
                    if (document.ExtendedFilePropertiesPart.Properties.Words is not null)
                    {
                        int.TryParse(document.ExtendedFilePropertiesPart.Properties.Words.Text, out wordCount);
                    }
                }
            }
            else if (Path.GetExtension(FileName) == ".txt" || Path.GetExtension(FileName) == ".rtf")
            {
                using (var stream = new System.IO.StreamReader(DocumentPath, true))
                {
                    // string text = stream.ReadToEnd();
                    string text = System.IO.File.ReadAllText(DocumentPath, stream.CurrentEncoding);

                    if (Path.GetExtension(FileName) == ".rtf")
                    {
                        var rtfDeletionRegex = new Regex(@"({[^}]*}|{|}|\\[\w]*) ?");
                        text = rtfDeletionRegex.Replace(text, "");
                    }
                    
                    var wordsRegex = new Regex(@"\b\w*\b");
                    wordCount = wordsRegex.Matches(text).Count();
                }                
            }
            
            wordCount = (int) Math.Round(wordCount / 10d) * 10; 
            return wordCount;
        }
    }
}