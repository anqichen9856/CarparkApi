using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CarparkApi.Services;
using CarparkApi.Models;
using CarparkApi.Util;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarparkApi.Controllers {
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings) {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model) {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect." });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                //expires after 3 hours
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new {
                Id = user.Id,
                Email = user.Email,
                Contact = user.Contact,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model) {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex) {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll() {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpGet("carpark/{date_time}")]
        public async Task<IActionResult> GetCarparkAvailabilityAsync(string date_time) {
            //string _apiUrl = "https://api.data.gov.sg/v1/transport/carpark-availability?";
            //string _baseAddress = "https://api.data.gov.sg/v1/transport/carpark-availability?";

            //using (var client = new HttpClient()) {
            //    client.BaseAddress = new Uri(_baseAddress);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var responseMessage = await client.GetAsync(_apiUrl + "date_time=" + dateTime);

            //    var response = Request.CreateResponse(HttpStatusCode.OK);
            //    response.Content = responseMessage.Content;
            //    return ResponseMessage(response);
            //}
            //using (var httpClient = new HttpClient()) {
            //    using (var response = await httpClient.GetAsync("https://api.data.gov.sg/v1/transport/carpark-availability?date_time=" + dateTime)) {
            //        //string apiResponse = await response.Content.ReadAsStringAsync();
            //        //reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
            //        return Ok(response);
            //    }
            //}
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://api.data.gov.sg/v1/transport/carpark-availability?date_time=" + date_time);
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

    }
}
