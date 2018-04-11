using Microsoft.AspNetCore.Mvc;
using Template.Infrastructure;

namespace Template.Api.Utils
{
    /// <inheritdoc>Microsoft.AspNetCore.Mvc.Controller</inheritdoc>
    /// <summary>
    /// The base controller that wraps results with an <see cref="Envelope"/>
    /// </summary> 
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns an <see cref="Envelope"/> containing empty OKResult
        /// </summary>
        /// <returns></returns>
        protected new IActionResult Ok()
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok());
        }

        /// <summary>
        /// Returns an <see cref="Envelope"/> containing OKResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult Ok<T>(T result)
        {
            _unitOfWork.Commit();
            return base.Ok(Envelope.Ok(result));
        }

        /// <summary>
        /// Returns an <see cref="Envelope"/> containing error information
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }
    }
}
