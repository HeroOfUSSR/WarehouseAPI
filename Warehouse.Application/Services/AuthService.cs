using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Models;

namespace Warehouse.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> RegisterAsync(string email, string password, UserRole role, CancellationToken ct)
        {
            var existing = await _userRepository.GetByEmailAsync(email, ct);
            if (existing is not null)
                throw new DuplicateEmailException(email);

            var passwordHash = _passwordHasher.Hash(password);
            var user = new AppUser(email, passwordHash, role);

            await _userRepository.AddAsync(user, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return user.Id;
        }

        public async Task<string> LoginAsync(string email, string password, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmailAsync(email, ct)
                ?? throw new InvalidCredentialsException();

            if (!user.IsActive)
                throw new InvalidCredentialsException();

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new InvalidCredentialsException();

            return _tokenGenerator.GenerateToken(user);
        }
    }
}
