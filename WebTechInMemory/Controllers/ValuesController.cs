using APPLICATION.DTOs.Auth;
using APPLICATION.DTOs.Request;
using APPLICATION.DTOs.Response;
using APPLICATION.Interfaces;
using APPLICATION.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace WebTechInMemory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Endpoints principales para gestión de usuarios, autenticación y perfiles")]
    public class ValuesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IUserProfileService _profileService;

        public ValuesController(
            IUserService userService,
            IAuthService authService,
            IUserProfileService profileService)
        {
            _userService = userService;
            _authService = authService;
            _profileService = profileService;
        }

        // ==============================================
        // Endpoints de UserService
        // ==============================================
        [HttpGet("users")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
    Summary = "Obtener todos los usuarios",
    Description = "Retorna una lista completa de usuarios registrados en el sistema",
    OperationId = "GetAllUsers"
)]
        [SwaggerResponse(200, "Lista de usuarios obtenida exitosamente", typeof(IEnumerable<UserResponseDto>))]
        [SwaggerResponse(401, "No autorizado. Token JWT requerido")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Obtiene un usuario específico por ID",
            Description = "Retorna los detalles de un usuario basado en su ID único",
            OperationId = "GetUserById"
        )]
        [SwaggerResponse(200, "Usuario encontrado", typeof(UserResponseDto))]
        [SwaggerResponse(404, "Usuario no encontrado")]
        public async Task<IActionResult> GetUserById(
            [SwaggerParameter(Description = "ID único del usuario", Required = true)] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }




        [HttpPost("users")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
        Summary = "Crea un nuevo usuario",
        Description = "Registra un nuevo usuario en el sistema con los datos proporcionados",
        OperationId = "CreateUser")]
        [SwaggerResponse(201, "Usuario creado exitosamente", typeof(UserResponseDto))]
        [SwaggerResponse(400, "Datos de entrada inválidos")]
        public async Task<IActionResult> CreateUser(
    [FromBody, SwaggerRequestBody(
        Description = "Datos requeridos para creación de usuario",
        Required = true)] UserCreateDto userDto)
        {
            var userId = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userDto);
        }





        // ==============================================
        // Endpoints de AuthService
        // ==============================================
        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("auth/refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshDto)
        {
            var result = await _authService.RefreshTokenAsync(refreshDto);
            return Ok(result);
        }

        // ==============================================
        // Endpoints de ProfileService
        // ==============================================
        [HttpPut("profiles/{userId}/picture")]
        public async Task<IActionResult> UpdateProfilePicture(int userId, [FromBody] string imageUrl)
        {
            //await _profileService.UpdateProfilePictureAsync(userId, imageUrl);
            return NoContent();
        }

        [HttpPut("profiles/{userId}/bio")]
        public async Task<IActionResult> UpdateBiography(int userId, [FromBody] string bio)
        {
            await _profileService.UpdateBiographyAsync(userId, bio);
            return NoContent();
        }
    }
}
