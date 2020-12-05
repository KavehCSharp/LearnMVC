using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrudImageRepository.Models;
using CrudImageRepository.Data;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace CrudImageRepository.Controllers
{
    public class HomeController : Controller
    {
        bool isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRpository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRpository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userRpository = userRpository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index() => View(_userRpository.ReadAll());

        [Route("home/add")]
        [Route("home/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            var user = id != null ? _userRpository.Read(id.Value) : new User();

            if (id != 0 && user == null)
            {
                _logger.LogError($"User #{id} not found");
                return RedirectToAction("index");
            }

            // var model = new UserViewModel();
            // model.Id = user.Id;
            // model.Firstname = user.Firstname;
            // .....

            // OR

            // var model = new UserViewModel
            // {
            //     Id = user.Id,
            //     Firstname = user.Firstname,
            //     ...
            // };

            // OR use AutoMapper
            var model = _mapper.Map<UserViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("home/add")]
        [Route("home/edit/{id}")]
        public IActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModelState.ErrorCount} Error in UserViewModel");
                return View(model);
            }

            var user = _userRpository.Read(model.Id) ?? new User();

            if (model.Id != 0 && user == null)
            {
                _logger.LogError($"User #{model.Id} not found");
                return View(model);
            }

            user = _mapper.Map<User>(model);

            if (user.Id == 0)
                user.Id = _userRpository.Create(user);
            else
                _userRpository.Update(user);

            _logger.LogInformation($"User #{user.Id} Added/Updated");

            return RedirectToAction("index");
        }

        [Route("home/delete/{id}")]
        public IActionResult Delete(int id) 
        {
            if(!_userRpository.Delete(id))
                _logger.LogError($"Something wrong when User #{id} Deleted");
            else
                _logger.LogInformation($"User #{id} Deleted");

            return RedirectToAction("index");
        }

        [Route("home/image/{id}")]
        public IActionResult Image(int id) 
        {
            var user = _userRpository.Read(id);

            return View(new ImageViewModel
            {
                Id = user.Id,
                IsImageStoreInDB = isInDocker?true:user.IsImageStoreInDB,
                Image = user.Image,
                IsInDocker = isInDocker
            });
        }

        [Route("home/deleteimage/{id}")]
        public IActionResult DeleteImage(int id) 
        {
            var user = _userRpository.Read(id);

            if(user!=null)
            {
                user.Image = "";
                _userRpository.Update(user);
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("home/upload/")]
        public async Task<ActionResult> Upload(IFormFile file, ImageViewModel model)
        {
            var user = _userRpository.Read(model.Id);
            if(user != null)
            {
                if (file != null)
                {
                    var image = "";

                    if(!model.IsImageStoreInDB&&!isInDocker)
                    {
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                        if(!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        
                        path = Path.Combine(path, file.FileName); // file.FileName not Safe, use GUID in real app

                        using (var stream = System.IO.File.Create(path))
                        {
                            await file.CopyToAsync(stream);
                        }

                        image = $"/img/{file.FileName}";

                    } else {
                        using (MemoryStream ms = new MemoryStream()) 
                        {
                            await file.CopyToAsync(ms);
                            image = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                    
                    user.Image = image;
                    user.IsImageStoreInDB = model.IsImageStoreInDB;

                    _userRpository.Update(user);
                }
            }

            return RedirectToAction("image", new { Id = model.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
