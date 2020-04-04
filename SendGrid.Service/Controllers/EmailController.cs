using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid.Service.Helpers;
using SendGrid.Service.Models;

namespace SendGrid.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IOptions<SendGridSettings> _sgConfig;

        public EmailController(IOptions<SendGridSettings> sgConfig, ISendGridClient sendGridClient)
        {
            _sgConfig = sgConfig;
            _sendGridClient = sendGridClient;
        }

        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(string emailAddress, string templateid, [FromBody]Dictionary<string, string> Substitutions)
        {
            try
            {
                var msg = new SendGridMessage()
                {
                    TemplateId = templateid,
                    From = new EmailAddress(_sgConfig.Value.FromAddress, _sgConfig.Value.FromName)
                };

                msg.AddTo(emailAddress);

                if (Substitutions.Any())
                {
                    msg.SetTemplateData(Substitutions);
                }

                var result = await _sendGridClient.SendEmailAsync(msg);

                if (!result.StatusCode.IsSuccessStatusCode())
                {
                    var content = await result.Body.ReadAsStringAsync();
                    throw new Exception(content);
                }                

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}