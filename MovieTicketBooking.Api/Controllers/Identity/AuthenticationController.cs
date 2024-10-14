using AutoMapper;
using Azure;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Domain.Constants.MailSender;
using MovieTicketBooking.Domain.Models;
using System.Net.Mail;
using System.Net;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using MovieTicketBooking.Application.Common.Errors;
using Serilog;
using MovieTicketBooking.Application.Common.SuccessRusult;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using Microsoft.IdentityModel.Tokens;

namespace MovieTicketBooking.Api.Controllers.Identity;

[ApiVersionNeutral]
[Route("api/[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;   
    private readonly UserManager<Customer> _userManager;
    private readonly IConfiguration _configuration;
    public AuthenticationController(ICustomerService customerService,IMapper mapper,UserManager<Customer> userManager,IConfiguration configuration)
    {
        _customerService = customerService;
        _mapper = mapper;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register([FromForm] CustomerDTO customerDTO,string? role)
    {
            Result<Customer> registerResult = await _customerService.Register(customerDTO,role);
            if (registerResult.IsFailed)
            {
                return Problem(registerResult.Errors);
            }
            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(registerResult.Value);
            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Authentication", new { token=code, email = customerDTO.Email }, Request.Scheme);

            await SendEmailAsync(customerDTO.Email, "Confirm your email",
            $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");
        return Ok(registerResult.Value);
    }
    private async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
    {
        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("phamkhanhduy.contact@gmail.com");
            message.To.Add(email);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = confirmLink;

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("phamkhanhduy.contact@gmail.com", "vkqvitiruymstkqr");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    [HttpGet("ConfirmEmail")]
    public async Task<Result> ConfirmEmail(string token,string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user!=null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Result.Ok();
            }
            
        }
        string message = "An error when confirm email for account !";
        Log.Warning($"{this.GetType().Name} - {message} ");
        return Result.Fail(new NotFoundError(message));
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        Customer? user;
        if (loginDTO.UserName.Contains("@")){
            user = await _userManager.FindByEmailAsync(loginDTO.UserName);
        }
        else 
            user= await _userManager.FindByNameAsync(loginDTO.UserName);
        if(user is null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        if (user!=null && await _userManager.CheckPasswordAsync(user,loginDTO.Password)) {
            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwtToken=GetToken(authClaim);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo

            });
        }
        return Unauthorized();
    }
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;   
    }

}

