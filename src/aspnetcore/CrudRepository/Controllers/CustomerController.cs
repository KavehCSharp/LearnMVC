using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrudRepository.Models;
using CrudRepository.Data;
using AutoMapper;

namespace CrudRepository.Controllers
{
    public class CustomerController : Controller
    {
        // Default Code
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        // Mine
        protected ICustomerRepository CustomerRepository { get; }

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository, IMapper mapper)
        {
            _logger = logger;
            CustomerRepository = customerRepository;
            _mapper = mapper;
        }

        // My Code
        public IActionResult Index() => View(CustomerRepository.ReadAll());

        [Route("customers/add")]
        [Route("customers/edit/{id}")]
        public IActionResult Edit(Guid? id)
        {
            var customer = id != null ? CustomerRepository.Read(id.Value) : new Customer();

            if (id != Guid.Empty && customer == null)
            {
                _logger.LogError($"Customer #{id} not found");
                return RedirectToAction("index");
            }

            // var model = new CustomerViewModel();
            // model.Id = customer.Id;
            // model.Firstname = customer.Firstname;
            // .....

            // OR

            // var model = new CustomerViewModel
            // {
            //     Id = customer.Id,
            //     Firstname = customer.Firstname,
            //     ...
            // };

            // OR use AutoMapper
            var model = _mapper.Map<CustomerViewModel>(customer);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("customers/add")]
        [Route("customers/edit/{id}")]
        public IActionResult Edit(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"{ModelState.ErrorCount} Error in CustomerViewModel");
                return View(model);
            }

            var customer = CustomerRepository.Read(model.Id) ?? new Customer();

            if (model.Id != Guid.Empty && customer == null)
            {
                _logger.LogError($"Customer #{model.Id} not found");
                return View(model);
            }

            customer = _mapper.Map<Customer>(model);

            if (customer.Id == Guid.Empty)
                customer.Id = CustomerRepository.Create(customer);
            else
                CustomerRepository.Update(customer);

            _logger.LogInformation($"Customer #{customer.Id} Added/Updated");

            return RedirectToAction("index");
        }

        [Route("customers/delete/{id}")]
        public IActionResult Delete(Guid id) 
        {
            if(!CustomerRepository.Delete(id))
                _logger.LogError($"Something wrong when Customer #{id} Deleted");
            else
                _logger.LogInformation($"Customer #{id} Deleted");

            return RedirectToAction("index");
        }

        // Default Code
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
