using CRMV2.Models;
using System.Diagnostics.Contracts;

namespace CRMV2.ViewModels
{
    public class Mappers
    {
        public Company CompanyMap(CompanyVM companyVM)
        {

            Company company = new Company();
            company.Name = companyVM.CompanyName;
            company.Adress = companyVM.Address;
            company.Email = companyVM.Email;
            return company;

        }

        public ContactPerson ContactPersonMap(ContactPersonVM contactPersonVM)
        {

            ContactPerson contactPerson = new ContactPerson();
            contactPerson.Name = contactPersonVM.Name;
            contactPerson.LastName = contactPersonVM.LastName;
            contactPerson.PhoneNumber = contactPersonVM.PhoneNumber;
            contactPerson.Email = contactPersonVM.Email;
            contactPerson.ContractId = (int)contactPersonVM.SelectedContractt;
            return contactPerson;

        }
        public Models.Contract ContractMap(ContractVM contractVM)
        {

            Models.Contract contract = new Models.Contract();
            contract.Name = contractVM.Name;
            contract.BeginDate = contractVM.BeginDate;
            contract.EndDate = contractVM.EndDate;
            contract.Active = contractVM.Active;
            contract.Type = contractVM.SelectedContractType;
            contract.CompanyId = (int)contractVM.SelectedCompany;
            return contract;

        }
        public User UserMap(UserVM userVM)
        {

            User user = new User();
            user.Id = userVM.Id;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Email = userVM.Email;
            user.Password = userVM.Password;
            return user ;

        }
        public Log LogMap(LogVM logVM) 
        {
            Log log = new Log();
            log.Id = logVM.Id;
            log.UserId = logVM.UserId;
            log.Date = logVM.Date;
            log.Thread = logVM.Thread;
            log.Level = logVM.Level;
            log.Logger = logVM.Logger;
            log.Message = logVM.Message;
            return log;
        }

    }
}