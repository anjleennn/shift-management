using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using shift_management.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace shift_management.Controllers
{
    public class EmailTempelatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ITempDataProvider _tempDataProvider;
        private object _viewEngine;

        public EmailTempelatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> SendInvitations(int shiftId)
        {
            var shift = await _context.Shifts.FindAsync(shiftId);
            var workers = await _context.Workers.ToListAsync();

            foreach (var worker in workers)
            {
                var emailBody = RenderRazorViewToString("Views/EmailTemplates/ShiftInvitation.cshtml", new { Worker = worker, Shift = shift });
                SendEmail(worker.Email, "Shift Invitation", emailBody);

                _context.InvitationLogs.Add(new InvitationLog
                {
                    WorkerId = worker.WorkerId,
                    ShiftId = shift.ShiftId,
                    SentDate = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void SendEmail(string email, string subject, string body)
        {
            using (var message = new MailMessage("your_email@example.com", email))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("your_smtp_server"))
                {
                    client.Credentials = new NetworkCredential("your_username", "your_password");
                    client.Send(message);
                }
            }
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model };
            var tempData = new TempDataDictionary(ControllerContext.HttpContext, _tempDataProvider);
            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, viewData, tempData, writer, new HtmlHelperOptions());
                viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }

    }
}
