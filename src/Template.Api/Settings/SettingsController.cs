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
        private readonly EntaSettings _entaSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="entaSettingOptions"></param>
        public SettingsController(IUnitOfWork unitOfWork,
                                  IOptions<EntaSettings> entaSettingOptions) 
            : base(unitOfWork)
        {
            _entaSettings = entaSettingOptions?.Value;
        }

        /// <summary>
        /// Get All settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => 
            _entaSettings == null 
            ? NotFound() 
            : Ok(_entaSettings);

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/version")]
        public IActionResult GetVersion() => 
            _entaSettings == null 
            ? NotFound() 
            : Ok(_entaSettings.EntaVersion);

        /// <summary>
        /// Get Workstation Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/workstation")]
        public IActionResult GetWorkstation() => 
            _entaSettings == null 
            ? NotFound() 
            : Ok(_entaSettings.WorkStationId);
    }
}
