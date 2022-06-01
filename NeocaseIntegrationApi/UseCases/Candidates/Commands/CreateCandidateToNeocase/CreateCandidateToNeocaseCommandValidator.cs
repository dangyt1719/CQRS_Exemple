using Entities;
using FluentValidation;

namespace UseCases.Candidates.Commands.CreateCandidateToNeocase
{
    public class CreateCandidateToNeocaseCommandValidator : AbstractValidator<Candidate>
    {
        public CreateCandidateToNeocaseCommandValidator()
        {
            RuleFor(p => p.LastName).NotNull().NotEmpty().WithMessage("Фамилия кандидата - должна быть задана");

            RuleFor(p => p.FirstName).NotNull().NotEmpty().WithMessage("Имя кандидата - должно быть задано");

            RuleFor(p => p.DateOfBirth).NotNull().WithMessage("Дата рождения - должна быть задана");

            RuleFor(p => p.Gender).NotNull().NotEmpty().WithMessage("Пол кандидата - должен быть задан");

            RuleFor(p => p.FirstWorkDay).NotNull().WithMessage("Дата выхода - должна быть определена");

            RuleFor(p => p.Position).NotNull().WithMessage("Идентификатор должности - должен быть задан и не должен равняться нулю");
            RuleFor(p => p.Position.Id).NotNull().WithMessage("Идентификатор подразделения - должен быть задан");

            RuleFor(p => p.UnitId).NotNull().NotEmpty().WithMessage("Идентификатор подразделения - должен быть задан");

            RuleFor(p => p.Mvz).NotNull().NotEmpty().WithMessage("МВЗ - должен быть задан");

            RuleFor(p => p.Grade).NotNull().WithMessage("Идентификатор грейда - должен быть задан и не должен равняться нулю");
            RuleFor(p => p.Grade.Id).NotNull().WithMessage("Идентификатор грейда - должен быть задан");

            RuleFor(p => p.Region).NotNull().WithMessage("Данные по региону должны быть заданы");
            RuleFor(p => p.Region.Id).NotNull().WithMessage("Данные по региону должны быть заданы - не задан идентификатор региона");
            RuleFor(p => p.Region.Title).NotNull().NotEmpty().WithMessage("Данные по региону должны быть заданы - не задано имя региона");
            RuleFor(p => p.Region.OKATOCode).NotNull().NotEmpty().WithMessage("Данные по региону должны быть заданы - не задан код ОКАТО");

            RuleFor(p => p.LegalPerson).NotNull().WithMessage("Идентификатор юридического лица - должен быть задан");
            RuleFor(p => p.LegalPerson.Id).NotNull().NotEmpty().WithMessage("Идентификатор юридического лица - должен быть задан");

            RuleFor(p => p.ProbationPeriod).NotNull().WithMessage("Испытательный срок - должен быть задан");
            RuleFor(p => p.ProbationPeriod.Id).NotNull().WithMessage("Испытательный срок - должен быть задан");

            //RuleFor(p => p.Phone).NotNull()
            //                     .NotEmpty()
            //                     .Matches(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")
            //                     .WithMessage("Требуется указать один номер телефона в формате +X(XXX)XXX-XXXX");

            RuleFor(p => p.ContractType).NotNull().WithMessage("Тип трудового договора - должен быть указан");
            RuleFor(p => p.ContractType.Id).NotNull().WithMessage("Тип трудового договора - должен быть указан");

            RuleFor(p => p.ContractType).NotNull().WithMessage("Тип трудового договора - должен быть указан");
            RuleFor(p => p.ContractType.Id).NotNull().WithMessage("Тип трудового договора - должен быть указан");
            RuleFor(p => p.ContractEndingDate).NotNull().When(p => p.ContractType.Id == 3 || p.ContractType.Id == 1).WithMessage("Дата окончания срочного ТД - должна быть указана");

            RuleFor(p => p.EmploymentType).NotNull().WithMessage("Вид занятости - должен быть указан");
            RuleFor(p => p.EmploymentType.Id).NotNull().WithMessage("Вид занятости - должен быть указан");

            RuleFor(p => p.WorkingTimeNorm).NotNull().WithMessage("График работы - должен быть указан");
            RuleFor(p => p.WorkingTimeNorm.Id).NotNull().WithMessage("График работы - должен быть указан");

            RuleFor(p => p.WorkFunction).NotNull().WithMessage("Производственная функция - должен быть указан");
            RuleFor(p => p.WorkFunction.Id).NotNull().WithMessage("Производственная функция - должен быть указан");

            RuleFor(p => p.StaffType).NotNull().WithMessage("Вид персонала - должен быть указан");
            RuleFor(p => p.StaffType.Id).NotNull().WithMessage("Вид персонала - должен быть указан");

            RuleFor(p => p.BossCpId).NotNull().NotEmpty().WithMessage("Непосредственный руководитель (CP_ID) - должен быть задан");

            RuleFor(p => p.RecruiterCpId).NotNull().NotEmpty().WithMessage("Рекрутер (CP_ID) - должен быть задан");

            RuleFor(p => p.BudgetOwnerCpId).NotNull().NotEmpty().WithMessage("Руководитель МВЗ (CP_ID) - должен быть задан");

            RuleFor(p => p.OfferId).NotNull().WithMessage("Идентификатор оффера - должен быть задан");
        }
    }
}