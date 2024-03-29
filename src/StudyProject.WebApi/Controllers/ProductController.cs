﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Interfaces.Application;
using StudyProject.Secutity.Auth;

namespace StudyProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IProductApplicationService _productApplicationService;
        public ProductController(
                     ILogger<AccountController> logger, IConfiguration configuration, IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _productApplicationService.GetAll();
                return Ok(products);
            }
            catch
            {
                return BadRequest("Error in Index");
            }
        }

        // POST: ProductController/Delete/5
        [HttpDelete("{id}")]
        [AuthorizePolicyEnum(PERMISSIONS.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok("Deleteded Succeeded");
            }
            catch
            {
                return BadRequest("Error in Delete");
            }
        }
    }
}
