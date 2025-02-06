using Microsoft.AspNetCore.Identity;

namespace MVM.LojaVirtual.Identidade.API.Extensions;

public class IdentityMessagesPT : IdentityErrorDescriber
{
    public override IdentityError DefaultError()
        => new() { Code = nameof(DefaultError), Description = "Ocorreu um erro inesperado." };

    public override IdentityError ConcurrencyFailure()
        => new() { Code = nameof(ConcurrencyFailure), Description = "Erro de concorrência. Tente novamente." };

    public override IdentityError PasswordTooShort(int length)
        => new() { Code = nameof(PasswordTooShort), Description = $"A senha deve ter no mínimo {length} caracteres." };

    public override IdentityError PasswordRequiresDigit()
        => new() { Code = nameof(PasswordRequiresDigit), Description = "A senha deve conter pelo menos um número." };

    public override IdentityError PasswordRequiresLower()
        => new() { Code = nameof(PasswordRequiresLower), Description = "A senha deve conter pelo menos uma letra minúscula." };

    public override IdentityError PasswordRequiresUpper()
        => new() { Code = nameof(PasswordRequiresUpper), Description = "A senha deve conter pelo menos uma letra maiúscula." };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        => new() { Code = nameof(PasswordRequiresUniqueChars), Description = $"A senha deve ter pelo menos {uniqueChars} caracteres únicos." };

    public override IdentityError InvalidUserName(string userName)
        => new() { Code = nameof(InvalidUserName), Description = $"O nome de usuário '{userName}' é inválido." };

    public override IdentityError InvalidEmail(string email)
        => new() { Code = nameof(InvalidEmail), Description = $"O e-mail '{email}' não é válido." };

    public override IdentityError DuplicateUserName(string userName)
        => new() { Code = nameof(DuplicateUserName), Description = $"O nome de usuário '{userName}' já está em uso." };

    public override IdentityError DuplicateEmail(string email)
        => new() { Code = nameof(DuplicateEmail), Description = $"O e-mail '{email}' já está em uso." };

    public override IdentityError UserAlreadyHasPassword()
        => new() { Code = nameof(UserAlreadyHasPassword), Description = "O usuário já possui uma senha definida." };

    public override IdentityError UserLockoutNotEnabled()
        => new() { Code = nameof(UserLockoutNotEnabled), Description = "O bloqueio de conta não está habilitado para este usuário." };

    public override IdentityError UserAlreadyInRole(string role)
        => new() { Code = nameof(UserAlreadyInRole), Description = $"O usuário já está na função '{role}'." };

    public override IdentityError UserNotInRole(string role)
        => new() { Code = nameof(UserNotInRole), Description = $"O usuário não está na função '{role}'." };

    public override IdentityError PasswordMismatch()
        => new() { Code = nameof(PasswordMismatch), Description = "A senha informada está incorreta." };

    public override IdentityError InvalidToken()
        => new() { Code = nameof(InvalidToken), Description = "Token inválido." };

    public override IdentityError LoginAlreadyAssociated()
        => new() { Code = nameof(LoginAlreadyAssociated), Description = "Este login já está associado a um usuário." };

    public override IdentityError InvalidRoleName(string role)
        => new() { Code = nameof(InvalidRoleName), Description = $"O nome da função '{role}' é inválido." };

    public override IdentityError DuplicateRoleName(string role)
        => new() { Code = nameof(DuplicateRoleName), Description = $"A função '{role}' já existe." };

    public override IdentityError RecoveryCodeRedemptionFailed()
        => new() { Code = nameof(RecoveryCodeRedemptionFailed), Description = "Falha ao validar o código de recuperação." };

    public override IdentityError PasswordRequiresNonAlphanumeric()
        => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve conter pelo menos um caractere especial." };

}