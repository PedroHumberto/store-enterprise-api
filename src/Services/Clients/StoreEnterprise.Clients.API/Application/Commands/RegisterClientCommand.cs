using Core.APP.DomainObjects;
using Core.APP.Messages;
using FluentValidation;

namespace StoreEnterprise.Clients.API.Application.Commands
{
    //realiza o transporte dos dados do cliente para o banco, aqui define quais dados irei levar
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            //Para popular a informação do aggregate
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
    public class RegisterClientCommandValidation : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid ID");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Invalid Email");
            
            RuleFor(c => c.Cpf)
                .Must(ValidCpf)
                .WithMessage("Invalid CPF");
            
            RuleFor(c => c.Email)
                .Must(ValidEmail)
                .WithMessage("Invalid Email");            
        }

        protected static bool ValidCpf(string cpf)
        {
            return Cpf.ValidateCPF(cpf);
        }

        protected static bool ValidEmail(string email)
        {
            return Email.EmailValidation(email);
        }
    }
}