using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Shortchase.Authorization;
using Shortchase.Entities;

using Shortchase.Services;
using Shortchase.Dtos;
using Shortchase.Helpers;

using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shortchase.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickController : ControllerBase
    {
        private readonly IPickService pickService;
        private readonly IErrorLogService errorLogService;
        private readonly IWebHostEnvironment hostingEnvironment;
        public PickController(IPickService pickService, IErrorLogService errorLogService, IWebHostEnvironment hostEnvironment)
        {
            this.errorLogService = errorLogService;
            this.pickService = pickService;
            this.hostingEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> GetPicks()
        {
            try
            {
                return Ok(await pickService.GetAll().ConfigureAwait(true));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Serval Error");
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> CreatePickNewPicWithPhoto([FromBody] CreatePickDto data)
        {
           //  return Ok(data);
            string team1UniqueNameAndPath = null;
            string team2UniqueNameAndPath = null;
            try
            {
                if (string.IsNullOrWhiteSpace(data.Team1)) throw new Exception("Team 1 is mandatory!");
                if (string.IsNullOrWhiteSpace(data.Team2)) throw new Exception("Team 2 is mandatory!");
                if (string.IsNullOrWhiteSpace(data.StartTime)) throw new Exception("Start Time is mandatory!");
                if (string.IsNullOrWhiteSpace(data.FinishTime)) throw new Exception("Finish Time is mandatory!");
                if (data.Team1PhotoFile == null) throw new Exception("You must seelect Photo for Team 1 !");
                if (data.Team1PhotoFile == null) throw new Exception("You must seelect Photo for Team 2 !");
                if (data.CategoryId == 0) throw new Exception("Category is mandatory!");

                DateTime dateStart = DateHelper.FromISO(data.StartTime);
                dateStart = dateStart.AddMinutes(data.TimezoneOffset);
                DateTime dateEnd = DateHelper.FromISO(data.FinishTime);
                dateEnd = dateEnd.AddMinutes(data.TimezoneOffset);


                // string team1FilePath = null;
                // string team2FilePath = null;
                if (data.Team1PhotoFile != null && data.Team2PhotoFile != null)
                {
                    string extension = Path.GetExtension(data.Team1PhotoFile.FileName);
                    long size = data.Team1PhotoFile.Length;
                    if (FileHelper.FileIsFormSafe(extension, size))
                    {
                        string basePath = hostingEnvironment.ContentRootPath;
                        string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\Pick");
                        Directory.CreateDirectory(mediaPath);

                        team1UniqueNameAndPath = FileHelper.PathCombine(mediaPath, Guid.NewGuid().ToString() + Path.GetExtension(data.Team1PhotoFile.FileName));
                        using (var fileStream = new FileStream(team1UniqueNameAndPath, FileMode.Create))
                        {
                            await data.Team1PhotoFile.CopyToAsync(fileStream).ConfigureAwait(true);
                        }
                        // user.ProfilePicture = path;
                    }
                    else
                    {
                        return Ok(new { status = false, messageTitle = "Home Team File type is not acceptable!", message = "Try to upload one of the following types: JPG or PNG" });
                    }



                    string extension2 = Path.GetExtension(data.Team2PhotoFile.FileName);
                    long size2 = data.Team2PhotoFile.Length;
                    if (FileHelper.FileIsFormSafe(extension2, size2))
                    {
                        string basePath = hostingEnvironment.ContentRootPath;
                        string mediaPath = FileHelper.PathCombine(basePath, "\\Media\\", "\\Pick");
                        Directory.CreateDirectory(mediaPath);

                        team2UniqueNameAndPath = FileHelper.PathCombine(mediaPath, Guid.NewGuid().ToString() + Path.GetExtension(data.Team2PhotoFile.FileName));
                        using (var fileStream = new FileStream(team1UniqueNameAndPath, FileMode.Create))
                        {
                            await data.Team2PhotoFile.CopyToAsync(fileStream).ConfigureAwait(true);
                        }
                        // user.ProfilePicture = path;
                    }
                    else
                    {
                        return Ok(new { status = false, messageTitle = "Away Team File type is not acceptable!", message = "Try to upload one of the following types: JPG or PNG" });
                    }

                    /* var UploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                     team1UniqueName =  Guid.NewGuid().ToString() + "_" + data.Team1PhotoFile.FileName;
                     team2UniqueName = Guid.NewGuid().ToString() + "_" + data.Team2PhotoFile.FileName;
                     team1FilePath = Path.Combine(UploadFolder, team1UniqueName);
                     team2FilePath = Path.Combine(UploadFolder, team2UniqueName);
                     await data.Team1PhotoFile.CopyToAsync( new FileStream(team1FilePath, FileMode.Create )).ConfigureAwait(true);
                     await data.Team2PhotoFile.CopyToAsync(new FileStream(team2UniqueName, FileMode.Create)).ConfigureAwait(true);*/
                }
                if (dateEnd <= dateStart) throw new Exception("Start Time must be greater than Finish Time!");

                Pick item = new Pick
                {
                    Team1 = data.Team1,
                    Team2 = data.Team2,
                    StartTime = dateStart,
                    FinishTime = dateEnd,
                    CategoryId = data.CategoryId,
                    Team1PhotoPath = team1UniqueNameAndPath,
                    Team2PhotoPath = team2UniqueNameAndPath,
                    IsEnabled = true,
                };

                var result = await pickService.Insert(item).ConfigureAwait(true);
                if (result)
                {

                    return Ok(new { status = true, messageTitle = "Success", message = "New pick saved successfully!" });
                }
                else throw new Exception("Error creating new pick. Try again later.");

            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
                await errorLogService.InsertException(e).ConfigureAwait(true);
                return Ok(new { status = false, messageTitle = "Error", message = e.Message });
            }
        }
    }

}
      
