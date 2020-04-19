using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoGallery.Models;
using DemoGallery.Models.Services.Application;
using DemoGallery.Models.ViewModels;
using DemoGallery.Models.InputModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DemoGallery.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }
        public IActionResult Index()
        {
            AlbumViewModel album = albumService.GetAlbum("Default");
            //Aggiungi eventuali errori restituiti dalle altre action decorate con HttpPost
            AddErrorsToModelState(ModelState, TempData);
            return View(album);
        }


        [HttpPost]
        public IActionResult AddImages(ImageUploadInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                albumService.AddImages(inputModel);
            }
            else
            {
                AddModelStateErrorsToTempData(ModelState, TempData);
            }
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult RotateImageClockwise(ImageEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                albumService.RotateImageClockwise(inputModel);
            }
            else
            {
                AddModelStateErrorsToTempData(ModelState, TempData);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveImage(ImageEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                albumService.RemoveImage(inputModel);
            }
            else
            {
                AddModelStateErrorsToTempData(ModelState, TempData);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SetDefaultImage(ImageEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                albumService.SetDefaultImage(inputModel);
            }
            else
            {
                AddModelStateErrorsToTempData(ModelState, TempData);
            }
            return RedirectToAction(nameof(Index));
        }

        #region Helper methods
        private void AddErrorsToModelState(ModelStateDictionary modelState, ITempDataDictionary tempData)
        {
            var errors = tempData["Errors"] as IEnumerable<string>;
            if (errors != null)
            {
                foreach(var error in errors)
                {
                    modelState.AddModelError("", error);
                }
            }
        }
        
        private void AddModelStateErrorsToTempData(ModelStateDictionary modelState, ITempDataDictionary tempData)
        {
            tempData["Errors"] = modelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
        }
        #endregion
    }
}
