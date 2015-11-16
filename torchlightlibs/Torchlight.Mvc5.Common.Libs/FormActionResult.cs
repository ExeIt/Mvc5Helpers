using System;
using System.Web.Mvc;
using StructureMap;
using Torchlight.Mvc5.Common.Libs.Interfaces;

namespace Torchlight.Mvc5.Common.Libs
{
    public class FormActionResult<T> : ActionResult
    {
        public ViewResult Failure { get; private set; }
        public ActionResult Success { get; private set; }
        public T Form { get; private set; }

        public FormActionResult(T form, ActionResult success, ViewResult failure)
        {
            Form = form;
            Success = success;
            Failure = failure;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!context.Controller.ViewData.ModelState.IsValid)
            {
                Failure.ExecuteResult(context);

                return;
            }

            var handler = ObjectFactory.GetInstance<IFormHandler<T>>();

            try
            {
                if (handler.Handle(Form))
                    Success.ExecuteResult(context);
                else
                    ReportError(context);
            }
            catch (Exception)
            {
                ReportError(context);
            }
        }

        private void ReportError(ControllerContext context)
        {
            //context.Controller.ViewData.ModelState.AddModelError(
            //    string.Empty, Common.Msg_ErrorOccurred);

            Failure.ExecuteResult(context);
        }
    }
}
