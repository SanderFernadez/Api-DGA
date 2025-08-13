using Api_DGA.Application.Dtos.Auth;
using Api_DGA.Application.Interfaces.Repositories;
using Api_DGA.Application.Interfaces.Services;
using Api_DGA.Core.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Api_DGA.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            Console.WriteLine($"🔐 Iniciando login para: {loginDto.Email}");
            
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            
            if (user == null)
            {
                Console.WriteLine($"❌ Usuario no encontrado: {loginDto.Email}");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Credenciales inválidas"
                };
            }

            if (!user.IsActive)
            {
                Console.WriteLine($"❌ Usuario inactivo: {loginDto.Email}");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Usuario inactivo"
                };
            }

            Console.WriteLine($"🔍 Verificando contraseña para usuario: {user.Id}");
            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                Console.WriteLine($"❌ Contraseña incorrecta para: {loginDto.Email}");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Credenciales inválidas"
                };
            }

            Console.WriteLine($"✅ Usuario autenticado: {user.Id} - {user.Email}");
            
            // Obtener roles del usuario
            var userWithRoles = await _userRepository.GetByIdWithRolesAsync(user.Id);
            var roles = userWithRoles?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? new List<string>();
            
            Console.WriteLine($"👥 Roles del usuario: {string.Join(", ", roles)}");

            // Generar tokens
            Console.WriteLine($"🔑 Generando tokens para usuario: {user.Id}");
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();
            
            Console.WriteLine($"✅ Tokens generados exitosamente");

            // Actualizar refresh token en la base de datos
            await _userRepository.UpdateRefreshTokenAsync(
                user.Id, 
                refreshToken, 
                DateTime.UtcNow.AddDays(7)
            );

            // Actualizar último login
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, user.Id);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login exitoso",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                User = new UserInfoDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Roles = roles
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Verificar si el email ya existe
            if (await _userRepository.EmailExistsAsync(registerDto.Email))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "El email ya está registrado"
                };
            }

            // Crear nuevo usuario
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            // Asignar rol por defecto (Usuario)
            var defaultRole = await _roleRepository.GetByNameAsync("Usuario");
            if (defaultRole != null)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id,
                    AssignedAt = DateTime.UtcNow
                };
                await _userRoleRepository.AddAsync(userRole);
            }

            return new AuthResponseDto
            {
                Success = true,
                Message = "Usuario registrado exitosamente"
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var users = await _userRepository.GetAllListAsync();
            var userWithValidToken = users.FirstOrDefault(u => 
                u.RefreshToken == refreshTokenDto.RefreshToken && 
                u.RefreshTokenExpiryTime > DateTime.UtcNow);

            if (userWithValidToken == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Refresh token inválido o expirado"
                };
            }

            // Obtener roles del usuario
            var userWithRoles = await _userRepository.GetByIdWithRolesAsync(userWithValidToken.Id);
            var roles = userWithRoles?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? new List<string>();

            // Generar nuevos tokens
            var newAccessToken = _jwtService.GenerateAccessToken(userWithValidToken, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Actualizar refresh token
            await _userRepository.UpdateRefreshTokenAsync(
                userWithValidToken.Id,
                newRefreshToken,
                DateTime.UtcNow.AddDays(7)
            );

            return new AuthResponseDto
            {
                Success = true,
                Message = "Token renovado exitosamente",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var users = await _userRepository.GetAllListAsync();
            var userWithToken = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (userWithToken == null)
                return false;

            await _userRepository.UpdateRefreshTokenAsync(userWithToken.Id, null, null);
            return true;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            return _jwtService.ValidateToken(token);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                Console.WriteLine("🔍 Obteniendo todos los usuarios");
                var users = await _userRepository.GetAllListAsync();
                
                // Filtrar información sensible
                var filteredUsers = users.Select(u => new User
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginAt = u.LastLoginAt,
                    // No incluir PasswordHash, RefreshToken, etc.
                }).ToList();

                Console.WriteLine($"✅ Se obtuvieron {filteredUsers.Count} usuarios");
                return filteredUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error al obtener usuarios: {ex.Message}");
                throw;
            }
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash == hashed;
        }
    }
}
