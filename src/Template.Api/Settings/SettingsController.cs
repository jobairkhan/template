using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Template.Api.Utils;
using Template.Infrastructure;

namespace Template.Api.Settings
{
    /// <summary>
    /// Settings Controller
    /// </summary>
    [Route("api/[controller]")]
    public class SettingsController : BaseController
    {
        private readonly ApiSettings _apiSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="entaSettingOptions"></param>
        public SettingsController(IUnitOfWork unitOfWork,
                                  IOptions<ApiSettings> entaSettingOptions) 
            : base(unitOfWork)
        {
            _apiSettings = entaSettingOptions?.Value;
        }

        /// <summary>
        /// Get All settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => 
            _apiSettings == null 
            ? NotFound() 
            : Ok(_apiSettings);

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/version")]
        public IActionResult GetVersion() => 
            _apiSettings == null 
            ? NotFound() 
            : Ok(_apiSettings.ApiVersion);

        /// <summary>
        /// Get Workstation Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/workstation")]
        public IActionResult GetWorkstation() => 
            _apiSettings == null 
            ? NotFound() 
            : Ok(_apiSettings.WorkStationId);
    }
}
