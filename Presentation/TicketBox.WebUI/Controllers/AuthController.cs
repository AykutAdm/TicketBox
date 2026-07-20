using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using TicketBox.WebUI.DTOs.AuthDtos;

namespace TicketBox.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(loginDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Auth/login", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonTokenData = await responseMessage.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseDto>(jsonTokenData);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                {
                    HttpContext.Session.SetString("JwtToken", tokenResponse.Token);

                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(tokenResponse.Token);

                    var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var firstName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var lastName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, loginDto.Email),
                        new Claim(ClaimTypes.NameIdentifier, userId),
                        new Claim(ClaimTypes.Name, firstName),
                        new Claim(ClaimTypes.Surname, lastName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    if (!string.IsNullOrEmpty(loginDto.ReturnUrl) && Url.IsLocalUrl(loginDto.ReturnUrl))
                        return Redirect(loginDto.ReturnUrl);

                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
            }

            ViewBag.ErrorMessage = "E-posta veya şifre hatalı.";
            return View(loginDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(registerDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7171/api/Auth/register", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kayıt başarılı! Şimdi giriş yapabilirsin.";
                return RedirectToAction("Login");
            }
            return View(registerDto);
        }



        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }
}
