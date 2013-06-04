using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EasyERP.Models
{
    public class ImageUploader
    {
        private HttpPostedFileBase file;
        private string memberForErrorMessages;
        private List<ValidationResult> validationResults;
        private Controller controller;
        private string[] mimeTypes = { "image/png", 
                                        "image/jpeg", 
                                        "image/bmp", 
                                        "image/x-windows-bmp", 
                                        "image/gif" };

        public ImageUploader(Controller controller, HttpPostedFileBase file, string memberForErrorMessages)
        {
            if (file == null)
            {
                throw new Exception("nullObject");
            }

            this.controller = controller;
            this.file = file;
            this.memberForErrorMessages = memberForErrorMessages;
            validationResults = new List<ValidationResult>();
        }

        public void Validate()
        {
            if(file.ContentLength <= 0)
            {
                validationResults.Add(new ValidationResult("Przesłany plik jest pusty", new string[] { memberForErrorMessages }));
            }

            if(!mimeTypes.Contains(file.ContentType))
            {
                validationResults.Add(new ValidationResult("Plik musi być typu png|jpg|bmp|gif", new string[] { memberForErrorMessages }));
            }

            foreach (var vr in validationResults)
            {
                controller.ModelState.AddModelError(vr.MemberNames.First(), vr.ErrorMessage);
            }
        }

        public bool IsValid()
        {
            return !validationResults.Any();
        }

        public void Save(string name, string path)
        {
            file.SaveAs(Path.Combine(path, Path.GetFileName(name)));
        }
    }
}
